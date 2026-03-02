using Microsoft.EntityFrameworkCore;
using Prueba2.Data;
using Prueba2.Data.DTOs.CategoriesDTOs;
using Prueba2.Data.Models;

namespace Prueba2.Services.CategoriesService
{
    //lo primero que hacemos es definir que esta clase va a implementar la interfaz ICategoriesService,
    //Esto nos obliga a implmentar los metodos definidos en la interfaz, buena practica para asegurarnos de que nuestra clase cumple con un contrato definido.
    public class CategoriesService: ICategoriesService//esta es la interfaz
    {
        // lo siguiente es poner el contexto de AppDbContext, esto nos permite acceder a la base de datos y realizar operaciones CRUD
        // (Crear, Leer, Actualizar, Eliminar) en la tabla de categorias.
        private readonly AppDbContext _context;
        //Creamos el contructor que va a tener el AppDbContext como parametro, esto nos permite inyectar
        //el contexto de la base de datos cuando creamos una instancia de CategoriesService, lo que es una buena practica para manejar las dependencias y facilitar las pruebas unitarias.
        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryResponseDTO>> GetCategories()
        {
            try
            {

                var categories = await _context.Categories.ToListAsync();
                if (string.IsNullOrEmpty(categories.ToString())) throw new Exception("No se encontraron categorías");

                var response = categories.Select(c => new CategoryResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes registrar el error o lanzar una excepción personalizada
                throw new Exception("Error al obtener las categorías", ex);
            }
        }
        public async Task<CategoryResponseDTO> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _context.Categories.FindAsync(id);
                if (string.IsNullOrEmpty(category.ToString())) throw new Exception("No encontrado");

                var response = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };
                return response;
            }
            catch(Exception ex)
            {
                // Manejo de errores, puedes registrar el error o lanzar una excepción personalizada
                throw new Exception("Error al obtener la categoría por ID", ex);

            }
        }
        public async Task CreateCategory(CategoryRequestDTO categoryRequest)
        {
            try
            {
                if(string.IsNullOrEmpty(categoryRequest.Name)) throw new Exception("El nombre de la categoría no puede estar vacío");

                if (categoryRequest.Name.Length > 100) throw new Exception("El nombre no puedo exceder más de los 100 caracteres");

                var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryRequest.Name);
                if (existingCategory != null) throw new InvalidOperationException("Ya existe una categoría con ese nombre");

                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryRequest.Name
                };
                _context.Categories.Add(category);
                
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Error al crear la categoría", ex);
            }
        }
        public async Task UpdateCategory(Guid id, CategoryRequestDTO categoryRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryRequest.Name)) throw new Exception("Tiene que definir un nuevo nombre");
                if (string.IsNullOrEmpty(id.ToString())) throw new Exception("Tienes que definir un ID");
                var category = await _context.Categories.FindAsync(id);

                if (string.IsNullOrEmpty(category.ToString())) throw new Exception("No se encontró la categoría");

                category.Name = categoryRequest.Name;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex) 
            {
                throw new Exception("Error al actualizar categoria", ex);
            }
        }
        public async Task DeleteCategory(Guid id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString())) throw new Exception("Tienes que definir un ID");
                var category = await _context.Categories.FindAsync(id);
                if (string.IsNullOrEmpty(category.ToString())) throw new Exception("No se encontró la categoría");

                category.IsActive = false;
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

            }
            catch(Exception ex)
            {
                throw new Exception("Error al eliminar la categoría", ex);
            }
        }

    }
}
