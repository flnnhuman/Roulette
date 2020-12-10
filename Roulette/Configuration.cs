using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Roulette
{
    public sealed class Configuration
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public string ApiKey;


        internal static async Task<Configuration> LoadAsync(string filePath)
        {
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? "", filePath);

            if (string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine(nameof(filePath) + "is null");

                return null;
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine(filePath + "does not exist");
                return null;
            }

            Configuration configuration;

            try
            {
                var json = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);

                if (string.IsNullOrEmpty(json))
                {
                    Console.WriteLine($"{nameof(json)} is empty");

                    return null;
                }

                configuration = JsonConvert.DeserializeObject<Configuration>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return null;
            }

            switch (configuration)
            {
                case null:
                    Console.WriteLine(nameof(configuration) + "is null");

                    return null;
                default:
                    return configuration;
            }
        }
    }
}