
namespace Tranquillity.InMemory.Storage.Utility.Contracts
{
    public interface IKeyValueSerializer<T>
    {
        string SerializeObject(T value);

        T DeserializeObject(string serializedData);
    }
}