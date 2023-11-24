using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMABackend.Configurations.Users;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [HttpPost]
        [Authorize(AuthenticationSchemes = AccessTokenConfig.SchemeName)]
        public ActionResult Apply([FromBody]ApplyReductionArgument argument) => Execute(() =>
        {
            
        });
    }
}