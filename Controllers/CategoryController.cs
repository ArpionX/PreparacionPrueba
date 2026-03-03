using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba2.Data.DTOs.CategoriesDTOs;
using Prueba2.Services.CategoriesService;

namespace Prueba2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var response = await _categoriesService.GetCategories();
                return Ok(response);
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Error Obteniendo categorias",
                    Details = ex.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestDTO dto)
        {
            try
            {
                await _categoriesService.CreateCategory(dto);
                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Error creando categoria",
                    Details = ex.Message
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequestDTO dto)
        {
            try
            {
                await _categoriesService.UpdateCategory(id, dto);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Error actualizando categoria",
                    Details = ex.Message
                });
            }
        }

        [HttpPut("delete")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                await _categoriesService.DeleteCategory(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Error eliminando categoria",
                    Details = ex.Message
                });
            }
        }
    }
}
