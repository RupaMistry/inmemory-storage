using Tranquillity.InMemory.Storage.DataOperations;
using Tranquillity.InMemory.Storage.Tests.Fakes;
using Tranquillity.InMemory.Storage.ValueContracts;
using NUnit.Framework;
using System;

namespace Tranquillity.InMemory.Storage.Tests.DataComponent
{
    [TestFixture]
    public class ProductKeyValueDataOperationsTests
    {
        private const string BookNameSpace = "BOOKS";
        private const string BookNameSpace2 = "book";
        private const string ClothingNameSpace = "Clothing";
        private const string InvalidNameSpace = "InvalidNameSpace";
        private const string ScienceFictionKey = "ScienceFiction";
        private const string MysteryKey = "Mystery";
        private const string HorrorKey = "Horror";
        private const string KidsKey = "Kids";

        private KeyValueXmlSerializerFake<Product> keyValueXmlSerializerFake;
        private ProductKeyValueDataOperations productKeyValueDataOperationsToTest;

        [SetUp]
        public void Setup()
        {
            this.keyValueXmlSerializerFake = new KeyValueXmlSerializerFake<Product>();
            productKeyValueDataOperationsToTest = new ProductKeyValueDataOperations(this.keyValueXmlSerializerFake);
        }

        [Order(1)]
        [TestCase(BookNameSpace, ScienceFictionKey, null)]
        [TestCase(BookNameSpace, ScienceFictionKey, default(Product))]
        public void Put_NullProductValue_ThrowsInvalidCastException(string nameSpace, string key, Product value)
        {
            Assert.Throws<InvalidCastException>(
                () => this.productKeyValueDataOperationsToTest.Put(nameSpace, key, value));
        }

        [Order(2)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Put_ValidProductValue_ReturnsSucesss(string nameSpace, string key)
        {
            int existingCount = this.productKeyValueDataOperationsToTest.TotalCount() + 1;
            this.keyValueXmlSerializerFake.SetSerializeValue(GetProductXml());

            this.productKeyValueDataOperationsToTest.Put(nameSpace, key, GetProductDetails());

            Assert.AreEqual(existingCount, this.productKeyValueDataOperationsToTest.TotalCount());
        }

        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Put_ValidProductValue_IsKeyValueSerialized(string nameSpace, string key)
        {
            this.keyValueXmlSerializerFake.SetSerializeValue(GetProductXml());

            this.productKeyValueDataOperationsToTest.Put(nameSpace, key, GetProductDetails());

            Assert.IsTrue(this.keyValueXmlSerializerFake.IsValueSerialized);
        }

        [Order(3)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Get_ValidParametes_IsKeyValueDeserialized(string nameSpace, string key)
        {
            this.keyValueXmlSerializerFake.SetSerializeValue(GetProductXml());

            this.productKeyValueDataOperationsToTest.Get(nameSpace, key);

            Assert.IsTrue(this.keyValueXmlSerializerFake.IsValueDeserialized);
        }

        [Order(4)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Get_ValidParametes_ReturnsProduct(string nameSpace, string key)
        {
            this.keyValueXmlSerializerFake.SetDeserializeValue(GetProductDetails());

            var product = this.productKeyValueDataOperationsToTest.Get(nameSpace, key);

            Assert.IsNotNull(product);
        }

        
        [TestCase(InvalidNameSpace, ScienceFictionKey)]
        public void Delete_InvalidParameters_ReturnsFalse(string nameSpace, string key)
        {
            var result = this.productKeyValueDataOperationsToTest.Delete(nameSpace, key);

            Assert.IsFalse(result);
        }

        [Order(5)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Delete_ValidParameters_ReturnsTrue(string nameSpace, string key)
        {
            var result = this.productKeyValueDataOperationsToTest.Delete(nameSpace, key);

            Assert.IsTrue(result);
        }

        private Product GetProductDetails()
        {
            return new Product()
            {
                Id = 1,
                Name = "ProductName",
                Description = "ProductDescription",
                Price = 1.0m,
                Unit = 5
            };
        }

        private static string GetProductXml()
        {
            return @"<?xml version='1.0'?><Product xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>  <Id>1</Id>  <Name>Book</Name>'
            +'<Price>2</Price><Unit>2</Unit>  <Description>Horror Stuff</Description></Product>";
        }
    }
}