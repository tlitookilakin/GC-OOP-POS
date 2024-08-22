namespace PointOfSale
{
	internal class SalesCalculator
	{
		private readonly IEnumerable<object> Purchases;

		public SalesCalculator(IEnumerable<object> purchases)
		{
			Purchases = purchases;
		}

		public decimal GetSubTotal()
		{
			return 0m;
		}

		public decimal GetSalesTax()
		{
			return 0m;
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
		public void DisplayReceipt(string paymentType, decimal cashPaid, string cardNumber, string checkNumber)
		{
			decimal grandTotal = GetTotal();

            Console.WriteLine($"Paid with {paymentType}.");
            if (paymentType == "cash")
			{
                Console.WriteLine($"Amount paid: {cashPaid}");
                Console.WriteLine($"Change: {cashPaid - grandTotal}");
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
		}
	}
}
