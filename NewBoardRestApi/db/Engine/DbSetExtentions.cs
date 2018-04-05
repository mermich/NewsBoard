using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NewBoardRestApi.DataModel.Engine
{
    internal static class DbSetExtentions
    {

        public static IEnumerable<T> GetItemsToRemove<T, U>(this DbSet<T> itemsToFilter, IEnumerable<U> itemstoKeep, Func<T, U> identifierSelector)
            where T : class
            where U : IEquatable<U>
        {
            var result = new List<T>();

            foreach (var item in itemsToFilter)
            {
                if (!itemstoKeep.Contains(identifierSelector(item)))
                {
                    result.Add(item);
                }
            }

            return result;
        }


        public static void RemoveItemsFromList<T>(this DbSet<T> listToPurge, IEnumerable<T> itemsToRemove)
            where T : class
        {
            listToPurge.RemoveRange(itemsToRemove);
        }

        public static void AddItemsThatDoesnExists<T, U>(this DbSet<T> existingItems, IEnumerable<U> itemstoAdd, Func<U, T> convertFunction)
            where T : class
            where U : IEquatable<U>
        {

            foreach (var item in itemstoAdd)
            {
                if (!existingItems.Any(e => convertFunction(item).Equals(e)))
                {
                    var converted = convertFunction(item);
                    existingItems.Add(converted);
                }
            }
        }

        public static void MergeLists<T, U>(this DbSet<T> listToMerge, Func<T, U> existingItemSelector, IEnumerable<U> itemsEdited, Func<U, T> convertFunction)
            where T : class
            where U : IEquatable<U>
        {
            var itemsToremove = GetItemsToRemove(listToMerge, itemsEdited, existingItemSelector);
            listToMerge.RemoveRange(itemsToremove);

            AddItemsThatDoesnExists(listToMerge, itemsEdited, convertFunction);

        }
    }
}
