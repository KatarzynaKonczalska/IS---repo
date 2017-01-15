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
        GetAll, GetMagazine, GetAsset, SendAll, SendMagazine, SendAsset, DeleteAll, DeleteMagazine, DeleteAsset
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
            }

            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(RESTUrl));
            //request.ContentType = "application/json";
            //request.Method = "GET";

            //using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            //{
            //    if (response.StatusCode == HttpStatusCode.OK)
            //    {
            //        using (Stream stream = response.GetResponseStream())
            //        {
            //            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            //            return jsonDoc;
            //        }
            //    }
            //    throw new Exception(response.StatusCode.ToString());
            //}

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
            // TODO: Poprawic linki do uploadu
            /*
            switch (SendDataType)
            {
                case ConnectionTypes.SendAll:
                    {
                        RESTUrl = "/api/magazine";
                    }
                    break;
                case ConnectionTypes.SendMagazine:
                    {
                        RESTUrl = "/api/magazine[" + id + "]";
                    }
                    break;
                case ConnectionTypes.SendAsset:
                    {
                        RESTUrl = "/api/asset[" + id + "]";
                    }
                    break;
            }
            */

            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(RESTUrl));
            //request.ContentType = "application/json";
            //request.Method = "POST";

            //using (Stream response = await request.GetRequestStreamAsync())
            //{
            //    using (var writer = new StreamWriter(response))
            //    {
            //        writer.Write(Data.ToString());
            //        writer.Flush();
            //        writer.Dispose();
            //    }
            //}
            //using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            //{
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        using (var reader = new StreamReader(stream))
            //        {
            //            return reader.ReadToEnd();
            //        }
            //    }
            //}

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

        public async Task<string> DeleteData(ConnectionTypes DeleteDataType, long id = 0)
        {
            // TODO: Poprawic linki do kasowania
            /*
            switch (DeleteDataType)
            {
                case ConnectionTypes.DeleteAll:
                    {
                        RESTUrl = "/api/magazine";
                    }
                    break;
                case ConnectionTypes.DeleteMagazine:
                    {
                        RESTUrl = "/api/magazine[" + id + "]";
                    }
                    break;
                case ConnectionTypes.DeleteAsset:
                    {
                        RESTUrl = "/api/asset[" + id + "]";
                    }
                    break;
            }
            */

            //HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(RESTUrl));
            //request.ContentType = "application/json";
            //request.Method = "DELETE";

            //using (Stream response = await request.GetRequestStreamAsync())
            //{
            //    using (var writer = new StreamWriter(response))
            //    {
            //        writer.Write(Data.ToString());
            //        writer.Flush();
            //        writer.Dispose();
            //    }
            //}
            //using (HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse)
            //{
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        using (var reader = new StreamReader(stream))
            //        {
            //            return reader.ReadToEnd();
            //        }
            //    }
            //}

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
