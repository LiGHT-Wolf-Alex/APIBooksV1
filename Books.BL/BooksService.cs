using AutoMapper;
using AutoMapper.Internal;
using Books.BL.Dto;
using Common.Domain;
using Common.Repositories;

namespace Books.BL;

public class BooksService : IBooksService
{
    private readonly IRepository<Book> _booksRepository;
    private readonly IRepository<User> _usersRepository;
    private readonly IMapper _mapper;

    public BooksService(IRepository<Book> booksRepository, IRepository<User> usersRepository, IMapper mapper,
        IRepository<Book> booksRepository1, IRepository<User> usersRepository1)
    {
        _mapper = mapper;
        _booksRepository = booksRepository1;
        _usersRepository = usersRepository1;
        _booksRepository.Add(new Book()
        {
            Id = 1, AuthorId = 1, DateCreated = DateTime.UtcNow, Count = 123, Name = "Book 1", Summaries = "Best book"
        });
        _booksRepository.Add(new Book()
        {
            Id = 2, AuthorId = 2, DateCreated = DateTime.UtcNow, Count = 123, Name = "Book 2", Summaries = "Best book"
        });
        _booksRepository.Add(new Book()
        {
            Id = 3, AuthorId = 3, DateCreated = DateTime.UtcNow, Count = 123, Name = "Book 3", Summaries = "Best book"
        });
        _booksRepository.Add(new Book()
        {
            Id = 4, AuthorId = 1, DateCreated = DateTime.UtcNow, Count = 123, Name = "Book 4", Summaries = "Best book"
        });
        _booksRepository.Add(new Book()
        {
            Id = 5, AuthorId = 2, DateCreated = DateTime.UtcNow, Count = 123, Name = "Book 5", Summaries = "Best book"
        });
        _booksRepository.Add(new Book()
        {
            Id = 6, AuthorId = 3, DateCreated = DateTime.UtcNow, Count = 123, Name = "Book 6", Summaries = "Best book"
        });

        _usersRepository.Add(new User() { Id = 1, Name = "name 1" });
        _usersRepository.Add(new User() { Id = 2, Name = "name 2" });
        _usersRepository.Add(new User() { Id = 3, Name = "name 3" });
    }

    public Book? GetById(int id)
    {
        return _booksRepository.SingleOrDefault(b => b.Id == id);
    }

    public Book Create(CreateBookDto createBookDto)
    {
        var user = _usersRepository.SingleOrDefault(u => u.Id == createBookDto.AuthorId);
        if (user == null)
        {
            throw new Exception("Author not found");
        }

        var bookEntity = _mapper.Map<CreateBookDto, Book>(createBookDto);
        bookEntity.DateCreated = DateTime.UtcNow;
        bookEntity.Id = _booksRepository.GetList().Length == 0 ? 1 : _booksRepository.GetList().Max(b => b.Id) + 1;

        return _booksRepository.Add(bookEntity);
    }


    public int Count(string? nameFreeText)
    {
        return _booksRepository.Count(nameFreeText == null
            ? null
            : b => b.Name.Contains(nameFreeText, StringComparison.CurrentCultureIgnoreCase));
    }

    public bool Delete(int id)
    {
        var bookToDelete = GetById(id);
        if (bookToDelete != null)
        {
            return _booksRepository.Delete(bookToDelete);
        }

        return false;
    }

    public IReadOnlyCollection<Book> GetList(int? offset, string? nameFreeText, int? limit = 10)
    {
        return _booksRepository.GetList(
            offset,
            limit,
            nameFreeText == null ? null : b => b.Name.Contains(nameFreeText, StringComparison.InvariantCulture),
            b => b.Id);
    }

    public Book? Update(UpdateBookDto updateBookDto)
    {
        var user = _usersRepository.SingleOrDefault(u => u.Id == updateBookDto.AuthorId);
        if (user == null)
        {
            throw new Exception("Author not found");
        }

        var bookEntity = GetById(updateBookDto.Id);
        if (bookEntity == null)
        {
            return null;
        }

        _mapper.Map(updateBookDto, bookEntity);
        bookEntity.DateUpdate = DateTime.UtcNow;

        return _booksRepository.Update(bookEntity);
    }
}