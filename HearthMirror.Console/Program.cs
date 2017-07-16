using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                System.Console.WriteLine("Missing argument: cli, help or method name.");
                System.Environment.Exit(1);
                return null;
            }
        }

        private static void ExecuteCommand(string command, bool main)
        {
            object value = null;
            var type = typeof(Reflection);

            switch (command)
            {
                case "cli":
                    while (command != "exit")
                    {
                        command = System.Console.ReadLine();
                        ExecuteCommand(command, false);
                    }
                    break;
                case "help":
                    var methods = new List<MethodInfo>(type.GetMethods()).OrderBy(m => m.Name).ToList();
                    foreach (var method in methods)
                    {
                        System.Console.WriteLine($"Method: {method.Name}");
                    }
                    if (main)
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        return;
                    }
                    break;
                default:
                    value = type.GetMethod(command)?.Invoke(null, new object[0]);
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
