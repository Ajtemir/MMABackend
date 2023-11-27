using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMABackend.Configurations.Users;

namespace MMABackend.Controllers
{
    public partial class ReductionController
    {
        [Authorize]
        [HttpPost]
        public ActionResult Apply([FromBody]ApplyReductionArgument argument) => Execute(() =>
        {

        });
    }
}