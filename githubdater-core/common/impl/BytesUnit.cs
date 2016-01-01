using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    public sealed class BytesUnit
    {
        public static BytesUnit Byte = new BytesUnit(0, "Byte", "B");
        public static BytesUnit KiloBytes = new BytesUnit(1, "KiloByte", "kb");
        public static BytesUnit MegaByte = new BytesUnit(2, "MegaByte", "MB");
        public static BytesUnit GigaByte = new BytesUnit(3, "GigaByte", "GB");

        public static IEnumerable<BytesUnit> Values
        {
            get
            {
                yield return Byte;
                yield return KiloBytes;
                yield return MegaByte;
                yield return GigaByte;
            }
        }

        private readonly int order;
        private readonly string unit;
        private readonly string shortHandle;

        private BytesUnit(int order, string unit, string shortHandle)
        {
            this.order = order;
            this.unit = unit;
            this.shortHandle = shortHandle;
        }

        public int Order { get { return order; } }
        public string Unit { get { return unit; } }
        public string ShortHandle { get { return shortHandle; } }

        public static BytesUnit FromOrder(int order)
        {
            foreach(BytesUnit unit in Values)
                if (unit.order == order)
                    return unit;

            throw new InvalidOperationException("Unit with order '" + order + "' can't be processed.");
        }

        public static int MaxOrder
        {
            get
            {
                int max = 0;

                foreach (BytesUnit unit in Values)
                    if (unit.order > max)
                        max = unit.order;

                return max;
            }
        }

        public long Convert(long bytes)
        {
            if (this.order == 0)
                return bytes;

            for(int i = 1; i <= this.order; i++)
                bytes = bytes / 1024;

            return bytes;
        }
        
        public static KeyValuePair<long, BytesUnit> GetHumanReadable(long bytes)
        {
            int order = 0;

            BytesUnit unit = BytesUnit.Byte;
            long newBytes = bytes;

            while ((newBytes >= 1000) && (order <= BytesUnit.MaxOrder))
            {
                unit = BytesUnit.FromOrder(order);
                newBytes = unit.Convert(bytes);
                order++;
            }

            return new KeyValuePair<long, BytesUnit>(unit.Convert(bytes), unit);
        }

        public override int GetHashCode()
        {
            return unit.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (obj == null)
                return false;

            if (obj is BytesUnit)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        public override string ToString()
        {
            return unit;
        }
    }
}
