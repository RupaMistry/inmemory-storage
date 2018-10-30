using Tranquillity.InMemory.Storage.ValueContracts;
using System;
using System.Collections.Generic;

namespace Tranquillity.InMemory.Storage.Tests.UtilityComponent
{
    internal class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product product1, Product product2)
        {
            if (product1 == null && product2 == null)
            {
                return true;
            }

            if (product1 == null || product2 == null)
            {
                return false;
            }

            return (product1.Id == product2.Id) && (product1.Name == product2.Name) && (product1.Description == product2.Description) &&
                (product1.Price == product2.Price) && (product1.Unit == product2.Unit);
        }

        public int GetHashCode(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}