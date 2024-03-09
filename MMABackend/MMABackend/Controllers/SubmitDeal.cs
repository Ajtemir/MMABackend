using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class GroupDiscountController
    {
        [HttpPost]
        public ActionResult SubmitDeal([FromBody]SubmitDeal args) => Execute(() =>
        {
            var product = _uow.GroupDiscountProducts
                .FirstOrError(x => x.IsActual != null && x.IsActual.Value && x.ProductId == args.ProductId);
            product.Status = CollectiveProductStatus.Submitted;
            product.IsActual = null;
            _uow.SaveChanges();
        });
    }

    public class SubmitDeal
    {
        public int ProductId { get; set; }
    }
}