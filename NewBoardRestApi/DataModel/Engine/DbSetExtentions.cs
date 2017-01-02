using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace NewBoardRestApi.DataModel.Engine
{
    public static class DbSetExtentions
    {
        public static T SingleResult<T>(this DbSet<T> source, Expression<Func<T, bool>> predicate)
            where T : class, new()
        {
            return SingleResult(source, predicate, "Cannot find item");
        }

        public static T SingleResult<T>(this DbSet<T> source, Expression<Func<T, bool>> predicate, string notFoundMessage)
           where T : class, new()
        {
            var item = source.FirstOrDefault(predicate);
            if (item == null)
            {
                item = new T();
            }

            return item;
        }
    }
}
