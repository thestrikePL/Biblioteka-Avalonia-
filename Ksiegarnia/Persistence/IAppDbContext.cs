using Ksiegarnia.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Persistence
{
    public interface IAppDbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Checkout> Checkouts { get; set; }

        Task<int> SaveChangesAsync();

    }
}
