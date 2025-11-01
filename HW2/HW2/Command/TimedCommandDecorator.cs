using System;
using System.Diagnostics;

namespace HW2
{
    public class TimedCommandDecorator : ICommand
    {
        private readonly ICommand _inner;
        private readonly string _name;

        public TimeSpan LastElapsed { get; private set; }

        public TimedCommandDecorator(ICommand inner, string name)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _name = name ?? inner.GetType().Name;
        }


        public void Execute()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                _inner.Execute();
            }
            finally
            {
                sw.Stop();
                LastElapsed = sw.Elapsed;
                Console.WriteLine($"[TIMING] {_name} executed in {LastElapsed.TotalMilliseconds} ms");
            }
        }
    }
}
