using System.Reflection;

namespace Ans.Net7.Common
{

    public static class SuppConsole
    {

        /*
         * int Left { get; set; }
         * int Top { get; set; }
         * int Counter { get; set; }
         * 
         * void InputClear();
         * void SetCounter(int counter);
         * void CounterUp();
         * void CounterDown();
         * void SavePos();
         * void RestopePos();
         * void WriteFreeze(string template, params object[] args);
         * void Write(ConsoleColor fgColor, string value);
         * void Write(ConsoleColor fgColor, bool value);
         * void Write(bool expression, ConsoleColor fgColor, string value);
         * void Write(bool expression, ConsoleColor fgColor, bool value);
         * void Start(string title);
         * void Start();
         * void SectionStart(string title);
         * void SectionEnd(string title = null);
         * void End();
         * void BreakLineW();
         * void BreakLine();
         * 
         * ConsoleKeyInfo ReadKey();
         */


        /* properties */


        public static int Left { get; set; }
        public static int Top { get; set; }
        public static int Counter { get; set; }


        /* methods */


        public static void InputClear()
        {
            while (Console.KeyAvailable)
                _ = Console.ReadKey(true);
        }


        public static void SetCounter(
            int counter)
        {
            SavePos();
            Counter = counter;
        }


        public static void CounterUp()
        {
            Counter++;
            RestopePos();
            Console.Write(Counter);
            Console.WriteLine(" ");
        }


        public static void CounterDown()
        {
            Counter--;
            RestopePos();
            Console.Write(Counter);
            Console.WriteLine(" ");
        }


        public static void SavePos()
        {
            Left = Console.CursorLeft;
            Top = Console.CursorTop;
        }


        public static void RestopePos()
        {
            Console.SetCursorPosition(Left, Top);
        }


        public static void WriteFreeze(
            string template,
            params object[] args)
        {
            SavePos();
            Console.Write(template, args);
            RestopePos();
        }


        public static void Write(
            ConsoleColor fgColor,
            string value)
        {
            var save1 = Console.ForegroundColor;
            Console.ForegroundColor = fgColor;
            Console.Write(value);
            Console.ForegroundColor = save1;
        }


        public static void Write(
            ConsoleColor fgColor,
            bool value)
        {
            var save1 = Console.ForegroundColor;
            Console.ForegroundColor = fgColor;
            Console.Write(value);
            Console.ForegroundColor = save1;
        }


        public static void Write(
            bool expression,
            ConsoleColor fgColor,
            string value)
        {
            if (expression)
                Write(fgColor, value);
            else
                Console.Write(value);
        }


        public static void Write(
            bool expression,
            ConsoleColor fgColor,
            bool value)
        {
            if (expression)
                Write(fgColor, value);
            else
                Console.Write(value);
        }


        public static void Start(
            string title)
        {
            Console.WriteLine(title);
            BreakLineW();
            Console.WriteLine();
        }


        public static void Start()
        {
            Start(Assembly.GetCallingAssembly().GetName().Name);
        }


        public static void SectionStart(
            string title)
        {
            Console.WriteLine(title);
            BreakLine();
            Console.WriteLine();
        }


        public static void SectionEnd(
            string title = null)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(title))
                Console.WriteLine(title);
            BreakLine();
            Console.WriteLine();
        }


        public static void End()
        {
            Console.WriteLine();
            BreakLineW();
            Console.WriteLine(Resources.Common.Text_PressAnyKeyToExit);
            _ = Console.ReadKey();
        }


        public static void BreakLineW()
        {
            Console.WriteLine("=".MakeRepeats(80));
        }


        public static void BreakLine()
        {
            Console.WriteLine("-".MakeRepeats(80));
        }


        /* functions */


        public static ConsoleKeyInfo ReadKey()
        {
            InputClear();
            while (!Console.KeyAvailable) { }
            return Console.ReadKey(true);
        }

    }

}
