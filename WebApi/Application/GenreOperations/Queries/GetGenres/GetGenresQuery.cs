using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        public readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genres=_context.Genres.Where(a=>a.IsActive).OrderBy(x=>x.Id);
            return _mapper.Map<List<GenresViewModel>>(genres);
        }
    }
    public class GenresViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}