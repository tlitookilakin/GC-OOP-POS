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
   
    public static void ReadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            WriteToFile(path);
            return;
        }

        menu.Clear();

        using (StreamReader reader = new(path))
        {
            while(reader.ReadLine() is string line)
            {
                string[] columns = line.Split('\t');
                if (columns.Length < 4)
                    continue;

                if (!decimal.TryParse(columns[3], out decimal price))
                    continue;

                menu.Add(new Cafe(columns[0], columns[1], columns[2], price));
            }
        }
    }

    public static void WriteToFile(string path)
    {
        using (StreamWriter writer = new(path))
        {
            foreach (var product in menu)
                writer.WriteLine($"{product.MenuItem}\t{product.Category}\t{product.Description}\t{product.Price}");
        }
    }
}