using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFiltertionForCountAsync : BaseSpecifications<Product>
    {
        public ProductWithFiltertionForCountAsync(ProductSpecParams Param) :
            base  (p =>(string.IsNullOrEmpty(Param.Search) || p.Name.ToLower().Contains(Param.Search)) // ضفتها هنا عشان يجيب ليا الكاونت بتاع الحاجه اللي هسيرش عنها بس 
               &&((!Param.BrandId.HasValue) || (p.BrandId == Param.BrandId))
               && (!Param.CategoryId.HasValue) || (p.CategoryId == Param.CategoryId))
        {}

    }
}
