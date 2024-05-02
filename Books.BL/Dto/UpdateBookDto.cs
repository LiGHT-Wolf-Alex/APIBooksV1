namespace Books.BL.Dto;

public class UpdateBookDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int AuthorId { get; set; }
    public string Summaries { get; set; } = default!;
    public int Count { get; set; }
}