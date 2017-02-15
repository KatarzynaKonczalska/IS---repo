using System;
using System.Threading.Tasks;
using System.Json;
using System.IO;
using System.Text;
using System.Net.Http;

namespace MApp.REST
{
    public enum GetTypes
    {
        GetAll, GetMagazine, GetAsset, GetSectorAssets
    }

    public class RESTconnection
    {
        string ServerUrl, RESTUrl;
        HttpClient client;

        public RESTconnection(string ServerUrl)
        {
            this.ServerUrl = ServerUrl;
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<JsonValue> GetData(GetTypes DownloadDataType, string id = "")
        {
            // DONE: GetData
            switch (DownloadDataType)
            {
                case GetTypes.GetAll:
                    {
                        RESTUrl = "/api/magazine?format=json";
                    }
                    break;
                case GetTypes.GetMagazine:
                    {
                        RESTUrl = "/api/magazine/" + id;
                    }
                    break;
                case GetTypes.GetAsset:
                    {
                        RESTUrl = "/api/asset/" + id;
                    }
                    break;
                case GetTypes.GetSectorAssets:
                    {
                        RESTUrl = "/api/asset/sector/" + id;
                    }
                    break;
            }

            using (HttpResponseMessage response = await client.GetAsync(ServerUrl + RESTUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    string stream = await response.Content.ReadAsStringAsync();
                    JsonValue data = JsonObject.Parse(stream);
                    return data;
                }
                throw new Exception("Error witch connecting to server.");
            }
        }

        public async Task<string> SendData(string Data, string id)
        {
            // TODO: Poprawic
            RESTUrl = "/api/asset/" + id + "/update";

            using (HttpResponseMessage response = await client.PutAsync(ServerUrl + RESTUrl, new StringContent(Data, Encoding.UTF8, "application/json")))
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

            //RESTUrl = "/api/asset/" + id + "/delete";
            RESTUrl = "/api/asset/nfc/123/delete";

            using (HttpResponseMessage response = await client.DeleteAsync(ServerUrl + RESTUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return message;
                }
                throw new Exception("Error witch connecting to server.");
            }
        }

        public async Task<string> GenerateId(string ServerTrigger = "{}")
        {
            RESTUrl = "/api/asset/add";

            using (HttpResponseMessage response = await client.PostAsync(ServerUrl + RESTUrl, new StringContent(ServerTrigger, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    JsonValue id = JsonObject.Parse(message);
                    message = id["id"].ToString().Trim('"');
                    return message;
                }
                throw new Exception("Error witch connecting to server.");
            }
        }
    }
}