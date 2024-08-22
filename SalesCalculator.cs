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
	}
}
