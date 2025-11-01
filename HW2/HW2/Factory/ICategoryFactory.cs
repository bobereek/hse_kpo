namespace HW2
{
    public interface ICategoryFactory
    {
        Category CreateCategory(int id, string name, CategoryType type);
    }
}