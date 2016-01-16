using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGitHubdater
{
    /// <summary>
    /// Represents a unit of measurement for data. 1 byte = 8 bits.
    /// </summary>
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

        private BytesUnit(int order, string unit, string shortHandle)
        {
            this.Order = order;
            this.Unit = unit;
            this.ShortHandle = shortHandle;
        }

        /// <summary>
        /// The order of the unit, from 0 (Byte) to 3 (GigaByte).
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// The unit label (e.g. KiloByte).
        /// </summary>
        public string Unit { get; }

        /// <summary>
        /// The unit short handle (e.g. kb for KiloByte).
        /// </summary>
        public string ShortHandle { get; }

        /// <summary>
        /// Static constructor used to instanciate a <see cref="BytesUnit"/> from a given <see cref="Order"/>.
        /// </summary>
        /// <param name="order">The order used to instanciate the <see cref="BytesUnit"/></param>.
        /// <returns></returns>
        public static BytesUnit FromOrder(int order)
        {
            foreach(BytesUnit unit in Values)
                if (unit.Order == order)
                    return unit;

            throw new InvalidOperationException("Unit with order '" + order + "' can't be processed.");
        }

        /// <summary>
        /// The maximum <see cref="Order"/> handled by the <see cref="BytesUnit"/> class.
        /// </summary>
        public static int MaxOrder
        {
            get
            {
                int max = 0;

                foreach (BytesUnit unit in Values)
                    if (unit.Order > max)
                        max = unit.Order;

                return max;
            }
        }

        /// <summary>
        /// Convert a given bytes quantity into the <see cref="BytesUnit"/> equivalent.
        /// </summary>
        /// <param name="bytes">The bytes number to convert.</param>
        /// <returns></returns>
        public long Convert(long bytes)
        {
            if (this.Order == 0)
                return bytes;

            for(int i = 1; i <= this.Order; i++)
                bytes = bytes / 1024;

            return bytes;
        }

        /// <summary>
        /// <para>Convert a given bytes quantity into the most readable form for a human.</para>
        /// <para>The most readable form is the one where the bytes number is less than 1000.</para>
        /// </summary>
        /// <param name="bytes">The bytes number to convert.</param>
        /// <returns>A key/value pair with the new bytes number as the key and the <see cref="BytesUnit"/> as the value.</returns>
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
            return Unit.GetHashCode();
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
            return Unit;
        }
    }
}
