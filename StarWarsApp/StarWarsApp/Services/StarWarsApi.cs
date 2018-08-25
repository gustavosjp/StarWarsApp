using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StarWarsApp.Models;

namespace StarWarsApp.Services
{
    public class StarWarsApi
    {
        readonly string _api_base_url = "https://swapi.co/api";

        public async Task<List<string>> GetAllCategoriesAsync()
        {
            using (var client = new HttpClient())
            {
                //grab json from server
                var json = await GetJsonData($"{_api_base_url}/");

                //Deserialize json
                var items = JsonConvert.DeserializeObject<List<string>>(json);

                //return the items
                return items;
            }
        }

        public async Task<List<People>> GetPeoplesAsync()
        {
            //grab json from server
            var json = await GetJsonData($"{_api_base_url}/people");

            //Deserialize json
            var result = JsonConvert.DeserializeObject<PeopleResult>(json);

            //return the items
            return result.results;

        }

        public async Task<People> GetPeopleAsync(string url)
        {
            //grab json from server
            var json = await GetJsonData($"{url}");

            //Deserialize json
            var people = JsonConvert.DeserializeObject<People>(json);

            //return the items
            return people;
        }

        public async Task<List<Species>> GetSpeciesAsync()
        {
            //grab json from server
            var json = await GetJsonData($"{_api_base_url}/species");

            //Deserialize json
            var list = JsonConvert.DeserializeObject<List<Species>>(json);

            //return the items
            return list;
        }

        public async Task<Species> GetSpecieAsync(int id)
        {

            //grab json from server
            var json = await GetJsonData($"{_api_base_url}/species/{id}");

            //Deserialize json
            var species = JsonConvert.DeserializeObject<Species>(json);

            //return the items
            return species;
        }

        async Task<string> GetJsonData(string url)
        {
            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                return json;
            }
        }
    }
}