﻿namespace Ans.Net7.Common
{

	public static partial class _Consts
	{

		public static string GET_RANDOM_SAMPLE_RU()
			=> SAMPLES_RU[SuppRandom.Next(0, SAMPLES_RU.Length - 1)];


		public static string GET_RANDOM_SAMPLE_SMALL_RU()
			=> SAMPLES_SMALL_RU[
				SuppRandom.Next(0, SAMPLES_SMALL_RU.Length - 1)];


		public static string GET_RANDOM_SAMPLE_SMALLER_RU()
			=> SAMPLES_SMALLER_RU[
				SuppRandom.Next(0, SAMPLES_SMALLER_RU.Length - 1)];


		public static readonly string[] SAMPLES_RU = new string[]
		{
			"Эй, цирюльникъ, ёжик выстриги, да щетину ряхи сбрей, феном вошь за печь гони",
			"Экс-граф? Плюш изъят. Бьём чуждый цен хвощ",
			"Эй, жлоб! Где туз? Прячь юных съёмщиц в шкаф",
			"Любя, съешь щипцы, — вздохнёт мэр, — кайф жгуч",
			"В чащах юга жил-был цитрус… — да, но фальшивый экземпляръ",
			"Южно-эфиопский грач увёл мышь за хобот на съезд ящериц",
			"Аэрофотосъёмка ландшафта уже выявила земли богачей и процветающих крестьян",
			"Шифровальщица попросту забыла ряд ключевых множителей и тэгов",
			"Съешь ещё этих мягких французских булок, да выпей [же] чаю. 1234567890",
			"Щипцами брюки разлохмачу, Гребёнкой волосы взъерошу. Эффектно ожидать удачу. До самой смерти я не брошу",
			"Подъём с затонувшего эсминца легко бьющейся древнегреческой амфоры сопряжён с техническими трудностями",
			"Завершён ежегодный съезд эрудированных школьников, мечтающих глубоко проникнуть в тайны физических явлений и химических реакций",
			"Всё ускоряющаяся эволюция компьютерных технологий предъявила жёсткие требования к производителям как собственно вычислительной техники, так и периферийных устройств",
			"Шалящий фавн прикинул объём горячих звезд этих вьюжных царств",
			"Эх, жирафы честно в цель шагают, да щук объять за память ёлкой",
			"Объявляю: туфли у камина, где этот хищный ёж цаплю задел",
			"Лингвисты в ужасе: фиг выговоришь этюд: «подъём челябинский, запах щец»",
			"Съел бы ёж лимонный пьезокварц, где электрическая юла яшму с туфом похищает",
			"Официально заявляю читающим: даёшь подъем операции Ы! Хуже с ёлкой бог экспериментирует",
			"Эти ящерицы чешут вперёд за ключом, но багаж в сейфах, поди подъедь",
			"Бегом марш! У месторождения кварцующихся фей без слёз хочется электрическую пыль",
			"Хрюкнул ёж «Тыща», а ведь село Фершампенуаз — это центр Нагайбакского района Челябинской области",
			"Эх, взъярюсь, толкну флегматика: «Дал бы щец жарчайших, Пётр!»",
			"Здесь фабула объять не может всех эмоций — шепелявый скороход в юбке тащит горячий мёд",
			"Художник-эксперт с компьютером всего лишь яйца в объёмный низкий ящик чохом фасовал",
			"Юный директор целиком сжевал весь объём продукции фундука (товара дефицитного и деликатесного), идя энергично через хрустящий камыш",
			"Мюзикл-буфф «Огнедышащий простужается ночью» (в 12345 сценах и 67890 эпизодах)",
			"Обдав его удушающей пылью, множество ярких фаэтонов исчезло из цирка",
			"Безмозглый широковещательный цифровой передатчик сужающихся экспонент",
			"Однажды съев фейхоа, я, как зацикленный, ностальгирую всё чаще и больше по этому чуду",
			"Вопрос футбольных энциклопедий замещая чушью: эй, где съеден ёж",
			"Борец за идею Чучхэ выступил с гиком, шумом, жаром и фырканьем на съезде — и в ящик",
			"Твёрдый, как ъ, но и мягкий, словно ь, юноша из Бухары ищет фемину-москвичку для просмотра цветного экрана жизни",
			"БУКВОПЕЧАТАЮЩЕЙ СВЯЗИ НУЖНЫ ХОРОШИЕ Э/МАГНИТНЫЕ РЕЛЕ. ДАТЬ ЦИФРЫ (1234567890+= .?-)",
			"Пиши: зять съел яйцо, ещё чан брюквы… эх! Ждем фигу",
			"Флегматичная эта верблюдица жует у подъезда засыхающий горький шиповник",
			"Вступив в бой с шипящими змеями — эфой и гадюкой, — маленький, цепкий, храбрый ёж съел их",
			"Подъехал шофёр на рефрижераторе грузить яйца для обучающихся элитных медиков",
			"Широкая электрификация южных губерний даст мощный толчок подъёму сельского хозяйства",
			"Государев указ: душегубцев да шваль всякую высечь, да калёным железом по щекам этих физиономий съездить",
			"§ Проверка шрифта — «№–0123456789». 123−456+789=456 QWERTYUIOP{} ASDFGHJKL: | ZXCVBNM<>? ~!@#$%^&*()_+ qwertyuiop[] asdfghjkl;'\\ zxcvbnm,./ `1234567890-= ЙЦУКЕНГШЩЗХЪ ФЫВАПРОЛДЖЭ/ ЯЧСМИТЬБЮ, Ё! №;%:?*()_+ йцукенгшщзхъ фывапролджэ\\ ячсмитьбю. ё1234567890-="
		};


		public static readonly string[] SAMPLES_SMALL_RU = new string[]
		{
			"Эй, цирюльникъ, ёжик выстриги",
			"Экс-граф? Плюш изъят",
			"Эй, жлоб! Где туз",
			"Любя, съешь щипцы, — вздохнёт мэр",
			"В чащах юга жил-был цитрус",
			"Южно-эфиопский грач увёл мышь",
			"Аэрофотосъёмка ландшафта уже выявила",
			"Шифровальщица попросту забыла",
			"Съешь ещё этих мягких французских булок",
			"Щипцами брюки разлохмачу",
			"Подъём с затонувшего эсминца",
			"Завершён ежегодный съезд эрудированных",
			"Всё ускоряющаяся эволюция",
			"Шалящий фавн прикинул объём",
			"Эх, жирафы честно в цель шагают",
			"Объявляю: туфли у камина",
			"Лингвисты в ужасе: фиг выговоришь",
			"Съел бы ёж лимонный пьезокварц",
			"Официально заявляю читающим",
			"Эти ящерицы чешут вперёд за ключом",
			"Бегом марш",
			"Хрюкнул ёж «Тыща»",
			"Эх, взъярюсь, толкну флегматика",
			"Здесь фабула объять не может всех эмоций",
			"Художник-эксперт с компьютером",
			"Юный директор целиком сжевал",
			"Мюзикл-буфф «Огнедышащий простужается ночью»",
			"Обдав его удушающей пылью",
			"Безмозглый широковещательный",
			"Однажды съев фейхоа",
			"Вопрос футбольных энциклопедий",
			"Борец за идею Чучхэ выступил с гиком",
			"Твёрдый, как ъ, но и мягкий, словно ь",
			"Блеф разъедает ум",
			"Пиши: зять съел яйцо",
			"Флегматичная эта верблюдица",
			"Вступив в бой с шипящими змеями",
			"Подъехал шофёр на рефрижераторе",
			"Широкая электрификация южных губерний",
			"Государев указ"
		};


		public static readonly string[] SAMPLES_SMALLER_RU = new string[]
		{
			"Ёжик выстриги",
			"Плюш изъят",
			"Прячь юных съёмщиц",
			"Любя, съешь щипцы",
			"Фальшивый экземпляръ",
			"Увёл мышь за хобот",
			"Земли богачей",
			"Шифровальщица попросту",
			"Французских булок",
			"Эффектно ожидать удачу",
			"С техническими трудностями",
			"Явлений и химических реакций",
			"Периферийных устройств",
			"Этих вьюжных царств",
			"За память ёлкой",
			"Ёж цаплю задел",
			"Челябинский, запах щец»",
			"С туфом похищает",
			"Хуже с ёлкой бог",
			"В сейфах, поди подъедь",
			"Электрическую пыль",
			"Челябинской области",
			"Дал бы щец жарчайших, Пётр",
			"Тащит горячий мёд",
			"Ящик чохом фасовал",
			"Через хрустящий камыш",
			"Исчезло из цирка",
			"Сужающихся экспонент",
			"Больше по этому чуду",
			"Где съеден ёж",
			"И в ящик",
			"Цветного экрана жизни",
			"Ждем фигу",
			"Горький шиповник",
			"Храбрый ёж съел их",
			"Элитных медиков",
			"Сельского хозяйства",
			"Этих физиономий съездить"
		};

	}

}
