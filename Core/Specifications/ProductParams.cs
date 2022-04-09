using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductParams
    {
        private const int MaxPageSize = 50;
        public string Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private int pageSize = 6;
        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string Search { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
    }
}