namespace WebApplication1.Logging;

public class Logging : ILogging
{
    public void Log(string message, string type)
    {
        if (type == "error")
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("Error --> " + message);
        }
        else if (type == "info")
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Info --> " + message);
        }
        else if (type == "warning")  
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning --> " + message);
        }
    }
}