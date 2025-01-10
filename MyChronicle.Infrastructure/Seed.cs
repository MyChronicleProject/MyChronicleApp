using Microsoft.AspNetCore.Identity;
using MyChronicle.Domain;

namespace MyChronicle.Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            await SeedFamilyTrees(context);
            await SeedUsers(userManager);
            await SeedPersons(context);
        }
        private static async Task SeedFamilyTrees(DataContext context)
        {
            if (context.FamilyTrees.Any()) return;

            var familyTrees = new List<FamilyTree>
            {
                new FamilyTree { Name = "Example" }
            };

            await context.FamilyTrees.AddRangeAsync(familyTrees);
            await context.SaveChangesAsync();
        }

        private static async Task SeedPersons(DataContext context)
        {
            if (context.Persons.Any()) return;
            if (!context.FamilyTrees.Any()) return;

            var familyTreeId = context.FamilyTrees.First().Id;

            var persons = new List<Person> {
                new Person { BirthDate= new DateOnly(2000, 1, 1),
                    BirthPlace="London", FamilyTreeId=familyTreeId, Gender=Gender.Male, Name="Maria", LastName="Thomson" }
            };

            await context.Persons.AddRangeAsync(persons);
            await context.SaveChangesAsync();
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) return;

            var users = new List<AppUser>
            {
                new AppUser {
                    DisplayName = "John Doe", Email = "test@example.com", PasswordHash = "test" },
                new AppUser {
                    DisplayName = "Anna Nowak", Email="user@example.com", PasswordHash = "user" },
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user);
            }
        }


    }
}
