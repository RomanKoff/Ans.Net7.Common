namespace Ans.Net7.Common
{

    public abstract class _StringSerialized_Proto
    {
        public string[] Items { get; private set; }

        public _StringSerialized_Proto(
            string serial,
            int count)
        {
            Items = new string[count];
            var a1 = serial.Split('|');
            int c1 = Math.Min(a1.Length, count);
            for (int i1 = 0; i1 < c1; i1++)
                Items[i1] = a1[i1];
            if (c1 < count)
                for (int i1 = c1; i1 < count; i1++)
                    Items[i1] = null;
        }

        public abstract void Parse();
    }

}
