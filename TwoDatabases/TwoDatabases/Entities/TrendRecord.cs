using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDatabases.Entities
{
    public class TrendRecord
    {
        public int TrendServiceAcctId { get; set; }
        public string TrendServiceAcctCode { get; set; }
        public string ServiceName { get; set; }
        public int Transactions { get; set; }
    }
}
