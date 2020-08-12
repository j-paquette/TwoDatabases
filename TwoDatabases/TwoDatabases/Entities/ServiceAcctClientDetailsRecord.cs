using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDatabases.Entities
{
    public class ServiceAcctClientDetailsRecord
    {
        public int ServiceAcctID { get; set; }
        public string ServiceAcctCode { get; set; }
        public string CsdUrl { get; set; }
        public string ContactEmail { get; set; }
    }
}
