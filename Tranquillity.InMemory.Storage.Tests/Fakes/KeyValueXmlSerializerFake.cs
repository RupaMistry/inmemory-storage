using Tranquillity.InMemory.Storage.Utility.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquillity.InMemory.Storage.Tests.Fakes
{
    public class KeyValueXmlSerializerFake<T> : IKeyValueSerializer<T>
    {
        public bool IsValueSerialized { get; set; }

        public bool IsValueDeserialized { get; set; }
        
        private string serializedValue { get; set; }

        private T deserializedValue { get; set; }

        public T DeserializeObject(string serializedData)
        {
            this.IsValueDeserialized = true;
            return deserializedValue;
        }

        public string SerializeObject(T value)
        {
            this.IsValueSerialized = true;
            return this.serializedValue;
        }

        public void SetSerializeValue(string serializedValue)
        {
            this.serializedValue = serializedValue;
        }

        public void SetDeserializeValue(T serializedValue)
        {
            this.deserializedValue = serializedValue;
        }
    }
}
