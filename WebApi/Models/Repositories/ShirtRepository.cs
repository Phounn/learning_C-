namespace WebApi.Models.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt{ShirtId = 1, Brand = "Nike", Color ="Blue", Size = 10, Gender="Men", Price = 100},
            new Shirt{ShirtId = 2, Brand = "My Brand", Color ="Red", Size = 11, Gender="women", Price = 110},
            new Shirt{ShirtId = 3, Brand = "Your Brand", Color ="Yello", Size = 12, Gender="Men", Price = 200},
            new Shirt{ShirtId = 4, Brand = "Our Brand", Color ="Pink", Size = 13, Gender="women", Price = 300},
        };
        public static List<Shirt> GetAllShirts()
        {
            return shirts;
        }
        public static bool ShirtExists(int id) => shirts.Any(s => s.ShirtId == id);
        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(s => s.ShirtId == id);
        }
        public static Shirt? GetShirtByProperties(string? brand, string? gender, string? color, int? size)
        {
            return shirts.FirstOrDefault(x =>
                !string.IsNullOrWhiteSpace(brand) &&
                !string.IsNullOrWhiteSpace(x.Brand) &&
                x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&

                !string.IsNullOrWhiteSpace(gender) &&
                !string.IsNullOrWhiteSpace(x.Gender) &&
                x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&

                !string.IsNullOrWhiteSpace(color) &&
                !string.IsNullOrWhiteSpace(x.Color) &&
                x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
                size.HasValue

            );
        }
        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirts.Max(s => s.ShirtId);
            shirt.ShirtId = maxId + 1;
            shirts.Add(shirt);
        }
        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirts.First(s => s.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Price= shirt.Price;
            shirtToUpdate.Size= shirt.Size;
            shirtToUpdate.Color= shirt.Color;
            shirtToUpdate.Gender= shirt.Gender;
        }
        public static void DeleteShirt(int id)
        {
            var shirt = GetShirtById(id);
            if(shirt != null)
            {
                shirts.Remove(shirt);
            }
        }
    }
}
