using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bitcoinRate currentBitcoin = GetRates();
            Console.WriteLine("enter the amount of bitcoins");
            float userCoins = float.Parse(Console.ReadLine());
            Console.WriteLine("Select currency EUR/GBP/USD");
            string userCurrency = Console.ReadLine().ToUpper();

            float currencyCoinRate = 0;
            string currencyCode = "";

            float currentCoinRate = 0;
            if(userCurrency == "EUR")
            {
                currentCoinRate = currentBitcoin.bpi.EUR.rate_float;
                currencyCode = currentBitcoin.bpi.EUR.code;
            }
            else if (userCurrency == "USD")
            {
                currentCoinRate = currentCoinRate.bpi.USD.rate_float;
                currencyCode = currentBitcoin.bpi.USD.code;

            }
           // Console.WriteLine($"current rate: {currentBitcoin.bpi.USD.code} {currentBitcoin.bpi.USD.rate_float}");
            Console.WriteLine($"{currentBitcoin.disclaimer}");
        }

        public static bitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            bitcoinRate bitcoinData;

            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<bitcoinRate>(response);
            }

            return bitcoinData;

        }
    }
}
