using Newtonsoft.Json;
using System.IO;

namespace ZaynBot
{
    public class ConfigBot
    {
        public string Token = "token bot principal...";
        public string TokenTeste = "token bot teste...";
        public string Prefix = "prefix bot principal...";
        public string PrefixTeste = "prefix bot teste...";
        public string TopGGKey = "Top.GG Key...";

        public static ConfigBot LoadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                ConfigBot config = new ConfigBot();
                config.SaveToFile(path);
                return null;
            }
            else
                using (var sr = new StreamReader(path))
                    return JsonConvert.DeserializeObject<ConfigBot>(sr.ReadToEnd());
        }

        public void SaveToFile(string path)
        {
            using (var sw = new StreamWriter(path))
                sw.Write(JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}