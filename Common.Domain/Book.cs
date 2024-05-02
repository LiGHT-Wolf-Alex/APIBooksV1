namespace Common.Domain;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int AuthorId { get; set; }
    public string Summaries { get; set; } = default!;
    public int Count { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdate { get; set; }

}
