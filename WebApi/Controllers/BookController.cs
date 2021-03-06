using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        // private static List<Book> _books = new List<Book>()
        // {
        //     new Book{Id = 1, Title = "Lean Startup",GenreId=1,PageCount=200,PublishDate = new DateTime(2001,06,12)},
        //     new Book{Id = 2, Title = "Herland",GenreId=2,PageCount=250,PublishDate = new DateTime(2010,05,23)},
        //     new Book{Id = 3, Title = "Dune",GenreId=2,PageCount=570,PublishDate = new DateTime(1970,02,03)}
        // };

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result =query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            query.BookId = id;
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result=query.Handle();
            return Ok(result);
        }   

        //  [HttpGet] //Query ile get
        // public Book Get([FromQuery]string id)
        // {
        //     var  book = _books.Where(x=>x.Id==Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }

        [HttpPost]
        public IActionResult AddBook([FromBody]CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command); //hatay?? yakalar ve throw ile catch'e d??????r??r //her action'da try/catch yap??s?? yazmak istemedi??imizden, catch'e d??????rme i??leminide Custom Exception Middleware ile y??nettik
            command.Handle();
            // ValidationResult result=validator.Validate(command);
            // if (!result.IsValid)
            //     foreach (var item in result.Errors)
            //         Console.WriteLine("??zellik"+item.PropertyName+"- Error Message:"+item.ErrorMessage);
            // else
            //     command.Handle(); //Hatalar??m??z?? yakal??yoruz ancak UI taraf??na her zaman Ok() d??n??yoruz, bunu istemiyoruz
            
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody]UpdateBookModel updatedBook)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = id;
            command.Model = updatedBook;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = id;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}