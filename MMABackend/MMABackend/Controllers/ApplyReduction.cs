using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMABackend.Configurations.Users;
using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Apply([FromBody]ApplyReductionArgument argument) => Execute(() =>
        {
            var reduction =
                Uow.ActualReductionProductsWithOrdering.FirstOrError(
                    x => x.ProductId == argument.productId,
                    "Не найден не является тендерным");
            Uow.AuctionProductUsers.Add(new AuctionProductUser
            {
                Price = argument.SuggestedPrice,
                AuctionProductId = reduction.Id,
                UserId = UserId,
            });
            Uow.SaveChanges();
        });
    }
}