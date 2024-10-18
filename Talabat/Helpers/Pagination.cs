﻿using Talabat.DTO_s;

namespace Talabat.Helpers
{
    public class Pagination<T>
    {
        private IReadOnlyList<ProductDto> _Data;

        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data , int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count; 
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
