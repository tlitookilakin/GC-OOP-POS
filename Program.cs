namespace PointOfSale;

class Program
{
    static void Main(string[] args)
    {
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
}
