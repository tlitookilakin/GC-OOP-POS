namespace PointOfSale;

public class Cafe
{
   // properties 
   public string MenuItem { get; set; }
   public string Category { get; set; }
   public string Description { get; set; }
   public decimal Price { get; set; }

   public static List<Cafe> menu = new List<Cafe>();
   
   // constructor 
   public Cafe(string _menuItem, string _category, string _description, decimal _price)
   {
      MenuItem = _menuItem;
      Category = _category;
      Description = _description;
      Price = _price;
   }
   
   // methods 
  /* public int GetQuantity(int howmany)
   {
      
   }*/
   
}