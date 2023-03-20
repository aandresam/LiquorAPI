using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace liquorApi.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base() {}

        public ProductNotFoundException(string message) : base(message) {}
    }
}