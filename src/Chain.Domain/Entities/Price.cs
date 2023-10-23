using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain.Domain.Entities
{
    public sealed class Price
    {
        public long Tuman { get; private set; }
        public long Rial { get; private set; }
        public Price(long tuman, long rial)
        {
            if (Tuman <= 0)
                throw new ArgumentException($"{Tuman} is Negative Or Zero.");
            if (Rial <= 0)
                throw new ArgumentException($"{Rial} is Negative Or Zero.");
            Tuman = tuman;
            Rial = rial;
        }
    }
}
