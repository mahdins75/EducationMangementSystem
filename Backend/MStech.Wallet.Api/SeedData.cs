using Microsoft.AspNetCore.Identity;
using Mstech.Entity.Etity;

public class SeedData
{
    public async Task SeedUsersAsync(UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new User
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Phone = "123123_Mm",
                FullName = "John Doe",
                Name = "John",
                LastName = "Doe",
                Address = "123 Main St",
                Avatar = "avatar.jpg",
                Description = "Sample user description",
                IsActive = true,
                IsDeleted = false,
            };

            var result = await userManager.CreateAsync(user, "Password123!"); // Set a secure password

            if (result.Succeeded)
            {
                // Add roles or additional user claims as needed
            }
        }
    }
}
