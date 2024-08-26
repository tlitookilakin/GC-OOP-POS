namespace PointOfSale;

class Program
{
    static void Main(string[] args)
    {
        Cafe.menu.Add(new Cafe("Iced latte", "drink", "coffee", 7.99M));
        Cafe.menu.Add(new Cafe("Black coffee", "drink", "coffee", 2.99M));
        Cafe.menu.Add(new Cafe("Water", "drink", "water", 3.99M));
        Cafe.menu.Add(new Cafe("Chai", "drink", "hot and cozy", 2.99m));
        Cafe.menu.Add(new Cafe("Peppermint tea", "drink", "fresh and herbal, caffeine free.", 2.99m));
        Cafe.menu.Add(new Cafe("Danish", "food", "bread product", 6.99M));
        Cafe.menu.Add(new Cafe("Bear claw", "food", "pastry", 5.99m));
        Cafe.menu.Add(new Cafe("Donut", "food", "pastry", 1.99m));
        Cafe.menu.Add(new Cafe("Fresh bread", "food", "whole grain, fresh baked", 3.99m));
        Cafe.menu.Add(new Cafe("Yesterday's bread", "food", "whole grain", .99m));
        Cafe.menu.Add(new Cafe("Tomorrow's bread", "food", "whole grain, unbaked", 11.99m));
        Cafe.menu.Add(new Cafe("Branded thermos", "merchandise", "1L insulated bottle", 33.98m));
        Cafe.menu.Add(new Cafe("Branded T-shirt", "merchandise", "Black shirt with logo", 18.75m));
        
        Cafe.Restock(20);
        Cafe.ReadFromFile("product_list.tsv");
        
        do
        {
            if (Validator.GetContinue("Would you like to restock?"))
            {
                Console.WriteLine("Please enter amount to stock to:");
                Cafe.Restock(Validator.GetPositiveInputInt());
            }
            Shop();
        }
        while (Validator.GetContinue("Would you like to make another purchase?"));

        Console.WriteLine("Thank you for shopping at The Three Musketeers'!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static void Shop()
    {
        List<Cafe> cart = new();

        do
        {
            Console.Clear();
			Console.WriteLine("Welcome to The Three Musketeers' Coffee Shop! Here is the full menu:");
			int i = 1;
            string drinks = "DRINKS:";
            Console.SetCursorPosition((Console.WindowWidth - drinks.Length) / 10, Console.CursorTop);  
            Console.WriteLine(drinks); 
            foreach (var m in Cafe.menu.GetRange(0,5))
            {
                Console.WriteLine("{2,3}. {0,20} {1,-6:C2}", m.MenuItem, m.Price, i);
                i++;
            }
            Console.WriteLine();
            string food = "FOOD/PASTRIES:";
            Console.SetCursorPosition((Console.WindowWidth - food.Length) / 10, Console.CursorTop);  
            Console.WriteLine(food); 
            foreach (var m in Cafe.menu.GetRange(5,6))
            {
                Console.WriteLine("{2,3}. {0,20} {1,-6:C2}", m.MenuItem, m.Price, i);
                i++;
            }
            Console.WriteLine();
            string swag = "MERCHANDISE:";
            Console.SetCursorPosition((Console.WindowWidth - swag.Length) / 10, Console.CursorTop);  
            Console.WriteLine(swag); 
            foreach (var m in Cafe.menu.GetRange(11,2))
            {
                Console.WriteLine("{2,3}. {0,20} {1,-6:C2}", m.MenuItem, m.Price, i);
                i++;
            }

            Console.WriteLine();
            Console.Write("Please enter a number to the corresponding item you want to purchase:  ");
            int whichItem = SelectItem();
            Cafe selected = Cafe.menu[whichItem];

            if (selected.Count == 0)
            {
                Console.WriteLine($"{selected.MenuItem} is out of stock.\n");
                continue;
            }

            Console.Write($"You chose {selected.MenuItem}. How many would you like to purchase?  ");
			int count = Validator.GetPositiveInputInt();

            if (count != 0)
                cart.Add(new Cafe(selected, count));

            Console.WriteLine($"You purchased {count} {selected.MenuItem}s.\n");
        }
        while (Validator.GetContinue("Would you like to purchase another item?"));
        Console.Clear();

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
            if (Validator.GetContinue("Would you like to add the item?") == true)
            {
                addItem();
                return Cafe.menu.Count - 1;
            }
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
            Console.WriteLine($"Purchasing {sales.Count()} item(s) for a total of ${sales.GetTotal()}");
            //Console.WriteLine("Please select payment type:");
            Console.WriteLine("Please choose your payment method. We accept: cash, credit card, check.");
            paymentType = Console.ReadLine().Trim().ToLower();
            if ("cash".Contains(paymentType))
            {
                sales.DisplayReceipt("cash",payCash(sales.GetTotal()));
                break;
            }
            else if ("credit".Contains(paymentType) || "card".Contains(paymentType) || "credit card".Contains(paymentType))
            {
                sales.DisplayReceipt("credit card",cardNumber: payCreditCard(sales.GetTotal()));
                break;
            }
            else if ("check".Contains(paymentType))
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
                    decimal shortOnCash = totalCost - amountTendered;
                    Console.WriteLine($"Sorry, this is not enough. Your total is ${totalCost}, you still need ${shortOnCash}.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }
        return amountTendered;
    }

    static string payCreditCard(decimal totalCost)
    {
        string cardNumber;
        string expiration;
        string cvv;
        
        while (true)
        {
            Console.Write("Please enter 16-digit card number:  ");
            cardNumber = Console.ReadLine();
            if (cardNumber.Length != 16)
            {
                Console.WriteLine("Invalid card number. Please try again.");
            }
            else
            {
                break;
            }
        }

        //validating expiration date
        while (true)
        {
            Console.Write("Please enter expiration date (MM/YY):  ");
            expiration = Console.ReadLine();
            string[] expirationDate = expiration.Split('/','\\');
            if (expirationDate.Length == 2 && int.TryParse(expirationDate[0],out int expirationMonth) && int.TryParse(expirationDate[1], out int expirationYear))
            {
                expirationYear += 2000;
                if (expirationMonth <= 12 && expirationMonth > 0)
                {
                    if ((expirationMonth > DateTime.Now.Month && expirationYear == DateTime.Now.Year) || expirationYear > DateTime.Now.Year)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The card provided is expired. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid month. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        //validating cvv
        while (true)
        {
            Console.Write("Please enter 3-digit CVV:  ");
            cvv = Console.ReadLine();
            if (cvv.Length != 3)
            {
                Console.WriteLine("Invalid CVV. Please try again.");
            }
            else
            {
                break;
            }
        }
        Console.Clear();
        return cardNumber;
    }

    static string payCheck(decimal totalCost)
    {
        string checkNumber;
        while (true)
        {
            Console.Write("Please enter 6-digit check number:  ");
            checkNumber = Console.ReadLine();
            if (checkNumber.Length != 6)
            {
                Console.WriteLine("Invalid check. Please try again.");
            }
            else
            {
                break;
            }
        }
        Console.Clear();
        return checkNumber;
    }
    
    static void addItem()
    {
        string itemName;
        string itemCategory;
        string itemDescription;
        decimal itemPrice;
        int stock;

        Console.WriteLine("Please enter the name of the item.");
        itemName = Console.ReadLine();
        Console.WriteLine("Please enter the category of the item.");
        itemCategory = Console.ReadLine();
        Console.WriteLine("Please enter the item description.");
        itemDescription = Console.ReadLine();
        Console.WriteLine("Please enter the price of the item.");
        itemPrice = Math.Round(Validator.GetPositiveInputDecimal(),2);
        Console.WriteLine("How many of this item would you like to stock?");
        stock = Validator.GetPositiveInputInt();

        Cafe.menu.Add(new Cafe(itemName, itemCategory, itemDescription, itemPrice, stock));
        Cafe.WriteToFile("product_list.tsv");
    }
}