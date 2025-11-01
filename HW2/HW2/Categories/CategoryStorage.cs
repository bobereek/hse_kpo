namespace HW2
{
    public class CategoryStorage : ICategoryStorage
    {
        private Dictionary<int, Category> Categories = new Dictionary<int, Category>();
        public ICategoryFactory categoryFactory;

        public CategoryStorage(ICategoryFactory categoryFactory)
        {
            this.categoryFactory = categoryFactory;
        }

        public void AddCategory(int id, string name, CategoryType type)
        {
            if (id <= 0)
                throw new ArgumentException("Category ID must be positive", nameof(id));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Category name cannot be empty", nameof(name));

            if (Categories.ContainsKey(id))
                throw new InvalidOperationException($"Category with id {id} already exists");

            Categories.Add(id, categoryFactory.CreateCategory(id, name, type));
        }

        public void DeleteCategory(int id)
        {
            if (!Categories.ContainsKey(id))
                throw new ArgumentException($"Category with id {id} not found", nameof(id));

            Categories.Remove(id);
        }

        public Category? GetCategory(int id)
        {
            return Categories.TryGetValue(id, out var category) ? category : null;
        }

        public int GetOrCreateCategory(string name, CategoryType type)
        {
            var existingCategories = GetAllCategories();
            var category = existingCategories.FirstOrDefault(c => c.Name == name && c.Type == type);

            if (category != null)
                return category.Id;

            var newCategoryId = IdGenerator.Generate();
            AddCategory(newCategoryId, name, type);
            return newCategoryId;
        }

        public List<Category> GetAllCategories()
        {
            return Categories.Values.ToList();
        }


    }
}