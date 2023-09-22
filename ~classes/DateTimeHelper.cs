namespace Ans.Net7.Common
{

    /// <summary>
    /// Хелпер даты и времени
    /// </summary>
    public class DateTimeHelper
    {

        /* ctors */


        public DateTimeHelper()
        {
            Current = DateTime.Now;
            CurrentYearBegin = new DateTime(Current.Year, 1, 1);
            NextYearBegin = CurrentYearBegin.AddYears(1);
            Today = Current.Date;
            Yesterday = Today.AddDays(-1);
            Tomorrow = Today.AddDays(1);
            TomorrowAfter = Today.AddDays(2);
        }


        /* readonly properties */


        /// <summary>
        /// Текущая дата и время
        /// </summary>
        public DateTime Current { get; private set; }

        /// <summary>
        /// Дата начала текущего года
        /// </summary>
        public DateTime CurrentYearBegin { get; private set; }

        /// <summary>
        /// Дата начала следующего года
        /// </summary>
        public DateTime NextYearBegin { get; private set; }

        /// <summary>
        /// Дата сегодня
        /// </summary>
        public DateTime Today { get; private set; }

        /// <summary>
        /// Дата вчера
        /// </summary>
        public DateTime Yesterday { get; private set; }

        /// <summary>
        /// Дата завтра
        /// </summary>
        public DateTime Tomorrow { get; private set; }

        /// <summary>
        /// Дата послезавтра
        /// </summary>
        public DateTime TomorrowAfter { get; private set; }


        /* functions */


        /// <summary>
        /// Возвращает для указанной даты значение "прошло", "сейчас" или "будет".
        /// </summary>
        public TensesEnum GetDayTenses(
            DateTime datetime)
        {
            var date1 = datetime.Date;
            if (date1 < Today)
                return TensesEnum.Past;
            if (date1 > Today)
                return TensesEnum.Future;
            return TensesEnum.Present;
        }


        /// <summary>
        /// Возвращает дату (и время) события (для блога)
        ///	- будет в будущих годах
        ///		d MMMM yyyy г.[ в H:mmmm] / MMMM d, yyyy[ at h:mmmm]
        ///	- будет в этом году
        ///		d MMMM[ в H:mmmm] / MMMM d[ at h:mmmm]
        ///	- завтра
        ///		завтра[ в H:mmmm] / Tomorrow[ at h:mmmm]
        ///	- сегодня
        ///		сегодня[ в H:mmmm] / Today[ at h:mmmm]
        ///	- вчера
        ///		вчера[ в H:mmmm] / Yesterday[ at H:mmmm]
        ///	- было в этом году
        ///		d MMMM[ в H:mmmm] / MMMM d[ at h:mmmm]
        ///	- было в прошлые годы
        ///		d MMMM yyyy г.[ в H:mmmm] / MMMM d, yyyy[ at h:mmmm]
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="addTime">Добавить время</param>
        /// <param name="useYesterdayTodayTomorrow">Использовать «вчера», «сегодня» и «завтра»</param>
        public string GetPassed(
            DateTime datetime,
            bool addTime,
            bool useYesterdayTodayTomorrow = true)
        {
            if (!datetime.HasTime())
                addTime = false;
            // будет в будущих годах
            if (datetime >= NextYearBegin)
                return datetime.ToString(addTime
                    ? Resources.Common.Format_DateTime_ForFull
                    : Resources.Common.Format_Date_ForFull);
            // будет в этом году
            if (useYesterdayTodayTomorrow && datetime >= TomorrowAfter)
                return datetime.ToString(addTime
                    ? Resources.Common.Format_DateTime_ForCurrentYear
                    : Resources.Common.Format_Date_ForCurrentYear);
            // завтра
            if (useYesterdayTodayTomorrow && datetime >= Tomorrow)
                return datetime.ToString(addTime
                    ? Resources.Common.Format_DateTime_ForTomorrow
                    : Resources.Common.Format_Date_ForTomorrow);
            // сегодня
            if (useYesterdayTodayTomorrow && datetime >= Today)
                return datetime.ToString(addTime
                    ? Resources.Common.Format_DateTime_ForToday
                    : Resources.Common.Format_Date_ForToday);
            // вчера
            if (useYesterdayTodayTomorrow && datetime >= Yesterday)
                return datetime.ToString(addTime
                    ? Resources.Common.Format_DateTime_ForYesterday
                    : Resources.Common.Format_Date_ForYesterday);
            // было в этом году
            if (datetime >= CurrentYearBegin)
                return datetime.ToString(addTime
                    ? Resources.Common.Format_DateTime_ForCurrentYear
                    : Resources.Common.Format_Date_ForCurrentYear);
            // было в прошлые годы
            return datetime.ToString(addTime
                ? Resources.Common.Format_DateTime_ForFull
                : Resources.Common.Format_Date_ForFull);
        }


        /// <summary>
        /// Возвращает дату (и время) прошедшего события (для блогов)
        /// </summary>
        public string GetPassed(
            DateTime? datetime,
            bool hasTime,
            bool allowYesterdayTodayTomorrow = true)
        {
            if (datetime == null)
                return Resources.Common.Text_Never;
            return GetPassed(datetime.Value, hasTime, allowYesterdayTodayTomorrow);
        }


        /// <summary>
        /// Возвращает диапазон дат (для блога)
        /// - одна или равные даты
        /// - разные года
        /// - разные месяцы года
        /// - разные дни месяца
        /// </summary>
        public string GetSpan(
            DateTime date1,
            DateTime? date2,
            bool hideCurrentYear = true,
            bool allowYesterdayTodayTomorrow = false)
        {
            // одна или равные даты
            if (date2 == null || date1.Date.Equals(date2.Value.Date))
                return (hideCurrentYear)
                    ? GetPassed(date1, false, allowYesterdayTodayTomorrow)
                    : date1.ToString(Resources.Common.Format_DateRange_Full);
            DateTime d2 = date2.Value;
            if (date1 > d2)
                (date1, d2) = (d2, date1);
            if (date1.Year != d2.Year)
            {
                // разные года
                return string.Format("{0} – {1}",
                    date1.ToString(Resources.Common.Format_DateRange_Full),
                    d2.ToString(Resources.Common.Format_DateRange_Full));
            }
            if (date1.Month != d2.Month)
            {
                // разные месяцы года
                return string.Format("{0} – {1}",
                    date1.ToString(Resources.Common.Format_DateRange_WithinYear),
                    (hideCurrentYear)
                        ? GetPassed(d2, false, allowYesterdayTodayTomorrow)
                        : d2.ToString(Resources.Common.Format_DateRange_Full));
            }
            // разные дни месяца
            return string.Format("{0}–{1}",
                date1.ToString(Resources.Common.Format_DateRange_WithinMonth),
                (hideCurrentYear)
                    ? GetPassed(d2, false, allowYesterdayTodayTomorrow)
                    : d2.ToString(Resources.Common.Format_DateRange_Full));
        }

    }



    public enum TensesEnum
    {
        /// <summary>
        /// Прошло
        /// </summary>
        Past,

        /// <summary>
        /// Сейчас
        /// </summary>
        Present,

        /// <summary>
        /// Будет
        /// </summary>
        Future
    }

}
