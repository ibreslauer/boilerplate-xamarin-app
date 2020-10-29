using System;
using System.Collections.Generic;

namespace Boilerplate.Common.Models
{
	public class PagedList<T>
	{
		public List<T> Items { get; private set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public bool HasPrevious { get; set; }
		public bool HasNext { get; set; }

		public PagedList(List<T> items, int count, int pageNumber, int pageSize)
		{
			Items = items;

			TotalCount = count;
			PageSize = pageSize;
			CurrentPage = pageNumber;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			HasPrevious = CurrentPage > 1;
			HasNext = CurrentPage < TotalPages;
		}

		/// <summary>
		/// Returns updated item collection and metadata after fetching further pages of items
		/// </summary>
        public PagedList<T> UpdateItems(PagedList<T> fetched)
        {
			Items.AddRange(fetched.Items);

			TotalCount = fetched.TotalCount;
			PageSize = fetched.PageSize;
			CurrentPage = fetched.CurrentPage;
			TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
			HasPrevious = CurrentPage > 1;
			HasNext = CurrentPage < TotalPages;

			return this;
        }
    }
}
