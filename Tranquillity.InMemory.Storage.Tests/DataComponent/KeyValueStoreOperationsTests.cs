using Tranquillity.InMemory.Storage.Data;
using NUnit.Framework;

namespace Tranquillity.InMemory.Storage.Tests.DataComponent
{
    [TestFixture]
    public class KeyValueStoreOperationsTests
    {
        private const string BookNameSpace = "BOOK";
        private const string BookNameSpace2 = "book";
        private const string BookNameSpace3 = "B O O K";
        private const string ClothingNameSpace = "Clothing";
        private const string InvalidNameSpace = "InvalidNameSpace";
        private const string InvalidKey = "InvalidKey";
        private const string ScienceFictionKey = "ScienceFiction";
        private const string ScienceFictionKey2 = "Science Fictions ";
        private const string MysteryKey = "Mystery";
        private const string HorrorKey = "Horror";
        private const string KidsKey = "Kids";
        
        private KeyValueStoreOperations keyValueStoreOperationsToTest;

        [SetUp]
        public void Setup()
        {
            this.keyValueStoreOperationsToTest = KeyValueStoreOperations.Instance;
        }

        [Order(1)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Put_NewNameSpace_NewKey_KeyValueAdded(string nameSpace, string key)
        {
            string value = GetDataValue();

            this.keyValueStoreOperationsToTest.Put(nameSpace, key, value);

            Assert.AreEqual(1, this.keyValueStoreOperationsToTest.Count);
        }

        [Order(2)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Put_ExistingNameSpace_ExistingKey_KeyValueUpdated(string nameSpace, string key)
        {
            string value = GetUpdatedDataValue();

            this.keyValueStoreOperationsToTest.Put(nameSpace, key, value);

            var updatedValue = this.keyValueStoreOperationsToTest.Get(nameSpace, key);

            Assert.AreEqual(value,updatedValue);
        }

        [Order(3)]
        [TestCase(ClothingNameSpace, KidsKey)]
        public void Put_UniqueNameSpace_CreatesNewNameSpace(string nameSpace, string key)
        {
            string value = GetDataValue();

            this.keyValueStoreOperationsToTest.Put(nameSpace, key, value);

            Assert.AreEqual(2, this.keyValueStoreOperationsToTest.Count);
        }

        [Order(4)]
        [TestCase(BookNameSpace2, ScienceFictionKey)]
        public void Put_UniqueNameSpace_CaseSensitive_CreatesNewNameSpace(string nameSpace, string key)
        {
            string value = GetDataValue();

            this.keyValueStoreOperationsToTest.Put(nameSpace, key, value);

            Assert.AreEqual(3, this.keyValueStoreOperationsToTest.Count);
        }

        [Order(5)]
        [TestCase(BookNameSpace, MysteryKey)]
        public void Put_ExistingNameSpace_NewKey_KeyValueAdded(string nameSpace, string key)
        {
            string value = GetDataValue();

            this.keyValueStoreOperationsToTest.Put(nameSpace, key, value);

            Assert.AreEqual(4, this.keyValueStoreOperationsToTest.Count);
        }

        [TestCase(BookNameSpace3, ScienceFictionKey2)]
        public void Put_NameSpaceKey_TrailingSpaces_FormatAndAddKeyValue(string nameSpace, string key)
        {
            string value = GetDataValue();

            this.keyValueStoreOperationsToTest.Put(nameSpace, key, value);

            Assert.AreEqual(4, this.keyValueStoreOperationsToTest.Count);
        }

        [Order(6)]
        [TestCase(InvalidNameSpace, HorrorKey)]
        public void Get_InvalidNameSpace_InvalidKey_ThrowsException(string nameSpace, string key)
        {
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(
                () => this.keyValueStoreOperationsToTest.Get(nameSpace, key));
        }

        [TestCase(BookNameSpace, InvalidKey)]
        public void Get_ValidNameSpace_InValidKey_ThrowsException(string nameSpace, string key)
        {
            Assert.Throws<System.Collections.Generic.KeyNotFoundException>(
                () => this.keyValueStoreOperationsToTest.Get(nameSpace, key));
        }

        [Order(7)]
        [TestCase(BookNameSpace, ScienceFictionKey)]
        public void Get_ValidNameSpace_ValidKey_ReturnsValue(string nameSpace, string key)
        {
            var value = GetUpdatedDataValue();

            var result = this.keyValueStoreOperationsToTest.Get(nameSpace, key);

            Assert.AreEqual(value, result);
        }

        [TestCase(BookNameSpace, MysteryKey)]
        public void Get_ValidNameSpace_ValidKey_ReturnsSerializedValue(string nameSpace, string key)
        {
            var result = this.keyValueStoreOperationsToTest.Get(nameSpace, key);

            Assert.True(result.EndsWith(GetValueXMLEndElement()));
        }

        [Order(8)]
        [TestCase(InvalidNameSpace)]
        public void Values_InvalidNameSpace_ReturnsEmptyList(string nameSpace)
        {
            var result = this.keyValueStoreOperationsToTest.Values(nameSpace);

            Assert.AreEqual(0, result.Count);
        }

        [Order(9)]
        [TestCase(BookNameSpace)]
        public void Values_ValidNameSpace_ReturnsNonEmptyList(string nameSpace)
        {
            string value = GetUpdatedDataValue();

            var result = this.keyValueStoreOperationsToTest.Values(nameSpace);

            Assert.IsTrue(result.Count > 0);
        }

        [Order(10)]
        [TestCase(ClothingNameSpace)]
        public void Values_Filter_ClothingNameSpace_ReturnsOneKeyValue(string nameSpace)
        {
            var result = this.keyValueStoreOperationsToTest.Values(nameSpace);

            Assert.AreEqual(1, result.Count);
        }

        [TestCase(InvalidNameSpace, HorrorKey)]
        public void Delete_InvalidNameSpace_InvalidKey_ReturnsFalse(string nameSpace, string key)
        {
            var result = this.keyValueStoreOperationsToTest.Delete(nameSpace, key);

            Assert.IsFalse(result);
        }

        [Order(11)]
        [TestCase(ClothingNameSpace, KidsKey)]
        public void Delete_ValidNameSpace_ValidKey_ReturnsTrue(string nameSpace, string key)
        {
            var result = this.keyValueStoreOperationsToTest.Delete(nameSpace, key);

            Assert.IsTrue(result);
        }

        [Order(12)]
        [TestCase(ClothingNameSpace)]
        public void Delete_ValidNameSpace_Recheck_ReturnsValidCount(string nameSpace)
        {
            var result = this.keyValueStoreOperationsToTest.Values(nameSpace);

            Assert.AreEqual(0, result.Count);
        }

        private static string GetDataValue()
        {
            return @"<?xml version='1.0'?><Product xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>  <Id>1</Id>  <Name>Book</Name>'
            +'<Price>2</Price><Unit>2</Unit>  <Description>Science Stuff</Description></Product>";
        }

        private static string GetUpdatedDataValue()
        {
            return @"<?xml version='1.0'?><Product xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'>  <Id>1</Id>  <Name>Book</Name>'
            +'<Price>2</Price><Unit>2</Unit>  <Description>Horror Stuff</Description></Product>";
        }

        private static string GetValueXMLEndElement()
        {
            return "</Product>";
        }
    }
}
