using Books.BL.Dto;
using Common.Domain;

namespace Books.BL
{
    public interface IBooksService
    {
        IReadOnlyCollection<Book> GetList(int? offset, string? nameFreeText, int? limit = 10);
        Book? GetById(int id);
        Book Create(CreateBookDto createBookDto);
        Book? Update(UpdateBookDto updateBookDto);
        int Count(string? nameFreeText);
        bool Delete(int id);
    }
}
