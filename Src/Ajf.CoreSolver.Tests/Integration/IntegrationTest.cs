using NUnit.Framework;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Ajf.CoreSolver.WebApi.Tests.Controllers
{
    [TestFixture]
    public class IntegrationTest
    {
        const string BaseUrl = "http://localhost/Ajf.CoreSolver.WebApi/";

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = new System.Uri(BaseUrl);
            //client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            //request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = client.Execute(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return default(T);// response.Data;
        }

        [Test]
        [Category("Integration")]
        public  async Task ThatApiCanBeCalledLocally()
        {
            //// Arrange
            //var restClient = new RestClient();
            //var request = new RestRequest(Method.GET);
            //request.Resource = "api/values";

            //// Act
            //var result = Execute<Call>(new RestRequest());

            //// Assert
            ////Assert.IsNotNull(result);
            ////Assert.AreEqual("Home Page", result.ViewBag.Title);
            var target = getTarget();
            var client = new RestClient("http://"+target+"/Ajf.CoreSolver.WebApi/");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            var request = new RestRequest("api/values", Method.GET);
            //request.AddParameter("name", "value"); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

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
            //client.ExecuteAsync(request, response => {
            //    Console.WriteLine(response.Content);
            //});

            //// async with deserialization
            //var asyncHandle = client.ExecuteAsync<Person>(request, response => {
            //    Console.WriteLine(response.Data.Name);
            //});

            //// abort the request on demand
            //asyncHandle.Abort();
        }

        private object getTarget()
        {
            if (System.Environment.MachineName.ToLower() == "ajf-build-01")
                return "ajf-qa-02";

            return "localhost";
        }
    }
    public class Call
    {
        public string Sid { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string CallSegmentSid { get; set; }
        public string AccountSid { get; set; }
        public string Called { get; set; }
        public string Caller { get; set; }
        public string PhoneNumberSid { get; set; }
        public int Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int Flags { get; set; }
    }
}
