using System.Text;

namespace Ans.Net7.Common
{

    public class PaginationHelper
	{

		/* ctors */


		public PaginationHelper(
			int itemsOnPage,
			int totalItems,
			int offset = 4)
		{
			if (itemsOnPage < 1)
				itemsOnPage = 1;
			if (totalItems < 0)
				totalItems = 0;
			Offset = SuppMath.GetRestrict(offset, 1, 9);
			ItemsOnPage = itemsOnPage;
			TotalItems = totalItems;
			TotalPages = (int)Math.Ceiling((double)TotalItems / ItemsOnPage);
		}


		/* properties */


		/// <summary>
		/// Возвращает и устанавливает текущую страницу
		/// </summary>
		public int CurrentPage
		{
			get => _currentPage;
			set
			{
				_currentPage = value;
				if (_currentPage < 1)
				{
					_currentPage = 1;
					NotValidIndex = true;
				}
				if (TotalPages > 0 && _currentPage > TotalPages)
				{
					_currentPage = TotalPages;
					NotValidIndex = true;
				}
				SkipItems = ItemsOnPage * (_currentPage - 1);
				if (_currentPage < Offset + 1)
				{
					StartPage = 1;
					EndPage = Offset * 2 + 1;
				}
				else if (_currentPage > TotalPages - Offset - 1)
				{
					StartPage = TotalPages - Offset * 2;
					EndPage = TotalPages;
				}
				else
				{
					StartPage = _currentPage - Offset;
					EndPage = _currentPage + Offset;
				}
				if (StartPage < 1)
					StartPage = 1;
				if (EndPage > TotalPages)
					EndPage = TotalPages;
				ActiveFirstPage = (_currentPage == 1);
				ActiveLastPage = (_currentPage == TotalPages);
				HasItemsBefore = (StartPage > 1);
				HasItemsAfter = (EndPage < TotalPages);
				PreviousPage = CurrentPage - (2 * Offset);
				if (PreviousPage < 1)
					PreviousPage = 1;
				NextPage = CurrentPage + (2 * Offset);
				if (NextPage > TotalPages)
					NextPage = TotalPages;
			}
		}
		private int _currentPage = 0;


		/* readonly properties */


		public int ItemsOnPage { get; private set; }
		public int Offset { get; private set; }
		public int TotalItems { get; private set; }
		public int TotalPages { get; private set; }
		public int SkipItems { get; private set; }
		public int PreviousPage { get; private set; }
		public int NextPage { get; private set; }
		public int StartPage { get; private set; }
		public int EndPage { get; private set; }
		public bool ActiveFirstPage { get; private set; }
		public bool ActiveLastPage { get; private set; }
		public bool HasItemsBefore { get; private set; }
		public bool HasItemsAfter { get; private set; }
		public bool NotValidIndex { get; private set; }


		/* functions */


		public override string ToString()
		{
			var sb1 = new StringBuilder();
			sb1.Append($"{ItemsOnPage}/{TotalItems} {CurrentPage}/{TotalPages} ");
			if (HasItemsBefore)
				sb1.Append($"🡠{PreviousPage} ");
			sb1.Append($"[{StartPage}-{CurrentPage}-{EndPage}]");
			if (HasItemsAfter)
				sb1.Append($" {NextPage}🡢");
			sb1.Append($" AF:{ActiveFirstPage} AL:{ActiveLastPage} S:{SkipItems}");
			return sb1.ToString();
		}

	}

}