using MyChronicle.Domain;

namespace MyChronicle.Infrastructure
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            await SeedFamilyTrees(context);
            //await SeedUsers(context);
            await SeedPersons(context);
        }
        private static async Task SeedFamilyTrees(DataContext context)
        {
            if (context.FamilyTrees.Any()) return;

            var familyTrees = new List<FamilyTree>
            {
                new FamilyTree { Id=1, Name = "Example" }
            };

            await context.FamilyTrees.AddRangeAsync(familyTrees);
            await context.SaveChangesAsync();
        }

        private static async Task SeedPersons(DataContext context)
        {
            if (context.Persons.Any()) return;

            var persons = new List<Person> {
                new Person { Id=1, BirthDate= new DateOnly(2000, 1, 1), BirthPlace="London", FamilyTreeId=1, Gender=Gender.Male, Name="Maria", LastName="Thomson" }
            };
            
            await context.Persons.AddRangeAsync(persons);
            await context.SaveChangesAsync();
        }

        private static async Task SeedUsers(DataContext context)
        {
            if (context.Users.Any()) return;

            var users = new List<User>
            {
                new User { Id = 1, FirstName = "John", LastName = "Doe", Login = "test", PasswordHash = "test" },
                new User { Id = 2, FirstName = "Anna", LastName = "Nowak", Login="user", PasswordHash = "user" },
            };

            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }


    }
}
