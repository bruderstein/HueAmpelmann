using BuildHueService;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;


namespace TeamCityTrafficLightsConfigurator.Management
{
    public class Lights
    {
        public Lights(string bridgeIP)
        {
            this.bridgeIP = bridgeIP;
        }

        public string DeveloperTest()
        {


            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/newdeveloper", Method.GET);

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            //todo return normal result
            return content;
        }


        public string CreateNewDeveloper(string deviceType, string username)
        {


            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { devicetype = deviceType, username = username });

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string



            //todo return normal result
            return content;
        }

        public List<int> GetIds(string developerName)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights", Method.GET);
            request.AddUrlSegment("username", developerName);
            request.RequestFormat = DataFormat.Json;
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var ser = new JavaScriptSerializer();
            var teststr = "{\"1\":{\"name\": \"Hue Lamp 1\"},\"2\":{\"name\": \"Hue Lamp 2\"},\"3\":{\"name\": \"Hue Lamp 3\"}}";
            var list = ser.Deserialize<Dictionary<string, Dictionary<string, string>>>(teststr);

            var ids = new List<int>();
            foreach(var item in list)
            {
                int id;
                var isInt = Int32.TryParse(item.Key, out id);
                if (isInt)
                {
                    ids.Add(id);
                }
                

            }

            return ids;
        }

        public string GetAll(string developerName)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights", Method.GET);
            request.AddUrlSegment("username", developerName);
            request.RequestFormat = DataFormat.Json;
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            
            var ser = new JavaScriptSerializer();
            var teststr = "{\"1\":{\"name\": \"Hue Lamp 1\"},\"2\":{\"name\": \"Hue Lamp 2\"},\"3\":{\"name\": \"Hue Lamp 3\"}}";
            var list = ser.Deserialize<Dictionary<string, Dictionary<string, string>>>(teststr);

            //todo return normal result
            return content;
            
        }

        public string Get(string developerName, int lightId)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights/{lightId}", Method.GET);
            request.AddUrlSegment("username", developerName);
            request.AddUrlSegment("lightId", developerName);

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //todo return normal result
            return content;

        }

        public string TurnOn(string developerName, int lightId)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights/{lightId}/state", Method.PUT);
            request.AddUrlSegment("username", developerName);
            request.AddUrlSegment("lightId", lightId.ToString());

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { on = true });

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //todo return normal result
            return content;
        }

        public string TurnOff(string developerName, int lightId)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights/{lightId}/state", Method.PUT);
            request.AddUrlSegment("username", developerName);
            request.AddUrlSegment("lightId", lightId.ToString());

            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { on = false });

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //todo return normal result
            return content;
        }

        public string ChangeColour(string developerName, int lightId, int sat, int bri, int hue)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights/{lightId}/state", Method.PUT);
            request.AddUrlSegment("username", developerName);
            request.AddUrlSegment("lightId", lightId.ToString());


            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { on = true, sat = sat, bri = bri, hue = hue, transitiontime= 2 });

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //todo return normal result
            return content;
        }

        public string ChangeColour(string developerName, int lightId, int sat, int bri, LightColour hue)
        {
            var client = new RestClient(bridgeIP);

            var request = new RestRequest("api/{username}/lights/{lightId}/state", Method.PUT);
            request.AddUrlSegment("username", developerName);
            request.AddUrlSegment("lightId", lightId.ToString());


            request.RequestFormat = DataFormat.Json;
            request.AddBody(new { on = true, sat = sat, bri = bri, hue = hue.Hue, transitiontime = 2 });

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            IRestResponse response = client.Execute(request);
            var content = response.Content;

            //todo return normal result
            return content;
        }


        //public string ChangeColour(string developerName, int lightId, Color color)
        //{
        //    var client = new RestClient(bridgeIP);

        //    var request = new RestRequest("api/{username}/lights/{lightId}/state", Method.PUT);
        //    request.AddUrlSegment("username", developerName);
        //    request.AddUrlSegment("lightId", lightId.ToString());


        //    request.RequestFormat = DataFormat.Json;
        //    var hue = Math.Round(color.GetHue() * 182.04);
        //    var bri = Math.Round(color.GetBrightness());
        //    var sat = Math.Round(color.GetSaturation());
        //    request.AddBody(new { on = true, sat = sat.ToString(), bri = bri.ToString(), hue = hue.ToString(), transitiontime = 1 });

        //    request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

        //    IRestResponse response = client.Execute(request);
        //    var content = response.Content;

        //    //todo return normal result
        //    return content;
        //}

        public void CycleColours(string developerName)
        {

        }

        private string bridgeIP;
 
        public void RestSharpTest()
        {
            var client = new RestClient("http://www.thomas-bayer.com");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("sqlrest/CUSTOMER/", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", 123); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // add files to upload (works with compatible verbs)
            //request.AddFile(path);

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // or automatically deserialize result
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            //RestResponse<Person> response2 = client.Execute<Person>(request);
            //var name = response2.Data.Name;

            //// easy async support
            //client.ExecuteAsync(request, response =>
            //{
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response =>
            //{
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();
        }


    }

   
}
