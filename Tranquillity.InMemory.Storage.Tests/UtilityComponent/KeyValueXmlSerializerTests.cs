using Tranquillity.InMemory.Storage.Utility;
using Tranquillity.InMemory.Storage.ValueContracts;
using NUnit.Framework;

namespace Tranquillity.InMemory.Storage.Tests.UtilityComponent
{
    [TestFixture]
    class KeyValueXmlSerializerTests
    {
        private const string DefaultNameSpace = "DefaultNameSpace";
        
        private const string EmptyString = "";
        private const string NullString = null;
        private const string WhiteSpaceString = "  ";
        private const string InValidString = "InValidString";
        private const string EmptyXMLAttribute = "xsi:nil";

        private KeyValueXmlSerializer<Product> keyValueXmlSerializerToTest;

        [SetUp]
        public void Setup()
        {
            this.keyValueXmlSerializerToTest = new KeyValueXmlSerializer<Product>();
        }

        [TestCase(null)]
        [TestCase(default(Product))]
        public void Serialize_NullProduct_ReturnsEmptyXML(Product product)
        {
            var serializedProduct = this.keyValueXmlSerializerToTest.SerializeObject(product);

            Assert.IsTrue(serializedProduct.Contains(EmptyXMLAttribute));
        }

        [TestCase]
        public void Serialize_ValidProduct_ReturnsNonEmptyXML()
        {
            Product product = GetProductDetails();

            var serializedProduct = this.keyValueXmlSerializerToTest.SerializeObject(product);

            Assert.IsTrue(!serializedProduct.Contains(EmptyXMLAttribute));
        }

        [TestCase]
        public void Serialize_TwoWay_ReturnsSameObject()
        {
            var productComparer = new ProductComparer();

            // Serialize the given object
            var product = GetProductDetails();
            var serializedProduct = this.keyValueXmlSerializerToTest.SerializeObject(product);

            // Deserialize the above serialize string
            var deserializedProduct = this.keyValueXmlSerializerToTest.DeserializeObject(serializedProduct);

            // Check if the Deserialize output is same as the initial input object
            Assert.That(product, Is.EqualTo(deserializedProduct).Using(productComparer));
        }

        [TestCase(EmptyString)]
        [TestCase(NullString)]
        [TestCase(WhiteSpaceString)]
        public void Deserialize_InvalidString_ReturnsNullProduct(string serializedString)
        {
            var product = this.keyValueXmlSerializerToTest.DeserializeObject(serializedString);

            Assert.IsNull(product);
        }

        [Test]
        public void Deserialize_InvalidString_ThrowsException()
        {
            Assert.Throws<System.InvalidOperationException>(
                () => this.keyValueXmlSerializerToTest.DeserializeObject(InValidString));
        }

        [Test]
        public void Deserialize_ValidString_ReturnsProduct()
        {
            string serializedString = GetProductXml();

            var product = this.keyValueXmlSerializerToTest.DeserializeObject(serializedString);

            Assert.IsNotNull(product);
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
            return @"<?xml version='1.0'?><Product xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>  <Id>1</Id>  <Name>ProductName</Name>'
            +'<Price>1</Price><Unit>5</Unit>  <Description>ProductDescription</Description></Product>";
        }
    }
}
