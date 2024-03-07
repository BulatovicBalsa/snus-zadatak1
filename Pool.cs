using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    internal class Pool
    {
        public double Level { get; set; } = 2.5;
        public double MinLevel { get; set; } = 0.0;
        public double MaxLevel { get; set; } = 5.0;
        public PoolStatus Status { get; set; } = PoolStatus.Half;
    }
}
