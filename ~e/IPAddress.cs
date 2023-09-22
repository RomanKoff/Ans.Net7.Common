using System.Collections;
using System.Net;

namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * bool IsInSubnet(this IPAddress address, IPSubnet subnet);
         */


        public static bool IsInSubnet(
            this IPAddress address,
            IpSubnet subnet)
        {
            if (subnet.MaskLength == 0)
                return true;
            if (subnet.IsV6)
            {
                var bits1 = new BitArray(
                    address.GetAddressBytes().Reverse().ToArray());
                var l1 = bits1.Length;
                if (subnet.MaskV6.Length != bits1.Length)
                    throw new ArgumentException(
                        "Length of IP Address and Subnet Mask do not match.");
                for (var i2 = l1 - 1; i2 >= l1 - subnet.MaskLength; i2--)
                    if (bits1[i2] != subnet.MaskV6[i2])
                        return false;
                return true;
            }
            var bits2 = BitConverter.ToUInt32(
                address.GetAddressBytes().Reverse().ToArray(), 0);
            var result2 = bits2 & subnet.MaskV4Template;
            return (subnet.MaskV4Result == result2);
        }

    }

}
