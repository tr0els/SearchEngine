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
                Console.WriteLine("Press enter to start sending requests to load balancer - q for quit");
                string input = Console.ReadLine();
                if (input.Equals("q")) break;

                for (int i = 0; i < 1000; i++)
                {
                    Task.Delay(1).Wait();
                    GetUrl(input);
                }

                Console.WriteLine("Done!");
            }
        }

        public async void GetUrl(string input)
        {
            Console.WriteLine($"{DateTime.Now.TimeOfDay} - Console sending request to load balancer");

            Task<string> getStringTask = _client.GetStringAsync("https://localhost:44306/DidYouMean");
            
            var content = await getStringTask;
            var result = JsonConvert.DeserializeObject<DidYouMean>(content);
            
            Console.WriteLine($"{DateTime.Now.TimeOfDay} - Console got response from load balancer");

            //return result;
        }
    }
}
