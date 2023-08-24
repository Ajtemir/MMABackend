using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MMABackend.ViewModels.Common;

namespace MMABackend.Controllers
{
    public partial class CategoriesController
    {
        [HttpGet]
        public ActionResult<List<ReadCategoryViewModel>> Index() => Execute(() =>
            {
                var categories = _uow.Categories.Select(x => (ReadCategoryViewModel)x).ToList();
                return categories;
            }
        );
    }
}