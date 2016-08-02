using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GPSOAuthSharp.NetCore.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Google account email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Password: ");
            string password = Console.ReadLine();

#pragma warning disable CS4014
            run(email, password);
#pragma warning restore CS4014
            Console.ReadKey();
        }

        public static async Task run(string email, string password)
        {
            GPSOAuthClient client = new GPSOAuthClient(email, password);
            var master = await client.PerformMasterLogin();
            Console.WriteLine("Master Login:");
            Console.WriteLine(JsonConvert.SerializeObject(master, Formatting.Indented));

            if (master.ContainsKey("Token"))
            {
                Console.WriteLine("\nOAuth Login:");
                var token = master["Token"];
                var oath = await client.PerformOAuth(token, "sj", "com.google.android.music",
                    "38918a453d07199354f8b19af05ec6562ced5788");
                Console.WriteLine(JsonConvert.SerializeObject(oath, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("MasterLogin failed (check credentials)");
            }
            Console.ReadKey();
        }
    }
}
