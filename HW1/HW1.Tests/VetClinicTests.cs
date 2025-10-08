namespace HW1.Tests;

public class VetClinicTests
{
    [Fact]
    public void IsHealthyTest()
    {
        var clinic = new VetClinic();
        var animal = new TestAnimal(10, 1, "Animal", 10);
        var result = clinic.IsHealthy(animal);
        Assert.True(result);

        animal.Health = 0;
        result = clinic.IsHealthy(animal);
        Assert.False(result);
    }
}
