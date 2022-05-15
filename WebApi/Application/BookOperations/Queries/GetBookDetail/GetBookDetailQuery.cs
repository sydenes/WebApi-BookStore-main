using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebApi.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
            var  book = _dbContext.Books.Include(x=>x.Genre).Where(x=>x.Id==BookId).SingleOrDefault();
            if (book is null)
            throw new InvalidOperationException("Book not found");

            // BookDetailViewModel vm=new BookDetailViewModel(){
            //     Title=book.Title,
            //     Genre=((GenreEnum)book.GenreId).ToString(), //Id'e göre Enum'dan Genre'yi verir
            //     PageCount=book.PageCount,
            //     PublishDate=book.PublishDate.ToString("dd/MM/yyyy")};
            BookDetailViewModel vm =_mapper.Map<BookDetailViewModel>(book); //AutoMapper
            return vm;
        }
    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}