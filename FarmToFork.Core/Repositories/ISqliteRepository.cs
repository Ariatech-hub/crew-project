using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Core.Repositories
{
    public interface ISqlLiteRepository
    {
        Task InsertResponse(string data, string filed);
    }

}
