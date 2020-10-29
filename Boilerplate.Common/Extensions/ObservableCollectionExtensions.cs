using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Boilerplate.Common.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static int RemoveAll<T>(this ObservableCollection<T> col, Func<T, bool> condition)
        {
            var itemsToRemove = col.Where(condition).ToList();
            itemsToRemove.ForEach(i => col.Remove(i));        
            
            return itemsToRemove.Count;
        }
    }
}
