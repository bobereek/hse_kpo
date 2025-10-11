public class ThingStorage : IThingStorage
{
    public ThingStorage() { }
    private List<Thing> storage = new List<Thing>();

    public List<Thing> Things => storage;
    public void CheckStorage()
    {
        foreach (var thing in storage)
        {
            Console.WriteLine("Inventory: Thing Name=" + thing.Name + " No=" + thing.Number);
        }
    }

    public void AddThing(Thing thing)
    {
        storage.Add(thing);
    }
}