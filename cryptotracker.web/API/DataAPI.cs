using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.entity;
using Newtonsoft.Json.Linq;

namespace cryptotracker.web.API
{
    public class DataAPI
    {
        private string URL = "https://api.coincap.io/v2/assets";
        private readonly HttpClient httpClient;

        public DataAPI()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(URL);
        }

        public async Task<List<CryptoCurrency>> GetAll()
        {
            List<CryptoCurrency> cryptoCurrencies = new List<CryptoCurrency>();
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("https://api.coincap.io/v2/assets");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);

                    JArray assets = (JArray)data["data"];

                    foreach (JToken asset in assets)
                    {
                        CryptoCurrency cryptoCurrency = new CryptoCurrency();
                        cryptoCurrency.Id = (string)asset["id"];
                        cryptoCurrency.Rank = (string)asset["rank"];
                        cryptoCurrency.Symbol = (string)asset["symbol"];
                        cryptoCurrency.Name = (string)asset["name"];
                        cryptoCurrency.Price = ((decimal)asset["priceUsd"]).ToString("0.00");
                        cryptoCurrency.Supply = ((decimal)asset["supply"]).ToString("0.00");
                        cryptoCurrency.Volume24Hr = ((decimal)asset["volumeUsd24Hr"]).ToString("0.00");
                        cryptoCurrency.ChangePercentFor24Hr = ((decimal)asset["changePercent24Hr"]).ToString("0.00");
                        cryptoCurrencies.Add(cryptoCurrency);
                    }
                }
                else
                {
                    // Handle the error response
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    // TODO: Handle the error appropriately
                }
            }
            catch (Exception ex)
            {
                // Handle any exception that occurred
                // TODO: Handle the exception appropriately
            }

            return cryptoCurrencies;
        }

        public async Task<List<CryptoCurrency>> GetTrending()
        {
            List<CryptoCurrency> tempList = new List<CryptoCurrency>();
            tempList = await GetAll();
            List<CryptoCurrency> trendList = tempList
                .Where(c => !c.ChangePercentFor24Hr.StartsWith("-"))
                .OrderByDescending(c => Convert.ToDouble(c.ChangePercentFor24Hr))
                .Take(3)
                .ToList();
            return trendList;
        }

        public async Task<List<CryptoCurrency>> GetDumping()
        {
            List<CryptoCurrency> tempList = new List<CryptoCurrency>();
            tempList = await GetAll();
            List<CryptoCurrency> trendList = tempList
                .Where(c => c.ChangePercentFor24Hr.StartsWith("-"))
                .OrderByDescending(c => Convert.ToDouble(c.ChangePercentFor24Hr))
                .TakeLast(3)
                .ToList();
            return trendList;
        }
    }
}
