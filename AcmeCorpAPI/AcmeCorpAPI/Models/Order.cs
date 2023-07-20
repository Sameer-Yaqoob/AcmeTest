namespace AcmeCorpAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual decimal CalculatePrice(decimal Price)
        {
            return Price;
        }
    }
    public class DiscountedOrder : Order
    {
        public override decimal CalculatePrice(decimal Price)
        {
            return base.CalculatePrice(Price) * 0.9m;
        }
    }
    public class ExpressOrder : Order
    {

    }
    public class OrderProcessor
    {
        public void Process(Order order)
        {
        }
    }
}
