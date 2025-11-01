namespace HW2
{


    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Unknown";
        public CategoryType Type { get; set; }

        public Category(int id, string name, CategoryType type) => (Id, Name, Type) = (id, name, type);
    }
}