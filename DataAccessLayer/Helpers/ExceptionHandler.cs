using System;

namespace DataAccessLayer.Helpers
{
    public static class ExceptionHandler
    {
        public static void ShowInConsole(string className, string methodName, Exception exception)
        {
            Console.WriteLine($"\nClassName - {className}\nMethodName - {methodName}" +
                              $"\nException message: {exception.Message}. \nException stack trace: {exception.StackTrace}");
        }
    }
}
