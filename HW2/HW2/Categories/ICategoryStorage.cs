namespace HW2
{
    public interface ICategoryStorage
    {
        void AddCategory(int id, string name, CategoryType type);
        void DeleteCategory(int id);
        Category? GetCategory(int id);

        int GetOrCreateCategory(string name, CategoryType type);
        List<Category> GetAllCategories();
    }
}