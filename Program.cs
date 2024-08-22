namespace PointOfSale;

class Program
{
    static void Main(string[] args)
    {
        Cafe.menu.Add(new Cafe("Iced latte", "drink", "coffee", 7.99M));
        Cafe.menu.Add(new Cafe("Black coffee", "drink", "coffee", 2.99M));
        Cafe.menu.Add(new Cafe("Water", "drink", "water", 3.99M));
        Cafe.menu.Add(new Cafe("Danish", "food", "bread product", 6.99M));
        
        Console.WriteLine("Welcome to the Three Musketeers' Coffee Shop:");
        foreach (var m in Cafe.menu)
        {
            Console.WriteLine($"{m.MenuItem} {m.Price}");
        }
        Console.WriteLine("What can I get for you today?");
        
        Console.WriteLine("How many do you want?");
        Validator.GetPositiveInputInt();

    }
    
    static void getPaymentType()
    { 
        string paymentType;
        while (true)
        {
            Console.WriteLine("Please select payment type:");
            Console.WriteLine("Enter 'cash',  'credit', or 'check'.");
            paymentType = Console.ReadLine().ToLower();
            if (paymentType == "cash")
            {
                payCash();
                break;
            }
            else if (paymentType == "credit")
            {

            }
            else if (paymentType == "check")
            {

            }
        }
    }

    static void payCash() 
    {
        double amountTendered;
        while (true)
        {
            Console.WriteLine($"Your total is {totalCost}.");
            Console.WriteLine("How much cash will you pay with?");
            try
            {
                amountTendered = double.Parse(Console.ReadLine());
                break;
            }
            catch
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
        double change = amountTendered - totalCost;
        Console.WriteLine($"Your change is {change}.");
    }
}
