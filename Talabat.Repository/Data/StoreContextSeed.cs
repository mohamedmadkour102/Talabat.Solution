using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static  class StoreContextSeed
    {
        public static async Task SeedAsync (StoreContext _dbcontext)
        {
            var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/brands.json");
            var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
            if (Brands.Count() > 0)
            {
              if (_dbcontext.productBrands.Count() >0)
                {
                    foreach (var Brand in Brands)
                    {
                        // حل الاكسبشن بتاع الايدنتيتي بتاع الاي دي 
                        // ف عملت سيليكت للاسم بس 
                        // الحل التاني ان الفايل بتاعي اصلا امسح منه ال اي دي 
                        //Brands = Brands.Select(B => new ProductBrand()
                        //{
                        //    Name = B.Name
                        //}).ToList();
                        _dbcontext.Set<ProductBrand>().Add(Brand);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            var CategoriesData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/categories.json");
            var Categories = JsonSerializer.Deserialize<List<ProductCategory>>(CategoriesData);
            if (Categories.Count() > 0)
            {
                if (_dbcontext.productCategories.Count() == 0)
                {
                    foreach (var Category in Categories)
                    {
                        // حل الاكسبشن بتاع الايدنتيتي بتاع الاي دي 
                        // ف عملت سيليكت للاسم بس 
                        // الحل التاني ان الفايل بتاعي اصلا امسح منه ال اي دي 
                        //Brands = Brands.Select(B => new ProductBrand()
                        //{
                        //    Name = B.Name
                        //}).ToList();
                        _dbcontext.Set<ProductCategory>().Add(Category);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

            var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeeding/products.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            if (Products.Count() > 0)
            {
                if (_dbcontext.products.Count() == 0)
                {
                    foreach (var Product in Products)
                    {
                        // حل الاكسبشن بتاع الايدنتيتي بتاع الاي دي 
                        // ف عملت سيليكت للاسم بس 
                        // الحل التاني ان الفايل بتاعي اصلا امسح منه ال اي دي 
                        //Brands = Brands.Select(B => new ProductBrand()
                        //{
                        //    Name = B.Name
                        //}).ToList();
                        _dbcontext.Set<Product>().Add(Product);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }




        }


    }
}
