using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationsParameter
    {
        private const int maxPageSize = 10; 
        private const int DefaultPageSize = 5;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortingOptions sort { get; set; }
        public string? Search { get; set; }

        public int PageIndex { get; set; } = 1;
        private int _PageSize = DefaultPageSize;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value>maxPageSize?DefaultPageSize:value; }
        }

    }
}
