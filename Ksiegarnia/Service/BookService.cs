using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Service
{
    public class BookService
    {
        private IAppDbContext _appDbContext;

        public BookService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Book> GetBooks() 
        {
            return _appDbContext.Books;
        }

        public async Task<Book> CreateBook(string tytul, string autor, Category category, User owner)
        {
            var newBook = new Book()
            {
                Title = tytul,
                Author = autor,
                Category = category,
                CategoryId = category.Id,
                Owner = owner,
                OwnerId = owner.Id,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var result = _appDbContext.Books.Add(newBook);
            await _appDbContext.SaveChangesAsync();
            return result;
        }

     

    


    }
}
