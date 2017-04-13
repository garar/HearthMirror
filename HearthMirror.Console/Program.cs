using Newtonsoft.Json;

namespace HearthMirror.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            string command = ParseArguments(args);
            ExecuteCommand(command, true);
        }

        private static string ParseArguments(string[] args)
        {
            if (args.Length > 0)
            {
                return args[0];
            }
            else
            {
                return null;
            }
        }

        private static void ExecuteCommand(string command, bool main)
        {
            object value = null;

            switch (command)
            {
                case "cli":
                    while (command != "exit")
                    {
                        command = System.Console.ReadLine();
                        ExecuteCommand(command, false);
                    }
                    break;
                default:
                    var type = typeof(Reflection);
                    var method = type.GetMethod(command);
                    value = method?.Invoke(null, new object[0]);
                    break;
            }

            if (value != null)
            {
                System.Console.WriteLine(JsonConvert.SerializeObject(value));
                if (main)
                {
                    System.Environment.Exit(0);
                }
            }
            else
            {
                System.Console.WriteLine("Unable to read data.");
                if (main)
                {
                    System.Environment.Exit(1);
                }
            }
        }
    }
}
