namespace CardFile.Business.Models;

public class Card {
    public int Id { get; set; }
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Studio { get; set; }
    public string Director { get; set; }
    public DateTime? EndDate { get; set; }
    public double Rating { get; set; }
}