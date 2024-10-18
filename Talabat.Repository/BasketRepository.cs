using Microsoft.Identity.Client;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        // محتاج اطلب هنا من ال سي ال ار انه ي انجيكت ليا اوبجكت من نوع الريديس كونكشن في الكونستراكتور
        // بس لازم الاول اروح اعمل اينستول للريديس باكيدج 
        // هنزلها في الكور وكده كده الريبو واخد منه ريفرنس وبرضه عشان هحتاجها في السيرفيس وانا بعمل كاشينج 
        // IConnectionMuliplexer وهاروح اعملها ريجيستر 

        public BasketRepository(IConnectionMultiplexer Redis)
        {
            
            _database = Redis.GetDatabase();

            // كده انا بال اوبجكت اللي اسمه داتابيز ده انا اقدر اعمل اكسيس للريديس داتا بيز بكل الميثودز بقى 
            // اللي هي موفرهالي زي الاوبجكت بتاع الدي بي كونتكست كده 

        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
           
        }

        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
           /*السترينج جيت اسينك ديه بتاخد مني الاي ديه اللي هو الكي وتروح تجيب الفاليو بقى زي ما  قولنا 
            * انه الستراكشر بتاع الريديس عبارة عن ديكشنري */
           var Basket = await _database.StringGetAsync(BasketId);
           return Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(Basket);
           // احنا كنا قايلين انه الفاليو بتاعت الديكشنري عبارة عن اراي اوف اوبجكت "جيسون ولما بتعمله
           // ريتيرين بيرجع في شكل جيسون برضه بس هنا الفانكشن بترجعلي حاجه بشكل الكاستومر باسكيت 
           // لذالك لازم اعمل ديسيريالايز 
           // هل اصلا ينفع يكون عندي اي دي بتاع باسكيت ل باسكيت مش موجود ؟ اه لو هو اكسبايرد 

        }

        
        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            // هنا لاني عايز اضيفه في الريديس ف لازم احوله لجيسون زي فوق لما حولته من جيسون للعكس 
            // الفانكشن ديه شغاله ابديت وادد عشان هي بتروح تتشيك على الاي دي لو لاقيته بتعمل ابديت
            // لو ملقيتوش بتعمل ادد 


            var CreateOrUpdateBasket = await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket) , TimeSpan.FromDays(30));
            if (CreateOrUpdateBasket is false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
