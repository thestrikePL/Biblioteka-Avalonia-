using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Service
{
    public class CheckoutService
    {
        private IAppDbContext _appDbContext;

        public CheckoutService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Checkout> GetCheckouts() 
        {
            return _appDbContext.Checkouts;
        }

        public async Task<Checkout> CreateCheckout(Book book, string name, string phone, DateTime from, DateTime to)
        {
            var checkout = new Checkout()
            {
                Book = book,
                BookId = book.Id,
                LastName = name,
                Phone = phone,
                From = from,
                To = to,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var result = _appDbContext.Checkouts.Add(checkout);
            await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> RemoveCheckout(int id)
        {
            var remove = _appDbContext.Checkouts.First(n => n.Id == id);
            var result = _appDbContext.Checkouts.Remove(remove);

            await _appDbContext.SaveChangesAsync();

            return result != null;
        }

        public async Task<Checkout> UpdateCheckout(int id, Book book, string name, string phone, DateTime from, DateTime to)
        {
            var update = _appDbContext.Checkouts.First(n => n.Id == id);

            update.Book = book;
            update.BookId = book.Id;
            update.LastName = name;
            update.Phone = phone;
            update.From = from;
            update.To = to;
            update.ModifiedDate = DateTime.Now;

            await _appDbContext.SaveChangesAsync();

            return update;
        }
    }
}
