using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }
       // public ICollection<Product> products { get; set; } = new HashSet<Product>();
       // كومنتد عشان انا في البيزنس عندي معنديش احتياج اني اوصل للبروداكت من خلال البراند بتاعه 
       // وبالنسبة للريليشن بينهم هظبطها بالفلوينت 
       // وهتتظبط في كلاس الكونفيجريشن بتاع البروداكت لانه الناف بروبرتي بتاعت الوان عنده 

    }
}
