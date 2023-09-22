using System.Text;

namespace Ans.Net7.Common
{

    public class TinyLogWriterHelper
    {

        private StringBuilder _sb;
        private int _count;


        /* ctor */


        public TinyLogWriterHelper(
            string filename,
            bool rewrite,
            int length = 300)
        {
            Filename = filename;
            Length = length;
            _init();
            if (rewrite)
                SuppIO.FileWrite(Filename, null);
        }


        /* properties */


        public int Length { get; set; }


        /* readonly properties */


        public string Filename { get; private set; }


        /* methods */


        public void Append(
            string text)
        {
            _sb.Append(text);
            _test();
        }


        public void Append(
            string template,
            params object[] args)
        {
            Append(string.Format(template, args));
        }


        public void AppendLine(
            string text)
        {
            _sb.AppendLine(text);
            _test();
        }


        public void AppendLine(
            string template,
            params object[] args)
        {
            AppendLine(string.Format(template, args));
        }


        public void AppendLog(
            string template,
            params object[] args)
        {
            string s1 = string.Format(template, args);
            Append(s1);
            Console.Write(s1);
        }


        public void AppendLineLog(
            string template,
            params object[] args)
        {
            string s1 = string.Format(template, args);
            AppendLine(s1);
            Console.WriteLine(s1);
        }


        public void Save()
        {
            SuppIO.FileWrite(
                Filename, _sb.ToString(), mode: FileMode.Append);
            _init();
        }


        /* privates */


        private void _init()
        {
            _sb = new StringBuilder();
            _count = 0;
        }


        private void _test()
        {
            _count++;
            if (_count > Length)
                Save();
        }

    }

}
