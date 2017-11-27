using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArchivesSpace_.Net_Client
{
    public class ArchivesSpaceConnectionHandler
    {   
        public JObject UserData;
        private bool _canImpersonate;
        private string _sessionKey;
        private readonly string _baseUrl;
        private readonly ArchivesSpaceCredential _userCred;
        private readonly HttpClient _httpClient;


        public ArchivesSpaceConnectionHandler(ArchivesSpaceCredential cred) : this (cred, Constants.ArchivesSpaceBaseUrl)
        {
        }

        public ArchivesSpaceConnectionHandler(ArchivesSpaceCredential cred, Uri apiBaseUri)
        {
            var baseUrl = apiBaseUri.ToString();
            if (baseUrl.EndsWith(@"/")) //We add the joining slash when creating the web request
            {
                baseUrl = baseUrl.TrimEnd('/');
            }
            _userCred = cred;
            _baseUrl = baseUrl;
            UserData = new JObject();

            ServicePointManager.DefaultConnectionLimit = 20;
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
	
	    public bool Login()
	    {
		    try
		    {
			    LoginAction();
			    return true;
		    }
		    catch (WebException ex) //Note we're using a webexception since login happens with webclient, everything else with httpclient
		    {
			    AsLogger.LogWarning("caught invalid login attempt, returning false. error was: ", ex);
		    }
		    return false;
	    }

        public void Impersonate(string targetUser)
        {
            if (_canImpersonate)
            {
                var apiUrl = string.Format("users/{0}/become-user", targetUser);
                var responseText = PostActionAsync(apiUrl).Result; //no deadlock b/c configure await is off
                var responseJson = JObject.Parse(responseText);
                UserData = responseJson["user"].Value<JObject>();                 
            }
            else
            {
                var ex = new InvalidOperationException("user does not have permission to impersonate other users");
                AsLogger.LogError(String.Format("user [ {0} ] attempted to impersonate user [ {1} ] but lacks become_user permission in ArchivesSpace", _userCred.Username, targetUser), ex);
            }

        }

        public void EndImpersonation()
        {
            LoginAction();
        }
        
        //goes here since the repository collection may be needed before a service is created
        public async Task<ICollection<Repository>> GetAllRepositoriesAsync()
        {
            return await GetAllRepositoriesActionAsync();
        }

        private async Task<ICollection<Repository>> GetAllRepositoriesActionAsync()
        {
            var uri = @"repositories";
            var response = await GetAsync(uri).ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<List<Repository>>(response);
            return result;
        }
	
        public async Task<string> GetAsync(string apiUri)
        {
            HttpResponseMessage response;
            if (string.IsNullOrEmpty(_sessionKey))
            {
                LoginAction(); //and we're going to let it throw an unauthorized exception if it doesn't work since the user isn't checking first
            }
            try
            {
                response = await GetActionAsync(apiUri).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return responseString;
            }
            catch (HttpRequestException ex)
            {
                //We are forcing a success code to continue, most likely error is an expired session so try that first
                AsLogger.LogError(String.Format("[ArchivesSpaceConnectionHandler.GetAsync] Error getting api resource [ {0} ] - attempting to login and retry", apiUri), ex);
                LoginAction();
                try
                {
                    response = GetActionAsync(apiUri).Result;
                    response.EnsureSuccessStatusCode();
                    var responseString = response.Content.ReadAsStringAsync().Result;
                    return responseString;
                }
                catch (HttpRequestException rex)
                {
                    AsLogger.LogError("[ArchivesSpaceConnectionHandler.GetAsync] Request failed after retrying login. Exception follows.", rex);
                    throw;
                }

            }
        }
	
        public async Task<string> PostAsync(string apiUri, IEnumerable<KeyValuePair<string,string>> postParams)
        {
            return await PostActionAsync(apiUri, postParams);
        }

        public async Task<string> PostAsync(string apiUri, JObject postData)
        {
            return await PostActionAsync(apiUri, postData);
        }

	    private void LoginAction()
	    {
            //This can't be async and needs to happen before we set the global request headers of the httpclient field
		    using (var wc = new WebClient())
		    {
                AsLogger.LogDebug(String.Format("ASpace Login routine starting for user with username [ {0} ]. Current session key is [ {1} ]", _userCred.Username, _sessionKey ?? "null"));
			    string loginUri = String.Format("{0}/users/{1}/login",_baseUrl, _userCred.Username);
			    var postParams = new NameValueCollection();
			    postParams.Add("password",_userCred.Password);
		
			    var response = wc.UploadValues(loginUri, postParams);
			    var responseString = Encoding.UTF8.GetString(response);
			    var responseJson = JObject.Parse(responseString);

			    _sessionKey = responseJson["session"].Value<string>();
                AsLogger.LogDebug(String.Format("New session key set: [ {0} ]", _sessionKey));
			    UserData = responseJson["user"].Value<JObject>();
		        _canImpersonate = UserData.ToString().Contains("become_user");
		        _httpClient.SetArchivesSpaceHeader(_sessionKey);
		    }
	    }

        private async Task<HttpResponseMessage> GetActionAsync(string apiUri)
        {
            //ToDo: Returning a string response is a design flaw in this context since we may want to handle 404s and other errors
            //which make sense for the REST API but would blend in with Network Errors if we convert to a string right away.
            //Error handling should happen upstream when we're more sure of what the request is about
            if (apiUri.StartsWith(@"/"))
            {
                apiUri = apiUri.TrimStart('/');
            }
            var uriString = String.Format("{0}/{1}", _baseUrl, apiUri);    
            AsLogger.LogDebug(String.Format("Requesting URL: {0}", uriString));
            AsLogger.LogDebug(String.Format("Current Session Key: [ {0} ]", _sessionKey));

            try
            {
                var response = await _httpClient.GetAsync(uriString).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    AsLogger.LogDebug("[GetActionAsync] Request Successful, returning response content");
                    return response;
                }
                else
                {
                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    AsLogger.LogWarning(String.Format("[GetActionAsync] Request did not return a success status code. Code [ {0} : {1} ] Message Body [ {2} ]", (int)response.StatusCode, response.StatusCode, responseString));
                    return response;
                }
            }
            catch (HttpRequestException rex)
            {
                AsLogger.LogError(String.Format("[ArchivesSpaceConnectionHandler.GetActionAsync] API request to [ {0} ] failed. Exception follows.", uriString), rex);
                throw;
            }

        }

        private async Task<string> PostActionAsync(string apiUri)
	    {
            var uri = String.Format("{0}/{1}", _baseUrl, apiUri);

            AsLogger.LogDebug(String.Format("Posting to URI: {0}", uri));

            var httpContent = new StringContent(""); //Empty, we're just posting to the URI in this method
            var response = await _httpClient.PostAsync(uri, httpContent).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseString;

	    }

        private async Task<string> PostActionAsync(string apiUri, IEnumerable<KeyValuePair<string,string>> postParams)
        {
            var uri = String.Format("{0}/{1}", _baseUrl, apiUri);

            AsLogger.LogDebug(String.Format("Posting to URI: {0}", uri));

            var postBody = new FormUrlEncodedContent(postParams);
            var response = await _httpClient.PostAsync(uri, postBody).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return responseString;
        }

        private async Task<string> PostActionAsync(string apiUri, JObject postData)
	    {
            var uri = String.Format("{0}/{1}", _baseUrl, apiUri);
            //_logger.LogDebug(String.Format("Posting to URI: {0}", uri)); //can't log uri for the JObject because if it's a user post the uri may contain password

            var postBody = new StringContent(postData.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, postBody).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return responseString;
	    }
    }
}
