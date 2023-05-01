using MMABackend.DomainModels.Common;

namespace MMABackend.ViewModels.Common
{
    public class ReadCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IconId { get; set; }

        public static explicit operator ReadCategoryViewModel(Category model)
        {
            return new ReadCategoryViewModel
            {
                Id = model.Id,
                Name = model.Name,
                IconId = model.IconId,
            };
        }
    }
}