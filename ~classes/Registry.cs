using System.Collections;
using System.Resources;

namespace Ans.Net7.Common
{

	public delegate void RegistryItemEventHandler(object sender, RegistryItemEventArgs e);
	public class RegistryItemEventArgs : EventArgs
	{
		public RegistryItem Item { get; private set; }
		public RegistryItemEventArgs(RegistryItem item) { Item = item; }
	}



	public class Registry
		: List<RegistryItem>
	{

		public event RegistryItemEventHandler AddedItem;
		protected void OnAddedItem(RegistryItemEventArgs e) { AddedItem?.Invoke(this, e); }


		/* ctors */


		public Registry()
		{
		}


		public Registry(
			string source)
			: this()
		{
			FillFromString(source);
		}


		public Registry(
			params string[] source)
			: this()
		{
			FillFromString(source);
		}


		public Registry(
			string[] source,
			int firstIndex)
			: this()
		{
			foreach (var item1 in source)
				Add(firstIndex++, item1);
		}


		public Registry(
			IEnumerable items,
			string keyField,
			string fieldName)
			: this()
		{
			var type1 = items.GetType().GetGenericArguments()[0];
			foreach (var item1 in items)
			{
				var key1 = item1.GetPropertyValue(keyField, type1).ToString();
				var value1 = (item1.GetPropertyValue(fieldName, type1) ?? string.Empty).ToString();
				Add(key1, value1);
			}
		}


		public Registry(
			IEnumerable items,
			string keyField,
			string tempalte,
			params string[] fieldsNames)
			: this()
		{
			var type1 = items.GetType().GetGenericArguments()[0];
			foreach (var item1 in items)
			{
				var key1 = item1.GetPropertyValue(keyField, type1).ToString();
				var values1 = fieldsNames
					.Select(x => item1.GetPropertyValue(x, type1))
					.ToArray();
				Add(key1, string.Format(tempalte, values1));
			}
		}


		/* readonly properties */


		public IEnumerable<RegistryItem> ItemsOnly
			=> this.Where(x => !x.IsLabel);


		/* functions */


		/// <summary>
		/// Возвращает сериализацию реестра в виде строки
		/// "key1=value1;key2=value2;..." или
		/// "key1=level1:value1;key2=level2:value2;..."
		/// Символ ';' экранируется "\;"
		/// </summary>
		public override string ToString()
		{
			return string.Join(";", ItemsOnly.Select(x => x.ToString()));
		}


		/// <summary>
		/// Возвращает элемент по ключу
		/// </summary>
		public RegistryItem GetItem(
			string key)
		{
			return ItemsOnly
				.FirstOrDefault(x => x.Key.Equals(key));
		}


		/// <summary>
		/// Возвращает ключ по значению
		/// </summary>
		public string GetKey(
			string value)
		{
			var item1 = ItemsOnly
				.FirstOrDefault(x => x.Value.Equals(value));
			return item1?.Key;
		}


		/// <summary>
		/// Возвращает ключ элемента.
		/// Если элемент отсутствует, то создает его
		/// </summary>
		public string GetKeyForValue(
			string value,
			string newKey = null)
		{
			var key1 = GetKey(value);
			if (key1 != null)
				return key1;
			Add(newKey ?? Count.ToString(), value);
			return newKey;
		}


		/// <summary>
		/// Возвращает значение по ключу
		/// </summary>
		public string GetValue(
			string key)
		{
			return GetItem(key)?.Value;
		}


		/// <summary>
		/// Возвращает уровень в дереве по ключу
		/// </summary>
		public int GetLevel(
			string key)
		{
			return GetItem(key)?.Level ?? 0;
		}


		/// <summary>
		/// Возвращает уровень в дереве по ключу
		/// </summary>
		public int GetLevel(
			int key)
		{
			return GetLevel(key.ToString());
		}


		/// <summary>
		/// Возвращает уровень в дереве по ключу
		/// </summary>
		public int GetLevel(
			int? key)
		{
			return (key == null)
				? 0 : GetLevel(key.Value);
		}


		/// <summary>
		/// Возвращает значение если ключ содержит 'inclusion'
		/// </summary>
		public string GetValueForKeyInclusion(
			string keyInclusion)
		{
			return ItemsOnly
				.FirstOrDefault(x => x.Key.IndexOf(keyInclusion) > 0)?
				.Value;
		}


		/// <summary>
		/// Возвращает значение если 'expansion' содержит ключ
		/// </summary>
		public string GetValueForKeyExpansion(
			string keyExpansion)
		{
			return ItemsOnly
				.FirstOrDefault(x => keyExpansion.IndexOf(x.Key) > 0)?
				.Value;
		}


		/// <summary>
		/// Возвращает максимальную длину значения элемента реестра
		/// </summary>
		public int GetMaxWidth()
		{
			return ItemsOnly
				.Max(x => x.Value.Length);
		}


		/// <summary>
		/// Возвращает предложение по элементу ввода
		/// для отображение реестра
		/// </summary>
		public static RegistryControlsEnum GetProposeControl(
			int count,
			int width)
		{
			if (width < 21 && count < 20)
				return RegistryControlsEnum.RadioLine;
			return (count < 30)
				? RegistryControlsEnum.RadioBox
				: RegistryControlsEnum.Select;
		}


		/// <summary>
		/// Возвращает предложение по элементу ввода
		/// для отображение реестра
		/// </summary>
		public RegistryControlsEnum GetProposeControl()
		{
			return GetProposeControl(
				Count, GetMaxWidth());
		}


		/// <summary>
		/// Возвращает предожение по ширине элемента ввода
		/// для отображение реестра
		/// </summary>
		public static WidthsEnum GetProposeWidth(
			int width)
		{
			if (width < 11)
				return WidthsEnum.Small;
			if (width < 31)
				return WidthsEnum.Medium;
			if (width < 61)
				return WidthsEnum.Large;
			return WidthsEnum.Full;
		}


		/// <summary>
		/// Возвращает предожение по ширине элемента ввода
		/// для отображение реестра
		/// </summary>
		public WidthsEnum GetProposeWidth()
		{
			return GetProposeWidth(
				GetMaxWidth());
		}


		/* methods */


		public void Add(
			string key,
			string value,
			int level)
		{
			var item1 = new RegistryItem(
				key, value, level);
			Add(item1);
			OnAddedItem(new RegistryItemEventArgs(item1));
		}


		public void Add(
			int key,
			string value,
			int level)
		{
			Add(key.ToString(), value, level);
		}


		public void Add(
			string key,
			string value)
		{
			Add(key, value, 0);
		}


		public void Add(
			int key,
			string value)
		{
			Add(key.ToString(), value);
		}


		public void Add(
			string value)
		{
			Add(value, value);
		}


		public void AddNullItem(
			string emptyValue = null)
		{
			var item1 = new RegistryItem(
				emptyValue ?? string.Empty,
				Resources.Common.Text_EmptyItem);
			Insert(0, item1);
			OnAddedItem(new RegistryItemEventArgs(item1));
		}


		public void AddAllItems(
			string allValue = null)
		{
			var item1 = new RegistryItem(
				allValue ?? string.Empty,
				Resources.Common.Text_AllItems);
			Insert(0, item1);
			OnAddedItem(new RegistryItemEventArgs(item1));
		}


		public void AddLabel(
			string title)
		{
			var item1 = new RegistryItem(0, title) { IsLabel = true };
			Add(item1);
			OnAddedItem(new RegistryItemEventArgs(item1));
		}


		/// <summary>
		/// Сериализация реестра из строки
		/// </summary>
		/// <param name="source">
		/// Строки пар значений реестра в виде "key1=value1", "key2=value2",...
		/// или "key1=level1:value1", "key2=level2:value2",...
		/// "!" — добавляет элемент «нулевое значение».
		/// "*" — добавляет элемент «все значения».
		/// Символ ';' экранируется "\;"
		/// </param>
		public void FillFromString(
			params string[] source)
		{
			Clear();
			if (source?.Any() ?? false)
			{
				bool hasNull1 = false;
				bool hasAll1 = false;
				foreach (var item1 in source)
					if (!hasNull1 && item1.Equals("!"))
					{
						AddNullItem();
						hasNull1 = true;
					}
					else if (!hasAll1 && item1.Equals("*"))
					{
						AddAllItems();
						hasAll1 = true;
					}
					else
					{
						var item2 = new RegistryItem();
						item2.FillFromString(item1);
						Add(item2);
					}
			}
		}


		/// <summary>
		/// Сериализация реестра из строки
		/// </summary>
		/// <param name="source">
		/// Строка сериализации реестра в виде "key1=value1;key2=value2;..."
		/// или "key1=level1:value1;key2=level2:value2;..."
		/// </param>
		public void FillFromString(
			string source)
		{
			FillFromString(
				source.ScreeningReg().Split(';'));
		}


		public void Localization(
			string source)
		{
			if (!string.IsNullOrEmpty(source))
			{
				var reg1 = new Registry(source);
				foreach (var item1 in ItemsOnly)
				{
					string value1 = reg1.GetValue(item1.Key);
					if (!string.IsNullOrEmpty(value1))
						item1.Value = value1;
				}
			}
		}


		public void Localization(
			string key,
			ResourceManager manager,
			ResourceManager commonManager = null)
		{
			if (manager != null)
			{
				string s1 = manager.GetString(key);
				if (string.IsNullOrEmpty(s1) && commonManager != null)
					s1 = commonManager.GetString(key);
				Localization(s1);
			}
		}

	}



	public enum RegistryControlsEnum
	{
		Select,
		RadioLine,
		RadioBox,
	}



	public enum WidthsEnum
	{
		Full,
		ExtraLarge,
		Large,
		Medium,
		Small,
		ExtraSmall,
		Nothing
	}

}
