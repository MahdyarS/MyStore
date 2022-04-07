using MyStore.Common.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Common.Pagination
{
    public static class Pagination
    {
        public static PaginationResult<T> ToPaged<T>(this IEnumerable<T> source,int requestedPageIndex,int itemsInPageCount)
        {
            var count = (double)source.Count();
            var pageCount = (int)Math.Floor(count/ itemsInPageCount);
           
            if(pageCount == 0)
                return new PaginationResult<T>()
                {
                    Succeeded = false,
                    Message = "آیتمی جهت نمایش موجود نیست!",

                };

            else if (requestedPageIndex < 0 || requestedPageIndex > pageCount)
            {
                return new PaginationResult<T>()
                {
                    Succeeded = false,
                    Message = "صفحه درخواست شده خارج از محدوده است!",

                };
            }

            var result = new PaginationResult<T>()
            {
                Succeeded = true,
                RequestedPageList = source.Skip((requestedPageIndex - 1) * itemsInPageCount).Take(itemsInPageCount).ToList(),
                PagesCount = source.Count() / itemsInPageCount
            };

            if (requestedPageIndex == 1)
                result.PrevIsDiabled = true;
            if (requestedPageIndex == pageCount)
                result.NextIsDisabled = true;

            if (requestedPageIndex <= 5)
                result.FirstPageIndexToShow = 1;
            else
                result.FirstPageIndexToShow = requestedPageIndex - 4;

            if (pageCount - requestedPageIndex > 4)
                result.LastPageIndexToShow = requestedPageIndex + 4;
            else
                result.LastPageIndexToShow = pageCount;


            return result;

        }
        public class PaginationResult<T>
        {
            public List<T>? RequestedPageList { get; set; } = null;
            public int PagesCount { get; set; }
            public bool Succeeded { get; set; }
            public string Message { get; set; }
            public bool PrevIsDiabled { get; set; }
            public bool NextIsDisabled { get; set; }
            public int FirstPageIndexToShow { get; set; }
            public int LastPageIndexToShow { get; set; }


        }
    }
}
