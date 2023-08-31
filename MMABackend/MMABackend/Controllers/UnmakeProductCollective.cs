using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class CollectiveTradeController
    {
        [HttpDelete]
        public ActionResult UnmakeProductCollective([FromBody] UnmakeProductCollectiveArgument argument) => Execute(() =>
            {
                var product = _uow.CollectiveSoldProducts.FirstOrError(x=>x.ProductId == argument.ProductId && x.IsActual.Value);
                product.Status = CollectiveProductStatus.Canceled;
                product.IsActual = null;
                _uow.SaveChanges();
            }
        );
    }

    public class UnmakeProductCollectiveArgument
    {
        public int ProductId { get; set; }
    }
}