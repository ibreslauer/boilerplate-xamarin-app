using Boilerplate.Common.Constants;

namespace Boilerplate.Common.Models
{
	public abstract class PagingQueryParams
	{
		public int PageNumber { get; set; } = 1;

		private int _pageSize = 50;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = value > AppConstants.MAX_PAGE_SIZE ? AppConstants.MAX_PAGE_SIZE : value;
		}

		public int StartFrom => (PageNumber - 1) * PageSize;
	}
}
