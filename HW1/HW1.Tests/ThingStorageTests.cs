namespace HW1.Tests;

public class ThingStorageTests
{
    [Fact]
    public void AddThingTest()
    {
        var storage = new ThingStorage();
        var Thing = new TestThing(1, "Thing");
        storage.AddThing(Thing);
        Assert.Collection(storage.Things, thing => Assert.Equal(Thing, thing));
    }
}