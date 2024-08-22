namespace POS_Martina;

public class Cafe
{
   // properties 
   public string DrinkName { get; set; }
   public decimal Price { get; set; }

   public static List<Cafe> menu = new List<Cafe>();
   
   // constructor 
   public Cafe(string _drinkName, decimal _price)
   {
      DrinkName = _drinkName;
      Price = _price;
   }
   
   // methods 
  /* public int GetQuantity(int howmany)
   {
      
   }*/
   
}