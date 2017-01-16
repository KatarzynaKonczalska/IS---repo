using System;
using System.Threading.Tasks;
using System.Json;
using System.IO;
using System.Net.Http;
using System.Text;

namespace MApp.REST
{
    public enum ConnectionTypes
    {
        GetAll, GetMagazine, GetAsset, GetSectorAssets, SendAll, SendMagazine, SendAsset, DeleteAll, DeleteMagazine, DeleteAsset
    }
    public class RESTconnection
    {
        string ServerUrl, RESTUrl;
        HttpClient client;

        public RESTconnection(string ServerUrl)
        {
            this.ServerUrl = ServerUrl;
            client = new HttpClient();
            client.BaseAddress = new Uri(ServerUrl);
        }

        public async Task<JsonValue> GetData(ConnectionTypes DownloadDataType, long id = 0)
        {
            // DONE: GetData
            switch (DownloadDataType)
            {
                case ConnectionTypes.GetAll:
                    {
                        RESTUrl = "/api/magazine";
                    }
                    break;
                case ConnectionTypes.GetMagazine:
                    {
                        RESTUrl = "/api/magazine[" + id + "]";
                    }
                    break;
                case ConnectionTypes.GetAsset:
                    {
                        RESTUrl = "/api/asset[" + id + "]";
                    }
                    break;
                case ConnectionTypes.GetSectorAssets:
                    {
                        RESTUrl = "/api/asset/sector/[" + id + "]";
                    }
                    break;
            }

            using (HttpResponseMessage response = await client.GetAsync(RESTUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        JsonValue Data = await Task.Run(() => JsonObject.Load(stream));
                        return Data;
                    }
                }
                throw new Exception("Error witch connecting to server.");
            }
        }

        public async Task<string> SendData(ConnectionTypes SendDataType, JsonValue Data, long id = 0)
        {
            // DONE: SendData
            RESTUrl = "/api/asset/add";

            using (HttpResponseMessage response = await client.PostAsync(RESTUrl, new StringContent(Data.ToString(), Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                throw new Exception("Error witch connecting to server.");
            }
        }

        public async Task<string> DeleteData(long id)
        {
            // DONE: DeleteData
            
            RESTUrl = "/api/asset/[" + id + "]/delete";

            using (HttpResponseMessage response = await client.DeleteAsync(RESTUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                throw new Exception("Error witch connecting to server.");
            }
        }

        public async Task<string> GenerateId()
        {
            // TODO: Link do generowania id
            RESTUrl = "";

            using (HttpResponseMessage response = await client.GetAsync(RESTUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    string GeneratedId = await response.Content.ReadAsStringAsync();
                    return GeneratedId;
                }
                throw new Exception("Error witch connecting to server.");
            }
        }
    }
}
