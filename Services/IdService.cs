using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreConfiguration.Services
{
    public class IdService
    {
        private readonly Guid _id = Guid.NewGuid();

        public Guid GetId() => _id;
    }
}
