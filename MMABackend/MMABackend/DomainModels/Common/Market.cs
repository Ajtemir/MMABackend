using System.Collections.Generic;

namespace MMABackend.DomainModels.Common
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Shop> Shops { get; set; } = new List<Shop>();
    }
}