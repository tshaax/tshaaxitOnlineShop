using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string msg): base(msg)
        {
            
        }

        public NotFoundException(string name, object key)
            :base($"Entity \"{name}\"({key})")
        {
            
        }
    }
}
