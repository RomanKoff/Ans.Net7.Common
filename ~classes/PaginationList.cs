namespace Ans.Net7.Common
{

	public class PaginationList<T>
		: List<T>
	{

		/* ctor */


		public PaginationList(
			IEnumerable<T> items,
			int totalItems,
			int currentPage,
			int itemsOnPage)
		{
			Pagination = new(itemsOnPage, totalItems, currentPage);
			AddRange(items);
		}


		/* readonly properties */


		public PaginationHelper Pagination { get; }


		/* functions */


		public static PaginationList<T> Create(
			IQueryable<T> source,
			int page,
			int itemsOnPage)
		{
			var count1 = source.Count();
			var items1 = source
				.Skip((page - 1) * itemsOnPage)
				.Take(itemsOnPage)
					.AsEnumerable();
			return new PaginationList<T>(
				items1, count1, page, itemsOnPage);
		}

	}



	/*

	public PagedList<Post> GetPosts(
		int page = 1,
		itemsOnPage = 20) 
	{
		var collection = ApplicationDbContext.Posts
			.OrderByDescending(x = > x.DateCreated)
			.AsQueryable();
		var posts = PagedList<Post>.Create(
			collection, page, itemsOnPage);
		return posts;
	}


	[HttpGet()]
	public async Task <IActionResult> GetPosts(
		[FromQuery] ResourceParameters resourceParameters) 
	{
		var postsPagedList = await _unitOfWork.Posts.GetPosts(resourceParameters);
		var posts = _mapper.Map<IEnumerable<PostViewModel>>(postsPagedList);
		var pageMetaData = _mapper.Map<PageMetaData>(postsPagedList);

		var response = new PostListResult() {
    		Posts = posts,
    		PageMetaData = pageMetaData
		};
		return Ok(response);
	}

	*/

}
