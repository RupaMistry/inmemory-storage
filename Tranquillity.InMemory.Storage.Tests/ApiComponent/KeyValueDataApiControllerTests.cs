using Tranquillity.InMemory.Storage.Api.Controllers;
using Tranquillity.InMemory.Storage.Api.Models;
using Tranquillity.InMemory.Storage.Tests.Fakes;
using Tranquillity.InMemory.Storage.ValueContracts;
using NUnit.Framework;

namespace Tranquillity.InMemory.Storage.Tests.ApiComponent
{
    [TestFixture]
    public class KeyValueDataApiControllerTests
    {
        private const string DefaultNameSpace = "DefaultNameSpace";
        private const string DefaultKey = "DefaultKey";

        private KeyValueStoreBusinessFake<Product> keyValueStoreBusinessFake;
        private KeyValueDataController keyValueDataControllerToTest;

        [SetUp]
        public void Setup()
        {
            this.keyValueStoreBusinessFake = new KeyValueStoreBusinessFake<Product>();
            this.keyValueDataControllerToTest = new KeyValueDataController(this.keyValueStoreBusinessFake);
        }

        [Test]
        public void Put_ValidParameters_IsGetNotCalled()
        {
            this.keyValueDataControllerToTest.Put(new KeyValueDataModel());

            Assert.IsTrue(this.keyValueStoreBusinessFake.IsPutCalled);
        }

        [Test]
        public void Put_NullParameters_IsGetNotCalled()
        {
            this.keyValueDataControllerToTest.Put(null);

            Assert.IsFalse(this.keyValueStoreBusinessFake.IsPutCalled);
        }

        [Test]
        public void Get_ValidParameters_IsGetCalled()
        {
            this.keyValueDataControllerToTest.Get(DefaultNameSpace, DefaultKey);

            Assert.IsTrue(this.keyValueStoreBusinessFake.IsGetCalled);
        }

        [Test]
        public void Values_ValidParameters_IsValuesCalled()
        {
            this.keyValueDataControllerToTest.Values(DefaultNameSpace);

            Assert.IsTrue(this.keyValueStoreBusinessFake.IsValuesCalled);
        }

        [Test]
        public void Delete_ValidParameters_IsDeleteCalled()
        {
            this.keyValueDataControllerToTest.Delete(DefaultNameSpace, DefaultKey);

            Assert.IsTrue(this.keyValueStoreBusinessFake.IsDeleteCalled);
        }
    }
}
