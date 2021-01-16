using GenericApp.Web.Data;
using GenericApp.Web.Data.Entities;
using GenericApp.Web.Models;
using System.Globalization;
using System.Threading.Tasks;

namespace GenericApp.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public CategoryEntity ToCategoryEntity(CategoryViewModel model, string path, bool isNew)
        {
            return new CategoryEntity
            {
                Id = isNew ? 0 : model.Id,
                ImagePath = path,
                Name = model.Name
            };
        }

        public CategoryViewModel ToCategoryViewModel(CategoryEntity categoryEntity)
        {
            return new CategoryViewModel
            {
                Id = categoryEntity.Id,
                ImagePath = categoryEntity.ImagePath,
                Name = categoryEntity.Name
            };
        }

        public async Task<ProductEntity> ToProductAsync(ProductViewModel model, bool isNew)
        {
            return new ProductEntity
            {
                Category = await _context.Categories.FindAsync(model.CategoryId),
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                IsActive = model.IsActive,
                Name = model.Name,
                Price = ToPrice(model.PriceString),
                ProductImages = model.ProductImages
            };
        }

        private decimal ToPrice(string priceString)
        {
            string nds = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (nds == ".")
            {
                priceString = priceString.Replace(',', '.');

            }
            else
            {
                priceString = priceString.Replace('.', ',');
            }

            return decimal.Parse(priceString);
        }

        public ProductViewModel ToProductViewModel(ProductEntity product)
        {
            return new ProductViewModel
            {
                Categories = _combosHelper.GetComboCategories(),
                Category = product.Category,
                CategoryId = product.Category.Id,
                Description = product.Description,
                Id = product.Id,
                IsActive = product.IsActive,
                Name = product.Name,
                PriceString = $"{product.Price}",
                ProductImages = product.ProductImages,
            };
        }
    }
}