﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class BasketItem
    {
        // مش هيورث من البيز انتيتي لانه مش هيكلم الكونتكست بتاع الداتا بيز لانه اين ميموري 
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }


    }
}
