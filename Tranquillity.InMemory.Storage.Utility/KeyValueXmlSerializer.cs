using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Tranquillity.InMemory.Storage.Utility
{
    public class KeyValueXmlSerializer<T>: Contracts.IKeyValueSerializer<T>
    {
        private readonly XmlSerializer _serializer = null;

        public KeyValueXmlSerializer()
        {
           this. _serializer = new XmlSerializer(typeof(T), new Type[] { typeof(T) }); ;
        }

        public string SerializeObject(T value)
        {
            using (var stream = new MemoryStream())
            {
                _serializer.Serialize(stream, value);
                stream.Position = 0;
                return Encoding.UTF8.GetString(stream.GetBuffer());
            }
        }

        public  T DeserializeObject(string serializedData)
        {
            if (string.IsNullOrWhiteSpace(serializedData))
                return default(T);

            using (var stream = new MemoryStream())
            {
                var bytes = Encoding.UTF8.GetBytes(serializedData);
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                return (T)_serializer.Deserialize(stream);
            }
        }
    }
}
