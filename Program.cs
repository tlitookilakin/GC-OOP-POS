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
                //Console.WriteLine($"{m.MenuItem} ${m.Price}");
                Console.WriteLine("{0,20} {1:C2}",m.MenuItem,m.Price);
            }

            Console.WriteLine("What can I get for you today?");
            int whichItem = SelectItem();
            Cafe selected = Cafe.menu[whichItem];

            Console.WriteLine($"Purchasing {selected.MenuItem}. How many do you want?");
			int count = Validator.GetPositiveInputInt();

            for (int i = 0; i < count; i++)
                cart.Add(selected);

            Console.WriteLine($"Purchased {count} {selected.MenuItem}s.");
        }
        while (Validator.GetContinue("Would you like to purchase another item?"));

        SalesCalculator sales = new(cart);
        makePayment(sales);
    }

    static int SelectItem()
    {
        while (true)
        {
            string? line = Console.ReadLine();
            if (line == null)
                return -1;

            line = line.Trim();

            if (int.TryParse(line, out int index) && (index >= 1 || index <= Cafe.menu.Count))
                return index - 1;

            index = Cafe.menu.FindIndex(item => item.MenuItem.Contains(line, StringComparison.OrdinalIgnoreCase));

			if (index >= 0)
                return index;

            Console.WriteLine("Sorry, I don't know what that is.");
        }
    }
    
    static void makePayment(SalesCalculator sales)
    { 
        if (sales.GetTotal() == 0)
        {
            return;
        }
        string paymentType;
        while (true)
        {
            Console.WriteLine("Please select payment type:");
            Console.WriteLine("Enter 'cash',  'credit card', or 'check'.");
            paymentType = Console.ReadLine().ToLower();
            if (paymentType == "cash")
            {
                sales.DisplayReceipt("cash",payCash(sales.GetTotal()));
                break;
            }
            else if (paymentType == "credit card")
            {
                sales.DisplayReceipt("credit card",cardNumber: payCreditCard(sales.GetTotal()));
                break;
            }
            else if (paymentType == "check")
            {
                sales.DisplayReceipt("check", checkNumber: payCheck(sales.GetTotal()));
                break;
            }
        }
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
        //decimal change = amountTendered - totalCost;
        //Console.WriteLine($"Your change is {change}.");
        return amountTendered;
    }

    static string payCreditCard(decimal totalCost)
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

        return cardNumber;
    }

    static string payCheck(decimal totalCost)
    {
        string checkNumber;

        Console.WriteLine("Please enter check number.");
        checkNumber = Console.ReadLine();

        return checkNumber;
    }
}
