using System.Runtime.Serialization;

namespace Ans.Net7.Common
{

    [Serializable]
    public class ParamsCollection
        : Dictionary<string, string>
    {

        /* ctors */


        protected ParamsCollection(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }


        public ParamsCollection()
            : base()
        {
        }


        public ParamsCollection(
            int capacity,
            IEqualityComparer<string> comparer)
            : base(capacity, comparer)
        {
        }


        /* methods */


        public void Append(
            string name,
            string value)
        {
            if (ContainsKey(name))
                this[name] = value;
            else
                Add(name, value);
        }


        public void AppendInt(
            string name,
            int value)
        {
            Append(name, value.ToString());
        }


        public void AppendLong(
            string name,
            long value)
        {
            Append(name, value.ToString());
        }


        public void AppendDouble(
            string name,
            double value)
        {
            Append(name, value.ToString());
        }


        public void AppendFloat(
            string name,
            float value)
        {
            Append(name, value.ToString());
        }


        public void AppendDecimal(
            string name,
            decimal value)
        {
            Append(name, value.ToString());
        }


        public void AppendDataTime(
            string name,
            DateTime value)
        {
            Append(name, value.ToString());
        }

    }

}
