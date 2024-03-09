using System.ComponentModel.DataAnnotations.Schema;

namespace MMABackend.DomainModels.Common
{
    public partial class GroupDiscountProduct
    {
        [NotMapped] public int CurrentPurchasersCount => CollectivePurchasers.Count;
    }
}