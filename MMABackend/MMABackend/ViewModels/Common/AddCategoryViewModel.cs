using MMABackend.DomainModels.Common;

namespace MMABackend.ViewModels.Common
{
    public class AddCategoryViewModel
    {
        public string Name { get; set; }
        public int IconId { get; set; }

        public static implicit operator Category(AddCategoryViewModel model)
        {
            return new Category
            {
                Name = model.Name,
                IconId = model.IconId,
            };
        }
    }
}