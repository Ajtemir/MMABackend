using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class GroupDiscountController
    {
        [HttpDelete]
        public ActionResult UnmakeProductGroupDiscount([FromBody] UnmakeProductGroupDiscountArgument argument) => Execute(() =>
            {
                var product = _uow.GroupDiscountProducts.FirstOrError(x=>x.ProductId == argument.ProductId && x.IsActual.Value);
                product.Status = CollectiveProductStatus.Canceled;
                product.IsActual = null;
                _uow.SaveChanges();
            }
        );
    }

    public class UnmakeProductGroupDiscountArgument
    {
        public int ProductId { get; set; }
    }
}