using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace SearchAPI.Controllers
{
    [ApiController]
    [Route("search")]
    public class SearchController : ControllerBase
    {
        private static SearchLogic searchLogic = new SearchLogic(new Database());

        [HttpGet]
        [Route("{query}/{maxAmount}")]
        public string SearchByQuery(string query, int maxAmount)
        {
            
            var result = searchLogic.Search(query.Split(","), maxAmount);
            var resultStr = JsonConvert.SerializeObject(result, Formatting.Indented);
            return resultStr;
        }
    }
}
