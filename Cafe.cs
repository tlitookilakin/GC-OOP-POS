namespace PointOfSale;

public class Cafe
{
    // properties 
    public string MenuItem { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Count
    {
        get => _count;
        set => _count = Math.Max(0, value);
    }
    private int _count = 0;

    public static List<Cafe> menu = new List<Cafe>();
   
    // constructor 
    public Cafe(string _menuItem, string _category, string _description, decimal _price, int _count = 0)
    {
        MenuItem = _menuItem;
        Category = _category;
        Description = _description;
        Price = _price;
        Count = _count;
    }

    public Cafe(Cafe from, int count)
    {
        count = Math.Min(from.Count, count);
        Count = count;
        MenuItem = from.MenuItem;
        Category = from.Category;
        Description = from.Description;
        Price = from.Price;
        from.Count -= count;
    }
   
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
                if (columns.Length < 5)
                    continue;

                if (!decimal.TryParse(columns[3], out decimal price) || !int.TryParse(columns[4], out int count))
                    continue;

                menu.Add(new Cafe(columns[0], columns[1], columns[2], price, count));
            }
        }
    }

    public static void WriteToFile(string path)
    {
        using (StreamWriter writer = new(path))
        {
            foreach (var product in menu)
                writer.WriteLine($"{product.MenuItem}\t{product.Category}\t{product.Description}\t{product.Price}\t{product.Count}");
        }
    }

    public static void Restock(int count)
    {
        foreach (Cafe cafe in menu)
            cafe.Count = Math.Max(count, cafe.Count);
    }
}