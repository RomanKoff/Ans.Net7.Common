namespace Ans.Net7.Common
{

    public class KeysComparer
    {

        /* ctor */


        public KeysComparer(
            IEnumerable<int> keys1,
            IEnumerable<int> keys2)
        {
            _keysComparer(keys1, keys2);
        }


        public KeysComparer(
            string[] keys1,
            string[] keys2)
        {
            _keysComparer(keys1, keys2);
        }


        public KeysComparer(
            string keys1,
            string keys2)
        {
            string[] a1 = (string.IsNullOrEmpty(keys1))
                ? Array.Empty<string>() : keys1.Split(new char[] { ';' });
            string[] a2 = (string.IsNullOrEmpty(keys2))
                ? Array.Empty<string>() : keys2.Split(new char[] { ';' });
            _keysComparer(a1, a2);
        }


        /* readonly properties */


        public IEnumerable<int> Deleted { get; private set; }
        public IEnumerable<int> Added { get; private set; }


        /* privates */


        private void _keysComparer(
            IEnumerable<int> keys1,
            IEnumerable<int> keys2)
        {
            Deleted = keys1.Except(keys2);
            Added = keys2.Except(keys1);
        }


        private void _keysComparer(
            string[] keys1,
            string[] keys2)
        {
            _keysComparer(keys1.ToIntArray(), keys2.ToIntArray());
        }

    }

}
