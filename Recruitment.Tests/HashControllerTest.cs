using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Recruitment.API.Controllers;
using Recruitment.Contracts;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Recruitment.API.Models;
using Xunit;

namespace Recruitment.Tests
{
    public class HashControllerTest
    {
        [Fact]
        public async void PostTest()
        {
            var authContract = new AuthContract { Login = "Login", Password = "123456" };
            var authHashContract = new AuthHashContract { HashValue = "hashhashhashhash" };

            var mockFactory = new Mock<IHttpClientFactory>();
            var handlerMock = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(authHashContract), Encoding.UTF8, "application/json"),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);


            var httpClient = new HttpClient(handlerMock.Object);
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);


            AppSettings app = new AppSettings() { ApiUrls = new ApiUrls(){ MD5Function = "http://test"} }; 
            var mockOptions = new Mock<IOptions<AppSettings>>();
           
            mockOptions.Setup(ap => ap.Value).Returns(app);

            var hashController = new HashController(mockFactory.Object, mockOptions.Object);

            var result = await hashController.PostAsync(authContract);

            var actual = JsonConvert.SerializeObject(((OkObjectResult)result).Value);
            var expected = JsonConvert.SerializeObject(authHashContract);


            Assert.Equal(expected, actual);

        }
    }
}
