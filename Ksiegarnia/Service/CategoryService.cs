using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Service
{
    public class CategoryService
    {
        private IAppDbContext _appDbContext;

        public CategoryService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> GetCategory() 
        {
            return _appDbContext.Category;
        }

        public async Task<Category> CreateCategory(string nazwa)
        {
            var newCategory = new Category()
            {
                Name = nazwa,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var result = _appDbContext.Category.Add(newCategory);

            await _appDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> RemoveCategory(int id)
        {
            var remove = _appDbContext.Category.First(n => n.Id == id);
            var result = _appDbContext.Category.Remove(remove);

            await _appDbContext.SaveChangesAsync();

            return result != null;
        }

        public async Task<Category> UpdateCategory(int id, string nazwa)
        {
            var update = _appDbContext.Category.First(n => n.Id == id);

            update.Name = nazwa;
            update.ModifiedDate = DateTime.Now;

            await _appDbContext.SaveChangesAsync();

            return update;
        }
    }
}
