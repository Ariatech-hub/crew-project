using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Exception
{
    public class DataValidationException : System.Exception
    {
        public DataValidationException() { }
        public DataValidationException(String message) : base(message) { }
        public DataValidationException(string message, System.Exception? innerException) : base(message, innerException)
        { }
    }
}
