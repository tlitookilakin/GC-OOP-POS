namespace PointOfSale
{
    internal class SalesCalculator
    {
        //TODO add method to list items

        const decimal TAX_RATE = .07m;
        private readonly IEnumerable<Cafe> Purchases;

        public SalesCalculator(IEnumerable<Cafe> purchases)
        {
            Purchases = purchases;
        }

        public int Count()
        {
            return Purchases.Sum(cafe => cafe.Count);
        }

        public decimal GetSubTotal()
        {
            return Purchases.Sum(cafe => cafe.Price * cafe.Count);
        }

        public decimal GetSalesTax()
        {
            decimal taxableSubTotal = Purchases
                .Where(cafe => !cafe.Category.Equals("food", StringComparison.OrdinalIgnoreCase))
                .Sum(cafe => cafe.Price * cafe.Count);

            return Math.Round(taxableSubTotal * TAX_RATE, 2);
        }

        public decimal GetTotal()
        {
            return GetSubTotal() + GetSalesTax();
        }

        // items ordered
        // subtotal
        // grand total
        // payment type

        // cash paid with
        // card number
        // check number
        public void DisplayReceipt(string paymentType, decimal cashPaid = 0, string cardNumber = "", string checkNumber = "")
        {
            Console.Clear();
            decimal grandTotal = GetTotal();

            foreach (Cafe cafe in Purchases)
            {
                Console.WriteLine("{0,20} x{1,-3} @ {2,-6:C2}/ea : {3:C2}", cafe.MenuItem, cafe.Count, cafe.Price, cafe.Price * cafe.Count);
            }
            Console.WriteLine(new string('\x2500', 50));

            Console.WriteLine("SUBTOTAL: {0:C2}",GetSubTotal());
            Console.WriteLine("SALESTAX: {0:C2}",GetSalesTax());
            Console.WriteLine("GRAND TOTAL: {0:C2}",grandTotal);
            //Console.WriteLine($"Paid with {paymentType}.");
            if (paymentType == "cash")
            {
                Console.WriteLine("CASH TENDERED: {0:C2}",cashPaid);
                Console.WriteLine("CHANGE DUE: {0:C2}",cashPaid - grandTotal);
            }
            else if (paymentType == "credit card")
            {
                //Console.WriteLine($"Card used: {cardNumber}");
                Console.Write("VISA AUTHORIZED: ");
                string lastFour = cardNumber.Substring(12, 4);
                for (int cn = 0; cn < cardNumber.Length - 4; cn++)
                {
                    Console.Write($"x");
                }
                Console.WriteLine($"{lastFour}\n");
            }
            else
            {
                Console.WriteLine($"CHECK REFERENCE #: xxxxxxxxx xxxxxxxxxxxx {checkNumber}");
                //Console.WriteLine($"Check number: {checkNumber}");
            }

            Cafe.WriteToFile("product_list.tsv");
        }
    }
}
