namespace HW2
{
    public class IdGenerator
    {
        public static int Generate()
        {
            return DateTime.Now.Millisecond + new Random().Next(1000, 9999);
        }
    }

}