using Tranquillity.InMemory.Storage.Business;
using Tranquillity.InMemory.Storage.Tests.Fakes;
using Tranquillity.InMemory.Storage.ValueContracts;
using NUnit.Framework;
using System;

namespace Tranquillity.InMemory.Storage.Tests.BusinessComponent
{
    [TestFixture]
    public class KeyValueStoreBusinessTests
    {
        private const string EmptyString = "";
        private const string NullString = null;
        private const string WhiteSpaceString = "  ";
        private const string DefaultNameSpace = "DefaultNameSpace";
        private const string DefaultKey = "DefaultKey";

        private DataOperationsFake<Product> dataOperationsFake;
        private KeyValueStoreBusiness<Product> keyValueStoreBusinessToTest;

        private Product GetDefaultProduct()
        {
            return new Product();
        }

        [SetUp]
        public void Setup()
        {
            this.dataOperationsFake = new Fakes.DataOperationsFake<Product>();
            this.keyValueStoreBusinessToTest = new KeyValueStoreBusiness<Product>(this.dataOperationsFake);
        }

        [TestCase(EmptyString)]
        [TestCase(NullString)]
        [TestCase(WhiteSpaceString)]
        public void Put_InvalidNameSpace_ThrowsException(string nameSpace)
        {
            var productValue = GetDefaultProduct();

            Assert.Throws<ArgumentNullException>(() => this.keyValueStoreBusinessToTest.Put(nameSpace, DefaultKey, productValue));
        }

        [TestCase(EmptyString)]
        [TestCase(NullString)]
        [TestCase(WhiteSpaceString)]
        public void Put_InvalidKey_ThrowsException(string key)
        {
            var productValue = GetDefaultProduct();

            Assert.Throws<ArgumentNullException>(() => this.keyValueStoreBusinessToTest.Put(DefaultNameSpace, key, productValue));
        }

        [Test]
        public void Put_NullProductValue_ThrowsException()
        {
            Product productValue = null;

            Assert.Throws<ArgumentNullException>(() => this.keyValueStoreBusinessToTest.Put(DefaultNameSpace, DefaultKey, productValue));
        }

        [Test]
        public void Put_ValidParameters_AddDataOperationIsCalled()
        {
            Product productValue = this.GetDefaultProduct();

            this.keyValueStoreBusinessToTest.Put(DefaultNameSpace, DefaultKey, productValue);

            Assert.IsTrue(this.dataOperationsFake.IsKeyStoreValueAdded);
        }

        [Test]
        public void Get_ValidParameters_GetDataOperationIsCalled()
        {
            this.keyValueStoreBusinessToTest.Get(DefaultNameSpace, DefaultKey);

            Assert.IsTrue(this.dataOperationsFake.IsGetFunctionCalled);
        }

        [Test]
        public void Values_ValidParameters_ValuesDataOperationIsCalled()
        {
            this.keyValueStoreBusinessToTest.Values(DefaultNameSpace);

            Assert.IsTrue(this.dataOperationsFake.IsValuesFunctionCalled);
        }

        [Test]
        public void Delete_ValidParameters_DeleteDataOperationIsCalled()
        {
            this.keyValueStoreBusinessToTest.Delete(DefaultNameSpace, DefaultKey);

            Assert.IsTrue(this.dataOperationsFake.IsKeyStoreValueDeleted);
        }
    }
}