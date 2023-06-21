using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDataChartRepresentation
{
    internal class AuthService
    {
        private string authenticationEndpoint = "http://127.0.0.1:8083/auth";

        public async Task<string> AuthenticateAndGetToken(string username, string password)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var requestBody = new
                    {
                        Username = username,
                        Password = password
                    };

                    var jsonRequestBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                    var request = new HttpRequestMessage(HttpMethod.Post, authenticationEndpoint);
                    request.Content = new StringContent(jsonRequestBody, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
                        string token = responseObject.token;
                        return token;
                    }
                    else
                    {
                        throw new Exception("Authentication failed");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public string FakeAuthenticateAndGetRole(string username, string password)
        {
            if (username == "asd" && password == "asd") return "Admin";
            if (username == "123" && password == "123") return "Mod";
            return null;
        }
    }

}
