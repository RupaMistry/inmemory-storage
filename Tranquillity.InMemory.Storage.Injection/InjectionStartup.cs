using Tranquillity.InMemory.Storage.Business;
using Tranquillity.InMemory.Storage.Business.Contracts;
using Tranquillity.InMemory.Storage.DataOperations;
using Tranquillity.InMemory.Storage.DataOperations.Contracts;
using Tranquillity.InMemory.Storage.Utility;
using Tranquillity.InMemory.Storage.Utility.Contracts;
using Tranquillity.InMemory.Storage.ValueContracts;
using SimpleInjector;
using System.Web.Mvc;

namespace Tranquillity.InMemory.Storage.Injection
{
    public class InjectionStartup
    {
        public Container Container { get; set; }

        public InjectionStartup()
        {
            this.Container = new Container();
        }

        public void InitializeComponents()
        {
            Container.Register<IKeyValueSerializer<Product>, KeyValueXmlSerializer<Product>>();
            Container.Register<IDataOperations<Product>, ProductKeyValueDataOperations>();
            Container.Register<IStoreBusiness<Product>, KeyValueStoreBusiness<Product>>();

            Container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
            
        }
    }
}