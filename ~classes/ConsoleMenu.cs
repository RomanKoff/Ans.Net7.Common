namespace Ans.Net7.Common
{

    public class ConsoleMenu
    {

        /* ctor */


        public ConsoleMenu(
            string title)
        {
            Items = new List<ConsoleMenuItem>();
            Title = title;
            UseExit = false;
        }


        /* properties */


        public string Title { get; set; }
        public bool UseExit { get; set; }


        /* readonly properties */


        public List<ConsoleMenuItem> Items { get; private set; }


        /* methods */


        public void Add(
            ConsoleMenuItem item)
        {
            Items.Add(item);
        }


        public void Release()
        {
            bool pressEscape = false;
            do
            {
                Console.WriteLine();
                if (!string.IsNullOrEmpty(Title))
                    Console.WriteLine($"{Title}:");
                foreach (var item1 in Items)
                    Console.WriteLine($"[{(char)item1.Key}] — {item1.Title}");
                Console.WriteLine(Resources.Common.Text_PressEscToExit);
                Console.WriteLine();
                do
                {
                    UseExit = false;
                    var key1 = SuppConsole.ReadKey();
                    foreach (var item1 in Items)
                        if (item1.Key == key1.Key)
                        {
                            UseExit = true;
                            item1.Action();
                        }
                    pressEscape = key1.Key == ConsoleKey.Escape;
                } while (!UseExit && !pressEscape);
            } while (!pressEscape);
        }

    }



    public class ConsoleMenuItem
    {
        public ConsoleKey Key { get; private set; }
        public string Title { get; private set; }
        public Action Action { get; private set; }

        public ConsoleMenuItem(
            ConsoleKey key,
            string title,
            Action action)
        {
            Key = key;
            Title = title;
            Action = action;
        }
    }

}
