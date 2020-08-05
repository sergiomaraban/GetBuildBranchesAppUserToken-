using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace GetBuildBranchesApplication
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();

        private static void Main()
        {
            AppCenter.Start("4f429339-14cc-499b-a5f4-83186d7cb016",typeof(Analytics), typeof(Crashes));
            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task<IEnumerable<string>> GetItems(string path)
        {
            var response = await Client.GetAsync(path);
            

            if (!response.IsSuccessStatusCode) return null;

            return new List<string>();
        }

        private static async Task RunAsync()
        {
            // Update your local service port no. / service APIs etc in the following line
            Client.BaseAddress = new Uri("https://api.appcenter.ms/v0.1/apps/sergey/me/branches");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var items = await GetItems("https://api.appcenter.ms/v0.1/apps/sergey/me/branches");
                Console.WriteLine("Items read using the web api GET");
                Console.WriteLine(string.Join(string.Empty, items.Aggregate((current, next) => current + ", " + next)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
