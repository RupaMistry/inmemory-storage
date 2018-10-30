
using Tranquillity.InMemory.Storage.ValueContracts;

namespace Tranquillity.InMemory.Storage.Api.Models
{
    public class KeyValueDataModel
    {
        public string NameSpace { get; set; }

        public string Key { get; set; }

        public Product Value { get; set; }
    }
}