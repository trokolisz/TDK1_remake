using System;
using System.Formats.Asn1;
using System.Net.Http;
using System.Threading.Tasks;


class Program
{
    public static async Task Main(string[] args)
    {
        // API endpoint - replace with the correct endpoint and parameters
        string apiUrl = "https://re.jrc.ec.europa.eu/api/v5_2/PVcalc";

        // Sample query parameters - adjust these to fit your needs
        string query = "?lat=48.85&lon=2.35&peakpower=1&loss=14&angle=35&aspect=0";

        using (HttpClient client = new HttpClient())
        {
            // Full URL with query parameters
            string url = apiUrl + query;

            try
            {
                // Send the GET request
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Get the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Print the result
                Console.WriteLine("API Response:");
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                // Handle errors
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }
}
