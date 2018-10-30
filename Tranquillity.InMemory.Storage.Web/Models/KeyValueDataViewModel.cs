using Tranquillity.InMemory.Storage.ValueContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tranquillity.InMemory.Storage.Web.Models
{
    public class KeyValueDataViewModel
    {
        public string NameSpace { get; set; }

        public string Key { get; set; }

        public Product Value { get; set; }
    }
}