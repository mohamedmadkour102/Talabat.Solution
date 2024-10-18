namespace Talabat.Core.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Street { get; set; }
       
        public string City { get; set; }
        public string Country { get; set; }   

        // لان كل ادريس لازم يكون تبع يوزر ولكن مش كل يوزر لازم يكون ليه ادريس الا لما يعمل اوردر 
        // ف اخدنا الكي بتاع الاوبشنال حطيناه في المنداتوري 
        public string AppUserId { get; set; }

        





    }
}