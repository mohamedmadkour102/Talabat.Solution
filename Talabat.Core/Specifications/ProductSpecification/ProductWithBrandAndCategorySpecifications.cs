using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.ProductSpecification
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecifications<Product>
    {
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams Param)
            : base  (p => (string.IsNullOrEmpty(Param.Search) ||  p.Name.ToLower().Contains( Param.Search))
               &&   ((!Param.BrandId.HasValue)||(p.BrandId == Param.BrandId))
               && (!Param.CategoryId.HasValue) || (p.CategoryId == Param.CategoryId))
            

        {
            Includes.Add(P=>P.Brand);
            Includes.Add(P=>P.Category);    
            if (!string.IsNullOrEmpty(Param.Sort))
            {
                switch (Param.Sort) 
                {
                    case "PriceAsc":
                        SetOrderBy(P=>P.Price);
                        break;
                    case "PriceDesc":
                        SetOrderByDescending(P=>P.Price);
                        break;
                    default:
                        SetOrderBy(P => P.Name);
                        break;
                }
            }

            ApplyPagination(Param.PageSize * (Param.PageIndex -1) , Param.PageSize);
        }

     
        public ProductWithBrandAndCategorySpecifications(int id ):base(P=>P.Id == id)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }

        
    }
}
