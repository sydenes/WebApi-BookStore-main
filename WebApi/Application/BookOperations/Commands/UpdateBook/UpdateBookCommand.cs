using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Common;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        public UpdateBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var book= _dbContext.Books.SingleOrDefault(x=>x.Id==BookId);
            if (book is null)
            throw new InvalidOperationException("No book found to be updated");
            
            book.GenreId=Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.Title=Model.Title != default ? Model.Title : book.Title;
            _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; } //Sadece 2 bilgiyi update açık hale getirmek istedik
    }
}