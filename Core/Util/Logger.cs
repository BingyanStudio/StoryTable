using System;

namespace StoryTable
{
    public static class Logger
    {
        public static Action<string> Message = s => Console.WriteLine($"Message: {s}");
        public static Action<string> Warning = s => Console.WriteLine($"Warning: {s}");
        public static Action<string> Error = s => Console.WriteLine($"Error: {s}");
    }
}
