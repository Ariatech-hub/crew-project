using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Exception
{
    public class GeneralException : System.Exception
    {
        public GeneralException() { }

        public GeneralException(string message)
            : base(message) { }

        public GeneralException(string message, System.Exception inner)
            : base(message, inner) { }
    }
}
