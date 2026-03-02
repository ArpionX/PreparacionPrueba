using Prueba2.Data.DTOs.CategoriesDTOs;

namespace Prueba2.Services.CategoriesService
{
    public interface ICategoriesService
    {
        Task<List<CategoryResponseDTO>> GetCategories();
        Task<CategoryResponseDTO> GetCategoryById(Guid id);
        Task CreateCategory(CategoryRequestDTO categoryRequest);
        Task UpdateCategory(Guid id, CategoryRequestDTO categoryRequest);
        Task DeleteCategory(Guid id);
    }
}
