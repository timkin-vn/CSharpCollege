namespace CardFile.DataAccess.Dtos;
public class CardDto {
    public int Id { get; set; }
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Studio { get; set; }
    public string Director { get; set; }
    public DateTime? EndDate { get; set; }
    public double Rating { get; set; }

    public CardDto Clone() {
        return new CardDto {
            Id = Id,
            Title = Title,
            OriginalTitle = OriginalTitle,
            Genre = Genre,
            ReleaseDate = ReleaseDate,
            Studio = Studio,
            Director = Director,
            EndDate = EndDate,
            Rating = Rating
        };
    }
}