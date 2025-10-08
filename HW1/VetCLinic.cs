public class VetClinic : IClinic
{
    public bool IsHealthy(Animal animal)
    {
        return animal.Health > 5;
    }

    public VetClinic() { }
}