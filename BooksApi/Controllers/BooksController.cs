using Books.BL;
using Books.BL.Dto;
using Common.Domain;
using Microsoft.AspNetCore.Mvc;


namespace BooksApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    public IActionResult GetList(int? offset, string? nameFreeText, int? limit)
    {
        var books = _booksService.GetList(offset, nameFreeText, limit);
        var count = _booksService.Count(null);
        HttpContext.Response.Headers.Append("X-Total-Count", count.ToString());
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetList(int id)
    {
        var book = _booksService.GetById(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet("TotalCount")]
    public IActionResult GetCount(string? nameFreeText)
    {
        return Ok(_booksService.Count(nameFreeText));
    }    
    [HttpPost]
    public IActionResult Post(CreateBookDto createBookDto)
    {
        var createdBook = _booksService.Create(createBookDto);
        return Created($"books/{createdBook.Id}", createdBook);
    }

    [HttpPut("{id}")]
    public IActionResult Post(int id, UpdateBookDto updateBookDto)
    {
        updateBookDto.Id = id;
        var updated = _booksService.Update(updateBookDto);
        if (updated != null)
        {
            return Ok(updated);
        }
        return NotFound();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var result = _booksService.Delete(id);
        return result ? Ok() : NotFound();
    }
}