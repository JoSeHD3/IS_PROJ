using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDataChartRepresentation
{
    internal class RestDataRetriever
    {
        private readonly HttpClient httpClient;

        public RestDataRetriever()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetDataFromAddress(string address)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(address);
                response.EnsureSuccessStatusCode();

                string responseData = await response.Content.ReadAsStringAsync();
                return responseData;
            }
            catch (HttpRequestException ex)
            {

                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

    }
}
