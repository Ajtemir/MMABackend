namespace MMABackend.ViewModels.Product
{
    public class AddProductViewModel
    {
        public string Description { get; set; } = string.Empty;
        public decimal? Price { get; set; } = null;
        public int CategoryId { get; set; }
        
        public static implicit operator DomainModels.Common.Product(AddProductViewModel model)
        {
            return new DomainModels.Common.Product
            {
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
            };
        }
    }
}