class TestAnimal : Animal
{
    public TestAnimal(int food, int number, string name, int health)
        : base(food, number, name, health) { }
}

public class TestKindAnimal : Herbo
{
    public TestKindAnimal(int food, int number, string name, int health, int kindness)
        : base(food, number, name, health, kindness)
    {
    }
}

public class TestPredatorAnimal : Predator {
    public TestPredatorAnimal(int food, int number, string name, int health)
        : base(food, number, name, health)
    {
    }
}

public class TestThing : Thing
{
    public TestThing(int number, string name)
        : base(number, name)
    {
    }
}