public class Herbo : Animal, IKind
{
    public int Kindness { get; set; }
    public Herbo(int food, int number, string name, int health, int kindness) : base(food, number, name, health)
    {
        Kindness = kindness;
    }

}