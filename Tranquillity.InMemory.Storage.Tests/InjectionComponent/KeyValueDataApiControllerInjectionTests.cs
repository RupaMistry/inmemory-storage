using Tranquillity.InMemory.Storage.Api.Controllers;
using Tranquillity.InMemory.Storage.Injection;
using NUnit.Framework;

namespace Tranquillity.InMemory.Storage.Tests
{
    [TestFixture]
    public class KeyValueDataApiControllerInjectionTests
    {
        [Test]
        public void InitializeStartup_RegisterComponents_ReturnsValidApiControllerInstance()
        {
            // Arrange
            var instance = new InjectionStartup();
            instance.InitializeComponents();

            // Act
            var keyValueDataApiControllerInstance = instance.Container.GetInstance<KeyValueDataController>();

            // Assert
            Assert.IsNotNull(keyValueDataApiControllerInstance);
        }
    }
}
