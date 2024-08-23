namespace PointOfSale;

class Program
{
    static void Main(string[] args)
    {
        Cafe.menu.Add(new Cafe("Iced latte", "drink", "coffee", 7.99M));
        Cafe.menu.Add(new Cafe("Black coffee", "drink", "coffee", 2.99M));
        Cafe.menu.Add(new Cafe("Water", "drink", "water", 3.99M));
        Cafe.menu.Add(new Cafe("Danish", "food", "bread product", 6.99M));
        Cafe.menu.Add(new Cafe("Branded thermos", "merchandise", "1L insulated bottle", 33.98m));
        Cafe.menu.Add(new Cafe("Branded T-shirt", "merchandise", "Black shirt with logo", 18.75m));
        Cafe.menu.Add(new Cafe("Bear claw", "food", "pastry", 5.99m));
        Cafe.menu.Add(new Cafe("Donut", "food", "pastry", 1.99m));
        Cafe.menu.Add(new Cafe("Fresh bread", "food", "whole grain, fresh baked", 3.99m));
        Cafe.menu.Add(new Cafe("Yesterday's bread", "food", "whole grain", .99m));
        Cafe.menu.Add(new Cafe("Chai", "drink", "hot and cozy", 2.99m));
        Cafe.menu.Add(new Cafe("Peppermint tea", "drink", "fresh and herbal, caffeine free.", 2.99m));
        
        Console.WriteLine("Welcome to the Three Musketeers' Coffee Shop:");
        do
        {
            Shop();
        }
        while (Validator.GetContinue("Would you like to make another purchase?"));

        Console.WriteLine("Thank you for shopping at Three Musketeers'!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void Shop()
    {
        List<Cafe> cart = new();

        do
        {
            foreach (var m in Cafe.menu)
            {
                Console.WriteLine($"{m.MenuItem} {m.Price}");
            }
            Console.WriteLine("What can I get for you today?");

            Console.WriteLine("How many do you want?");
            Validator.GetPositiveInputInt();
        }
        while (Validator.GetContinue("Would you like to purchase another item?"));

        SalesCalculator sales = new(cart);
        makePayment(sales);
    }
    
    static void makePayment(SalesCalculator sales)
    { 
        string paymentType;
        while (true)
        {
            Console.WriteLine("Please select payment type:");
            Console.WriteLine("Enter 'cash',  'credit card', or 'check'.");
            paymentType = Console.ReadLine().ToLower();
            if (paymentType == "cash")
            {
                payCash(sales.GetTotal());
                break;
            }
            else if (paymentType == "credit card")
            {
                payCreditCard(sales.GetTotal());
                break;
            }
            else if (paymentType == "check")
            {
                payCheck(sales.GetTotal());
                break;
            }
        }
        //TODO display receipt and verify payment
    }

    static decimal payCash(decimal totalCost) 
    {
        decimal amountTendered;
        while (true)
        {
            Console.WriteLine($"Your total is {totalCost}.");
            Console.WriteLine("How much cash will you pay with?");
            try
            {
                amountTendered = decimal.Parse(Console.ReadLine());
                if (amountTendered >= totalCost)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("This is not enough cash.");
                }
            }
            catch
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
        decimal change = amountTendered - totalCost;
        Console.WriteLine($"Your change is {change}.");
        return change;
    }

    static void payCreditCard(decimal totalCost)
    {
        //TODO validation
        string cardNumber;
        string expiration;
        string cvv;

        Console.WriteLine("Please enter card number.");
        cardNumber = Console.ReadLine();
        Console.WriteLine("Please enter expiration.");
        expiration = Console.ReadLine();
        Console.WriteLine("Please enter CVV.");
        cvv = Console.ReadLine();

        //TODO verify card is not expired
    }

    static void payCheck(decimal totalCost)
    {
        string checkNumber;

        Console.WriteLine("Please enter check number.");
        checkNumber = Console.ReadLine();
    }
}
