using Microsoft.EntityFrameworkCore;

namespace SubmitYourIdea.DataAccess.Entities;


public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
}