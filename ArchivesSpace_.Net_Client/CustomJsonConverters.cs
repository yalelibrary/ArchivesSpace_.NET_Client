using System;
using ArchivesSpace_.Net_Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ArchivesSpace_.Net_Client
{
    internal class CustomNoteConverter
    {

    }

    public class JsonAspaceNoteConverter : Newtonsoft.Json.Converters.CustomCreationConverter<NoteBase>
    {

        public override NoteBase Create(Type objectType)
        {
            var obj = objectType;
            return new NoteBase();
            //throw new NotImplementedException();
        }

        public NoteBase Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("jsonmodel_type");
            switch (type)
            {
                case "note_singlepart":
                    return new NoteSinglepart();
                case "note_multipart":
                    return new NoteMultipart();
            }
            return new NoteBase();
            throw new ApplicationException(String.Format("The given note type {0} is not supported!", type));
        }
        
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);
            //JObject jObject = JArray.Load(reader);

            // Create target object based on JObject
            var target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
        
    }

    public class JsonAspaceNoteItemConverter : Newtonsoft.Json.Converters.CustomCreationConverter<NoteItemBase>
    {

        public override NoteItemBase Create(Type objectType)
        {
            var obj = objectType;
            return new NoteItemBase();
            //throw new NotImplementedException();
        }

        public NoteItemBase Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("jsonmodel_type");
            switch (type)
            {
                case "note_chronology":
                    return new NoteItemChronology();
                case "note_orderedlist":
                    return new NoteItemOrderedList();
                case "note_definedlist":
                    return new NoteItemDefinedList();
                case "note_text":
                    return new NoteItemText();
            }
            return new NoteItemBase();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }

    }
}
