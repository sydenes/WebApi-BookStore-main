using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre=_context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if (genre is null)
                throw new InvalidOperationException("Kitap türü bulunamadı.");
            if(_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli kitap türü mevcut.");    
            
            genre.Name=string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name; //Eğer Name kısmını update etmeyekse 
            //boş gönderdiyse hata yerine default(önceki ismini) yazdırıp sadece IsActive değerinin değiştirilmesine imkan verdik.
            genre.IsActive=Model.IsActive;
            _context.SaveChanges();
        }
    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }=true;
    }
}