# Project Assembly Name
Changing project assembly name (YourProjectName) is used to marking what project you are working on. This will set the base **namespace** you will use in the project. 
The default assembly name of this movie is **YourProjectName** (you can find this in each csproj of the projects), so the namespace of the projects will go like this :
- **Agate.YourProjectName.Admin.Api**
- **Agate.YourProjectName.GameServer.Api**
- **Agate.YourProjectName.GameServer.Common**<br/>

So you should change this into something else.

## How to

So you need to set this assembly by using the name of the project you are working on right now (ex : `BNILearning`, `Accenture`, etc) without spacing. 
<br/>This is simple, you only need to change phrase "YourProjectName" into something globally. And thats it.
1. From Visual Studio or VS Code, press `CTRL + Shift + H`
2. Replace `YourProjectName` in to something else 
3. Then hit Enter to change it all
4. And bum, youre done! :D
5. But make sure your *.csproj* is updated too, this will make you easier to create new class in the project if you are using Visual Studio.
6. After that you better try to build and run your service, both admin-api and gameserver-api to see if it work or not. (gl!)

> Note that changing this assembly name will change the `dll file` too. So you have to inform **devops** about what name you use in your project just in case there is somethings missing.

> **Dont forgot to put this name into `ci/cd variables`.**

# Solution Name (.sln)
The current name of the solution of this movie is **AgateBackend.sln**. Since there are new rules that are applied in this version, this solution name is `mandatory` to be change too. Each quests should have different solution name.
1. Locate file **AgateBackend.sln**, then change the file name manually (ex. Accenture.sln, BNILearning.sln or something else)
2. Open file `.gitlab-ci-test-dotnet.yml`, find DOTNET_SLN_NAME and change the value according to what you named the solution 
3. Then Save.

> **Its better to give solution name equals to assembly name**