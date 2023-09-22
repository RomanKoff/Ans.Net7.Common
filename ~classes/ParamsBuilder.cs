using System.Text;

namespace Ans.Net7.Common
{

    public class ParamsBuilder
    {

        public ParamsCollection Parameters { get; private set; }


        /* ctor */


        public ParamsBuilder()
        {
            Parameters = new ParamsCollection();
        }


        /* methods */


        public void Append(
            string name,
            string value)
        {
            if (!string.IsNullOrEmpty(value))
                Parameters.Add(name, value);
        }


        public void Append(
            string name,
            bool value)
        {
            if (value)
                Parameters.Add(name, "1");
        }


        public void Append(
            string name,
            int value)
        {
            if (value != 0)
                Parameters.Add(name, value.ToString());
        }


        public void Append(
            string name,
            long value)
        {
            if (value != 0)
                Parameters.Add(name, value.ToString());
        }


        public void Append(
            string name,
            double value)
        {
            if (value != 0)
                Parameters.Add(name, value.ToString());
        }


        public void Append(
            string name,
            float value)
        {
            if (value != 0)
                Parameters.Add(name, value.ToString());
        }


        public void Append(
            string name,
            decimal value)
        {
            if (value != 0)
                Parameters.Add(name, value.ToString());
        }


        public void Append(
            string name,
            DateTime? value)
        {
            if (value != null)
                Parameters.Add(name, value.Value.ToString("u"));
        }


        /* functions */


        public override string ToString()
        {
            return _toString(Parameters);
        }


        public string ToString(
            string name,
            string value)
        {
            var a1 = new ParamsCollection(Parameters.Count, Parameters.Comparer);
            foreach (var item1 in Parameters)
                a1.Add(item1.Key, (string)item1.Value.Clone());
            a1.Append(name, value);
            return _toString(a1);
        }


        public string ToString(
            string name,
            int value)
        {
            return ToString(name, value.ToString());
        }


        /* privates */


        private static string _toString(
            ParamsCollection items)
        {
            if (!items.Any())
                return string.Empty;
            var sb1 = new StringBuilder();
            foreach (var item1 in items)
                sb1.Append($"&{item1.Key}={item1.Value}");
            return $"?{sb1.ToString()[1..]}";
        }

    }

}
