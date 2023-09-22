using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace Ans.Net7.Common
{

    public class IpSubnetsList
        : List<IpSubnet>
    {

        /* ctors */


        public IpSubnetsList(
            params string[] cidrs)
        {
            foreach (string s1 in cidrs)
                Add(new IpSubnet(s1));
        }


        public IpSubnetsList(
            string cidrs)
            : this(cidrs.Split(new char[] { ',', ';' }))
        {
        }


        /* functions */


        public override string ToString()
        {
            return string.Join("; ", this.Select(x => x.ToString()));
        }

    }



    public class IpSubnet
    {

        /* ctors */


        public IpSubnet(
            IPAddress address,
            int maskLength)
        {
            _init(address, maskLength);
        }


        public IpSubnet(
            string cidr)
        {
            var idx1 = cidr.IndexOf("/");
            if (idx1 == -1)
                throw new NotSupportedException(
                    "Only SubNetMasks with a given prefix length are supported.");
            _init(
                IPAddress.Parse(cidr[..idx1]),
                int.Parse(cidr[(idx1 + 1)..]));
        }


        /* readonly properties */


        public IPAddress Address { get; private set; }
        public bool IsV6 { get; private set; }
        public int MaskLength { get; private set; }
        public uint MaskV4Template { get; private set; }
        public uint MaskV4Result { get; private set; }
        public BitArray MaskV6 { get; private set; }


        /* functions */


        public override string ToString()
        {
            return $"{Address}/{MaskLength}";
        }


        /* privates */


        private void _init(
            IPAddress address,
            int maskLength)
        {
            if (maskLength < 0)
                throw new NotSupportedException(
                    "A Subnetmask should not be less than 0.");
            Address = address;
            MaskLength = maskLength;
            switch (Address.AddressFamily)
            {
                case AddressFamily.InterNetwork:
                    IsV6 = false;
                    var bits1 = BitConverter.ToUInt32(
                        Address.GetAddressBytes().Reverse().ToArray(), 0);
                    MaskV4Template = uint.MaxValue << (32 - MaskLength);
                    MaskV4Result = bits1 & MaskV4Template;
                    break;
                case AddressFamily.InterNetworkV6:
                    IsV6 = true;
                    MaskV6 = new BitArray(
                        Address.GetAddressBytes().Reverse().ToArray());
                    break;
                default:
                    throw new NotSupportedException(
                        "Only InterNetworkV6 or InterNetwork address families are supported.");
            }
        }

    }

}
