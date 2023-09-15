namespace MMABackend.Controllers
{
    public class ArgumentUpdateAppliedAuction
    {
        public string BuyerEmail { get; set; }
        public int ProductId { get; set; }
        public decimal UpdatedPrice { get; set; }
    }
}