using GenericApp.Web.Data.Entities;
using GenericApp.Web.Models;
using System.Threading.Tasks;

namespace GenericApp.Web.Helpers
{
    public interface IConverterHelper
    {
        CategoryEntity ToCategoryEntity(CategoryViewModel model, string path, bool isNew);

        CategoryViewModel ToCategoryViewModel(CategoryEntity categoryEntity);

        Task<ProductEntity> ToProductAsync(ProductViewModel model, bool isNew);

        ProductViewModel ToProductViewModel(ProductEntity product);
    }
}