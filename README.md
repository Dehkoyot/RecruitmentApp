# RecruitmentApp

Test task for Intersog company, Singapure client.

Screening test
Please develop a small solution in C# to demonstrate your approach to developing .NET projects. Please follow the specification outlined below and provide the source code before the interview.
The specification:

- Create a new solution and name it Recruitment.sln
- Initiate a new git repository with .gitignore and commit changes
- Create a new API project and name it Recruitment.API in .NET Core 3.1
- Create a new Azure Functions v3 project and name it Recruitment.Functions
- Create a new library project and name it Recruitment.Contracts for keeping the contract models to exchange between services
- Create a new test project and name it Recuitment.Tests
- Create a new API endpoint in the API project with the following address POST /api/hash
- Create a new C# model which will be posted to this endpoint. It should serialize to the JSON of the following structure: { “login”: “Example login”, “password”: “Example password” }
- The API project will pass this model further as a client to an Azure Function
- The Azure Function will calculate the MD5 value based on the login and password values and return it to the API project. The JSON returned from the endpoint should have the following structure as the contract model:
- {“hash_value” : “4004583eb8fb7f89”} Please write tests with mocks to cover this solution

