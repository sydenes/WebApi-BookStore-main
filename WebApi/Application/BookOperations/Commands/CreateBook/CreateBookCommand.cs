using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using WebApi.Entites;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper=mapper;
        }
        public void Handle()
        {
            var  book = _dbContext.Books.SingleOrDefault(x=>x.Title==Model.Title);
            if (book is not null)
            throw new InvalidOperationException("Kitap zaten mevcut.");

            //book=new Book(){Title=Model.Title, PublishDate=Model.PublishDate, PageCount=Model.PageCount, GenreId=Model.GenreId};
            book=_mapper.Map<Book>(Model); //Model ile gelen veriyi 'Book' objesine convert et.MappingProfile sınıfında verdiğimiz config'den faydalandı.Bu şekilde üsteki gibi tek tek property atamasındansa automapper ile <source,target> ilişkisi ile auto map'liyoruz.


            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}