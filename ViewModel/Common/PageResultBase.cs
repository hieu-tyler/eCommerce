using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Common
{
    public class PageResultBase
    {
        public int PageIndex { get; set; } 
        public int PageSize { get; set; } 
        //public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageCount
        {
            get
            {
                if (TotalRecords == 0 || PageSize == 0)
                    return 0;
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }
    }
}
