using Microsoft.EntityFrameworkCore;
using WebApi.Entites;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                return;
                //DB'nin default veriler ile ayağa kalkmasını sağlar.
                context.Genres.AddRange(
                    new Genre{ Name="Personel Growth"},
                    new Genre{ Name="Science Fiction"},
                    new Genre{ Name="Romance"}
                );
                context.Books.AddRange(
                    new Book{/*Id = 1,*/ Title = "Lean Startup",GenreId=1,PageCount=200,PublishDate = new DateTime(2001,06,12)},
                    new Book{/*Id = 2,*/ Title = "Herland",GenreId=2,PageCount=250,PublishDate = new DateTime(2010,05,23)},
                    new Book{/*Id = 3,*/ Title = "Dune",GenreId=2,PageCount=570,PublishDate = new DateTime(1970,02,03)}
                );
                context.SaveChanges();
            }
        }
    }
}