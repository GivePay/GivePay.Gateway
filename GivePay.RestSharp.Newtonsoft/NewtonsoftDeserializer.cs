using System.IO;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

// See: https://bytefish.de/blog/restsharp_custom_json_serializer/
namespace GivePay.RestSharp.Newtonsoft
{
    public class NewtonsoftJsonSerializer : IDeserializer
    {
        private readonly JsonSerializer _serializer;

        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public string ContentType
        {
            get { return "application/json"; } // Probably used for Serialization?
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return _serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new JsonSerializer
                {
                    
                });
            }
        }
    }
}
