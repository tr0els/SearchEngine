using CommonStuff;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleSearch
{
    public class App
    {

        HttpClient _client = new HttpClient();

        public App()
        {
        }

        public void RunAsync()
        {
            //SearchLogic mSearchLogic = new SearchLogic(new Database());

            Console.WriteLine("Console Search");
            
            while (true)
            {
                Console.WriteLine("enter search terms - q for quit");
                string input = Console.ReadLine();
                if (input.Equals("q")) break;

                var query = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var response = _client.GetAsync("https://localhost:44307/search/" + String.Join(",",query) + "/10");
                var resultStr = response.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<SearchResult>(resultStr);

                //var result = mSearchLogic.Search(query, 10);

                if (result.Ignored.Count > 0) {
                    Console.WriteLine("Ignored: ");
                    foreach (var aWord in result.Ignored)
                    {
                        Console.WriteLine(aWord + ", ");
                    }
                }

                int idx = 0;
                foreach (var doc in result.DocumentHits) {
                    Console.WriteLine("" + (idx+1) + ": " + doc.Document.mUrl + " -- contains " + doc.NoOfHits + " search terms");
                    Console.WriteLine("Index time: " + doc.Document.mIdxTime + ". Creation time: " + doc.Document.mCreationTime);
                    Console.WriteLine();
                    idx++;
                }
                Console.WriteLine("Documents: " + result.Hits + ". Time: " + result.TimeUsed.TotalMilliseconds);
            }
        }
    }
}
