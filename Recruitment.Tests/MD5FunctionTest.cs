using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using Recruitment.Contracts;
using Recruitment.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Recruitment.Tests
{
    public class HashFunctionTest
    {
        [Fact]
        public async void MD5FunctionTest()
        {
            var expected = "98219D96B37AD76F19AE12D45E07B487";
            object result = await MD5Function.Run(HttpRequestSetup(null, "loginandpassword"));

            var authHashContract = (AuthHashContract)((OkObjectResult)result).Value;
            var actual = authHashContract.HashValue;

            Assert.Equal(expected, actual);

        }


        public HttpRequest HttpRequestSetup(Dictionary<String, StringValues> query, string body)
        {
            var reqMock = new Mock<HttpRequest>();

            reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(body);
            writer.Flush();
            stream.Position = 0;
            reqMock.Setup(req => req.Body).Returns(stream);
            return reqMock.Object;
        }
    }
}
