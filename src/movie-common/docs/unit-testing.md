
# Unit Testing (xUnit)
In this movie we currently put unit test on the `service` level because its contains main logic of our API. We have to know that this `service` is performs as we expected or not based on valid or invalid input.<br/>

## Mocking
Instead of we use actual data from database we should create the alternate inmemorial data to use in our unit test project. This can be done by Mocking our repositories and then `setup` all the actual returns according to what actually it behaves `but with inmemorial data, not real data from dbContext`.<br/>

You can see the example of Mocking AdminRepository in `Admin.Api.UnitTest` project in `Mocks` folder > `MockRepo.cs`.<br/> 
As you can see, there are two functions of this repo that are overrided. The goal is `whenever thoose functions are being called by services it will process the overrided setup and not the actual repo`, because currently unit test projects are **unable** to access another parties like middleware or dbContext. <br/>
With this **Mocks** we can exclude parts or modules that we dont really need in testing our project, we can aim straights to test our logics. *Just mocks them and we will free to go*.

## Creating Test
Test functions in unit test porjects are defined by atrribute `[Facts]`. And to compare the actual result with our expected result is using `Assert`.<br/>
Follow this link for detail info :
https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/test-aspnet-core-services-web-apps

**Function Name Convention :**<br/>
Example :
```c#
[Fact]
public async void GetOneByAdminId_ValidId_NotNull()
{...}

[Fact]
public async void GetOneByAdminId_InvalidId_ThrowsException()
{...}
```
Explanation :
- `GetOneByAdminId` : function name of the service we want to test
- `ValidId` or `InvalidId` : the cases, wheter we want to try a valid or invalid input
- `NotNull` or `ThrowsException` : the expected result given by case we implement

> More links : https://www.thecodebuzz.com/read-appsettings-json-in-net-core-test-project-xunit-mstest/

## Run Test Project
1. Open CLI in root folder of the movie (where the .sln file is located)
2. Type `dotnet test`
3. Hit Enter