using Oshxona.Domain.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshxona.Service.Extensions
{
    public static class CollectionsExtension
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @params)
        {
            int toSkip = (@params.PageIndex - 1) * @params.PageSize;
            int count = source.Count();

            if (toSkip >= count && count > 0)
            {
                toSkip = count - count % @params.PageSize;
            }

            return source.Skip(toSkip).Take(@params.PageSize);
        }
    }
}
