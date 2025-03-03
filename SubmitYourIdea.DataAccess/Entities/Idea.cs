namespace SubmitYourIdea.DataAccess.Entities;

public class Idea
{
    public int Id { get; set; } // Should be guid 
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string Status { get; set; } = "pending";
    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public string UserId { get; set; } = null!;
    public AppUser? User { get; set; }
}