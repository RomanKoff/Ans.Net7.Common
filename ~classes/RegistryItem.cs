namespace Ans.Net7.Common
{

    public class RegistryItem
    {

        /* ctors */


        public RegistryItem()
        {
        }


        public RegistryItem(
            string key,
            string value,
            int level)
            : this()
        {
            Key = key;
            Value = value;
            Level = level;
        }


        public RegistryItem(
            int key,
            string value,
            int level)
            : this(key.ToString(), value, level)
        {
        }


        public RegistryItem(
            string key,
            string value)
            : this(key, value, 0)
        {
        }


        public RegistryItem(
            int key,
            string value)
            : this(key, value, 0)
        {
        }


        /* properties */


        public string Key { get; set; }
        public string Value { get; set; }
        public int Level { get; set; }
        public bool IsLabel { get; set; }


        /* functions */


        public string ValueSafe
        {
            get => Value?.Replace(";", "\\;");
            set => Value = value.Replace("\\;", ";");
        }


        /// <summary>
        /// Возвращает сериализацию элемента в виде строки
        /// "key=value" или "key=level:value".
        /// Символ ';' экранируется "\;"
        /// </summary>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.Key))
                return string.Empty;
            if (Level > 0)
                return string.Format("{0}={1}:{2}", Key, Level, ValueSafe);
            return string.Format("{0}={1}", Key, ValueSafe);
        }


        /* methods */


        /// <summary>
        /// Десериализация элемента из строки вида
        /// "key=value" или "key=level:value"
        /// Символ ';' экранируется "\;"
        /// </summary>
        public void FillFromString(
            string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                Key = string.Empty;
                Value = string.Empty;
                Level = 0;
            }
            else
            {
                int p1 = source.IndexOf('=');
                if (p1 > 0)
                {
                    Key = source[..p1];
                    var v1 = source[(p1 + 1)..];
                    p1 = v1.IndexOf(':');
                    if (p1 > 0)
                    {
                        ValueSafe = v1[(p1 + 1)..];
                        Level = v1[..p1].ToInt(0);
                    }
                    else
                    {
                        ValueSafe = v1;
                        Level = 0;
                    }
                }
                else
                {
                    Key = source;
                    ValueSafe = source;
                    Level = 0;
                }
            }
        }

    }

}
