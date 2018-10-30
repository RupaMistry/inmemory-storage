using System;

namespace Tranquillity.InMemory.Storage.ValueContracts
{
    public class Product : Object
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Unit { get; set; }
        public string Description { get; set; }
    }
}
