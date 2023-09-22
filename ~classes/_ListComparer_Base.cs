namespace Ans.Net7.Common
{

    public class _ListComparer_Base<T>
    {

        /* ctors */


        /// <param name="currentList">Текущий список</param>
        /// <param name="newestList">Новый список</param>
        /// <param name="equalTest">Функция проверки на эквивалентность</param>
        /// <param name="differentTest">Функция проверки на различия</param>
        /// <param name="removedTest">Функция проверки на удаление</param>
        public _ListComparer_Base(
            IEnumerable<T> currentList,
            IEnumerable<T> newestList,
            Func<T, T, bool> equalTest,
            Func<T, T, bool> differentTest,
            Func<T, bool> removedTest)
        {
            CurrentList = currentList;
            NewestList = newestList;
            EqualTest = equalTest;
            DifferentTest = differentTest;
            RemovedTest = removedTest;
            CurrentListCount = CurrentList.Count();
            NewestListCount = NewestList.Count();
            ActualList = new List<T>();
            RemovedList = new List<T>();
            foreach (var item1 in CurrentList)
                if (RemovedTest(item1))
                    RemovedList.Add(item1);
                else
                    ActualList.Add(item1);
            ActualListCount = ActualList.Count;
            RemovedListCount = RemovedList.Count;
        }


        /* readonly properties */


        public Func<T, T, bool> EqualTest { get; private set; }
        public Func<T, T, bool> DifferentTest { get; private set; }
        public Func<T, bool> RemovedTest { get; private set; }
        public IEnumerable<T> CurrentList { get; private set; }
        public IEnumerable<T> NewestList { get; private set; }
        public List<T> ActualList { get; private set; }
        public List<T> RemovedList { get; private set; }
        public List<T> ForAddingList { get; private set; }
        public List<T> ForRestorationList { get; private set; }
        public List<T> ForDeletingList { get; private set; }
        public List<T> ForUpdatingList { get; private set; }
        public int CurrentListCount { get; private set; }
        public int NewestListCount { get; private set; }
        public int ActualListCount { get; private set; }
        public int RemovedListCount { get; private set; }
        public int ForAddingListCount { get; private set; }
        public int ForRestorationListCount { get; private set; }
        public int ForDeletingListCount { get; private set; }
        public int ForUpdatingListCount { get; private set; }


        /* functions */


        public T FindItem(
            T find)
        {
            return CurrentList.First(x => EqualTest(find, x));
        }


        public T FindActualItem(
            T find)
        {
            return ActualList.First(x => EqualTest(find, x));
        }


        public T FindRemovedItem(
            T find)
        {
            return RemovedList.First(x => EqualTest(find, x));
        }


        /* methods */


        public void CalcAddingOnly()
        {
            ForAddingList =
                (from item1 in NewestList
                 where !CurrentList.Any(x => EqualTest(x, item1))
                 select item1).ToList();
            ForAddingListCount = ForAddingList.Count;
            ForRestorationListCount = 0;
        }


        public void CalcAddingRestoration()
        {
            var missings1 =
                from item1 in NewestList
                where !ActualList.Any(x => EqualTest(x, item1))
                select item1;
            ForRestorationList = new List<T>();
            ForAddingList = new List<T>();
            foreach (var item1 in missings1)
                if (RemovedList.Any(x => EqualTest(x, item1)))
                    ForRestorationList.Add(item1);
                else
                    ForAddingList.Add(item1);
            ForAddingListCount = ForAddingList.Count;
            ForRestorationListCount = ForRestorationList.Count;
        }


        public void CalcUpdating()
        {
            ForUpdatingList =
                (from item1 in ActualList
                 from item2 in NewestList
                 where EqualTest(item1, item2) && DifferentTest(item1, item2)
                 select item2).ToList();
            ForUpdatingListCount = ForUpdatingList.Count;
        }


        public void CalcDeleting()
        {
            ForDeletingList =
                (from item1 in ActualList
                 where !NewestList.Any(x => EqualTest(x, item1))
                 select item1).ToList();
            ForDeletingListCount = ForDeletingList.Count;
        }


        public void Log()
        {
            Console.WriteLine("  Newest: {0}", NewestListCount);
            Console.WriteLine("  CurrentActual: {0}", ActualListCount);
            CalcAddingRestoration();
            Console.WriteLine("  Adding: {0}", ForAddingListCount);
            Console.WriteLine("  Restoration: {0}", ForRestorationListCount);
            CalcUpdating();
            Console.WriteLine("  Updating: {0}", ForUpdatingListCount);
            CalcDeleting();
            Console.WriteLine("  Deleting: {0}", ForDeletingListCount);
        }

    }

}
