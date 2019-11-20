using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookingAppStore.Models
{
    public class BookContext:DbContext
    {    
        //якщо модель називається Book,то властивість (property) ми 
        // називаємов множині -- Books !!!
        public DbSet<Book> Books { get; set; }

        public DbSet<Purchase> Purchases{ get; set; }
    }
    public class BookDbInitializer : DropCreateDatabaseAlways<BookContext>
    {
        protected override void Seed(BookContext db)
        {
            db.Books.Add(new Book { Name = "Війна і мир", Author = "Л. Толстой", Price = 220 });
            db.Books.Add(new Book { Name = "Батьки та діти", Author = "І. Тургенєв", Price = 180 });
            db.Books.Add(new Book { Name = "Чайка", Author = "А. Чехов", Price = 150 });

            base.Seed(db);
        }
    }
}