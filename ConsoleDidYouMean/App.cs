using Newtonsoft.Json;

namespace ConsoleDidYouMean
{
    public class App
    {
        HttpClient _client = new HttpClient();

        public void Run()
        {
            Console.WriteLine("Console DidYouMean");
            
            while (true)
            {
                Console.WriteLine("enter a word - q for quit");
                string input = Console.ReadLine();
                if (input.Equals("q")) break;

                for (int i = 0; i < 10; i++)
                {
                    GetUrl(input);
                }

                Console.WriteLine("Done!");
            }
        }

        public async void GetUrl(string input)
        {
            Console.WriteLine($"Console sending request to load balancer at {DateTime.Now.TimeOfDay}");

            Task<string> getStringTask = _client.GetStringAsync("https://localhost:44306/DidYouMean");
            
            var content = await getStringTask;
            var result = JsonConvert.DeserializeObject<DidYouMean>(content);
            
            Console.WriteLine($"Console got response from load balancer at {DateTime.Now.TimeOfDay}");

            //return result;
        }
    }
}
