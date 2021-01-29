using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Recruitment.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Recruitment.Functions
{
    public static class MD5Function
    {
        [FunctionName("MD5Function")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req)
        {

            string authContractJson = await new StreamReader(req.Body).ReadToEndAsync();

            var hash = GetMD5Hash(authContractJson);

            AuthHashContract authHashContract = new AuthHashContract { HashValue = hash };

            return new OkObjectResult(authHashContract);
        }

        private static string GetMD5Hash(string source)
        {
            string hash;
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
            return hash;

        }
    }


}
