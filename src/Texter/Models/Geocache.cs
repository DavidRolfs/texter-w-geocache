using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Texter.Models;
using Newtonsoft.Json.Linq;

namespace Texter.Models
{
    public class Geocache
    {
        public string formatted_address { get; set; }

        public static Geocache FindLocation(double Lat, double Lng)
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/");
            var request = new RestRequest("json?latlng=" + Lat + ", " + Lng + "&key=" + EnvironmentVariables.geoKey);
        //client.Authenticator = new HttpBasicAuthenticator(EnvironmentVariables.geoKey);
        var response = new RestResponse();
        Task.Run(async() =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
    JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            Console.WriteLine(jsonResponse["results"][0]);
            Console.WriteLine(jsonResponse["results"][0]["formatted_address"]);
            var CoordList = JsonConvert.DeserializeObject<Geocache>(jsonResponse["results"][0].ToString());
            return CoordList;
        }

        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
