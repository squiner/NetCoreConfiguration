using System;

namespace NetCoreConfiguration.Services
{
    public class IdService
    {
        private readonly Guid _id = Guid.NewGuid();

        public Guid GetId() => _id;
    }
}
