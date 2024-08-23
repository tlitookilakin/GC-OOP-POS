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
			decimal grandTotal = GetTotal();

            Console.WriteLine($"Paid with {paymentType}.");
            if (paymentType == "cash")
			{
				Console.WriteLine("Amount paid: {0:C2}",cashPaid);
                Console.WriteLine("Change: {0:C2}",cashPaid - grandTotal);
            }
			else if (paymentType == "credit card")
			{
                // TODO card number substring for last 4 digits
                Console.WriteLine($"Card used: {cardNumber}");
            }
			else
			{
                Console.WriteLine($"Check number: {checkNumber}");
            }

			Cafe.WriteToFile("product_list.tsv");
		}
	}
}
