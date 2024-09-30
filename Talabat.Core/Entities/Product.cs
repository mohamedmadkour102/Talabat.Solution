using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        public int BrandId { get; set; }
        public ProductBrand Brand { get; set; }
        // we make this nav property bec if i have product for ex and i need to know this product 
        // in which brand , and vice versa , lw ana 3nd el brand w 3ayz a shof eh el products 
        // elly tb3 el brand deh 

        public int CategoryId { get; set; }
        public ProductCategory Category { get; set; }
   

    }
}
