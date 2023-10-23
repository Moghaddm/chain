using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain.Infrastructure.Persistence
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string ProductCollectionName { get; set; }
        public string DatabaseName { get; set; }
    }
}
