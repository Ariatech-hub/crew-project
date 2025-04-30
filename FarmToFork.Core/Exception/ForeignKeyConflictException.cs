using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Exception
{
    public class ForeignKeyConflictException : System.Exception
    {
        public ForeignKeyConflictException()
        {

        }

        public ForeignKeyConflictException(String message) : base(message)
        {

        }

        public ForeignKeyConflictException(string message, System.Exception? innerException) : base(message,
            innerException)
        {

        }
    }
}
