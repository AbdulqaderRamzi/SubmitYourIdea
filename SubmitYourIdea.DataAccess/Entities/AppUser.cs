using Microsoft.AspNetCore.Identity;

namespace SubmitYourIdea.DataAccess.Entities;

public class AppUser : IdentityUser
{
    public required string FirstName { get; set; }    
    public required string LastName { get; set; }
}
