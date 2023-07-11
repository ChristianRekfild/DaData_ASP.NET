using Dadata;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebApi_DaData
{
    public class DaDataHelper
    {
        public string API_Token {
            get
            {
                return _API_Token;
            }
        }
        string _API_Token;

        public string Secret
        {
            get
            {
                return _secret;
            }
        }
        private string _secret;

        private SuggestClientAsync api;

        public DaDataHelper()
        {
            
            //string pathToCurrDir = Directory.GetCurrentDirectory();
            //string pathToKeyFile = pathToCurrDir + "\\Key.txt";

            //if (!File.Exists(pathToKeyFile))
            //    throw new Exception("Файл с ключом для API DaData не был найден. Обратитесь к программисту. Или богу...");

            //using (StreamReader reader = new StreamReader(pathToKeyFile))
            //{
            //    string api_key = reader.ReadLine();

            //    if (string.IsNullOrWhiteSpace(api_key))
            //        throw new Exception("Ключ к API DaData отсутствует в файле или был стёрт");

            //    string secret = reader.ReadLine();

            //    if (string.IsNullOrWhiteSpace(api_key))
            //        throw new Exception("Секретная часть ключа была стёрта");

                _API_Token = "1a80f10a797f1b54cca792934de41e9c05c32e0d";
            //}

            api = new SuggestClientAsync(_API_Token);
            //api = new CleanClientAsync(token, secret);
        }

        public async Task<string> GetInfoForINN(string inputData)
        {
            var outData = await api.FindParty(inputData);
            int a = 0;

            string result = string.Empty;

            for (int i = 0; i < outData.suggestions.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(outData.suggestions[i].value))
                    continue;

                result += $"Название: {outData.suggestions[i].value}\n";
            }

            if (string.IsNullOrWhiteSpace(result))
                return "Нифига не найдено, Товарищь!";

            return result;
        }

    }
}
