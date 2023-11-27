using System;

namespace MMABackend.Controllers
{
    public class MakeReductionArgument
    {
        public int ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal StartPrice { get; set; }
    }
}