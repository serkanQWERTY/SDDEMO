using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Application.Configuration
{
    public class AppConfigData
    {
        public string jwtSecretKey { get; set; }

        public int jwtExpire { get; set; }

        public static AppConfigData GetAppConfig()
        {
            string rootPath = Directory.GetCurrentDirectory();
            string pathForConfigData = rootPath + "/" + "AppConfigurationData.json";

            using (StreamReader streamReader = new StreamReader(pathForConfigData))
            {
                string json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<AppConfigData>(json);
            }
        }
    }
}
