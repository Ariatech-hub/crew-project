using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Exception
{
    public class DataNotFoundException : System.Exception
    {
        public DataNotFoundException() { }
        public DataNotFoundException(String message) : base(message) { }
        public DataNotFoundException(string message, System.Exception? innerException) : base(message, innerException)
        { }
    }
}
