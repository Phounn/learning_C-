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
    }
}
