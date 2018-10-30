//using Tranquillity.InMemory.Storage.Data.Contracts;
//using NUnit.Framework;
//using System.Collections.Generic;
//using System.Threading;

//// TODO: To Run this test class succesfully, change KeyValueStore from internal to public.

//namespace Tranquillity.InMemory.Storage.Tests.DataComponent
//{
//    [TestFixture]
//    public class KeyValueStoreTests
//    {
//        private const string DefaultNameSpace = "DefaultNameSpace";
//        private const string DefaultValue = "DefaultValue";
//        private const int Count = 1000;

//        [OneTimeSetUp]
//        public void OneTimeSetUp()
//        {
//            KeyValueStore.Clear();
//        }

//        [Order(1)]
//        [TestCase(1, 10)]
//        public void CreateOrUpdate_Concurrency_OneThread_MultipleKeys(int noOfThreads, int addUpdateAttempts)
//        {
//            var threadName = string.Format("t{0}", noOfThreads);

//            var t1 = new Thread(() => AddOrUpdateKeyStore(threadName, addUpdateAttempts));

//            t1.Start();
//            t1.Join();

//            Assert.AreEqual(addUpdateAttempts, KeyValueStore.Count);
//        }

//        [Order(2)]
//        [TestCase(5, 10)]
//        [TestCase(10, 100)]
//        [TestCase(100, Count)]
//        public void CreateOrUpdate_Concurrency_MultipleThreads_MultipleNamespaces(int noOfThreads, int addUpdateAttempts)
//        {
//            for (int i = 0; i < noOfThreads; i++)
//            {
//                var threadName = string.Format("t{0}", i);

//                var thread = new Thread(() => AddOrUpdateKeyStore(threadName, addUpdateAttempts));

//                thread.Start();
//                thread.Join();
//            }

//            Assert.AreEqual(noOfThreads * addUpdateAttempts, KeyValueStore.Count);
//        }

//        [Order(3)]
//        [Test]
//        public void GetValue_Concurrency_MultipleThreads_SameNamespace()
//        {
//            var newValue = "UpdatedValue";
//            var existingNameSpaceKey = "t3-DefaultNameSpace_Key1";
//            string storedValue = string.Empty;

//            // Out of all the threads, give this one the highest priority
//            var updateThread = new Thread(() => KeyValueStore.CreateOrUpdate(existingNameSpaceKey, newValue)) { Priority = ThreadPriority.Highest };

//            var readThread = new Thread(() => { storedValue = KeyValueStore.GetValue(existingNameSpaceKey); }) { Priority = ThreadPriority.Normal };

//            updateThread.Start();
//            readThread.Start();

//            updateThread.Join();
//            readThread.Join();

//            Assert.AreEqual(newValue, storedValue);
//        }

//        [Order(4)]
//        [Test]
//        public void GetAllValues_LookupInvalidFormatNameSpace_ThrowsException()
//        {
//            Assert.Throws<KeyNotFoundException>(
//                () => KeyValueStore.GetAllValues(DefaultNameSpace));
//        }

//        [Order(5)]
//        [Test]
//        public void GetAllValues_LookupValidNameSpace_ReturnsList()
//        {
//            var existingNameSpaceKey = "t0-DefaultNameSpace0_";

//            var list = KeyValueStore.GetAllValues(existingNameSpaceKey);

//            // May Fail by returning the count as 0 since all Thread calls are non-deterministic. 
//            Assert.AreEqual(1, list.Count);
//        }

//        private void AddOrUpdateKeyStore(string threadName, int addUpdateAttempts)
//        {
//            for (int x = 0; x < addUpdateAttempts; x++)
//            {
//                string nameSpace = string.Format("{0}-{1}{2}", threadName, DefaultNameSpace, x);
//                var key = string.Format("{0}_Key{1}", nameSpace, x);
//                KeyValueStore.CreateOrUpdate(key, (DefaultValue + x));
//            }
//        }
//    }
//}