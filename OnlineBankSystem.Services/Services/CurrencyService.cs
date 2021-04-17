using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Services.Services
{
    public class CurrencyService : ICurrencyService
    {
        private const string BaseUri = "http://data.fixer.io/api/";
        public FixerOptions _config { get; private set; }

        public CurrencyService(IOptions<FixerOptions> options)
        {
            _config = options.Value;
        }

        public async Task<ConvertModel> Convert(string from, string to, decimal amount)
        {
            from = from.ToUpper();
            to = to.ToUpper();

            var url = GetFixerUrl(from, to, amount);

            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return ParseData(await response.Content.ReadAsStringAsync(), from, to);
        }

        private ConvertModel ParseData(string data, string from, string to)
        {
            var root = JObject.Parse(data);

            var rates = root.Value<JObject>("rates");
            var fromRate = rates.Value<decimal>(from);
            var toRate = rates.Value<decimal>(to);

            var rate = toRate / fromRate;

            return new ConvertModel(from, to, rate);
        }

        private string GetFixerUrl(string from, string to, decimal amount)
        {
            return $"{BaseUri}latest?access_key={_config.AccessKey}&from={from}&to={to}&amount={amount}";
        }
    }
}
