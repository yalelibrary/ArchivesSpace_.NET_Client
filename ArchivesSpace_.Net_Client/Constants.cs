using System;
using System.ComponentModel;
using System.Configuration;

namespace ArchivesSpace_.Net_Client
{
    public static class Constants
    {
        public static Uri ArchivesSpaceBaseUrl = new Uri(GetConfigSetting<string>("archivesSpaceApiUri"));
        public static string ArchivesSpaceUsername = GetConfigSetting<string>("archivesSpaceUsername");
        public static string ArchivesSpacePassword = GetConfigSetting<string>("archivesSpacePassword");

        public static T GetConfigSetting<T>(string key)
        {
            try
            {
                var appSetting = ConfigurationManager.AppSettings[key];
                if (string.IsNullOrWhiteSpace(appSetting))
                    throw new ConfigurationErrorsException(
                        string.Format("Helpers.AppSettings: key {0} was null or empty", key));

                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T) (converter.ConvertFromInvariantString(appSetting));

            }
            catch (ConfigurationErrorsException ex)
            {
                AsLogger.LogWarning("Configuration value was missing from configuration file. Returning default value.", ex);
                return default(T);
            }
            catch (Exception ex)
            {
                AsLogger.LogError("Unexpected exception accessing configuration file or data", ex);
                throw;
            }
        }
    }
}
