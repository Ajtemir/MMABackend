using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public partial class CollectiveSoldProduct
    {
        [NotMapped] public int CurrentPurchasersCount => CollectivePurchasers.Count;
    }
}