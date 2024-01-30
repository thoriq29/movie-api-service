# Game Backend Movie

![version](https://img.shields.io/badge/Version-2.0.0-brightgreen)
![status](https://img.shields.io/badge/Status-Maintained-brightgreen)
![contribution](https://img.shields.io/badge/Contribution-Open-brightgreen)
![.NetCore](https://img.shields.io/badge/NetCore-6.0.1-blue)

> preserve this movie version badge on readme when overwriting readme
>
> ![movie version](https://img.shields.io/badge/Version-2.0.0-brightgreen)
>
> **[Changelogs](docs/changelog.md)**

<hr/>

## Movie Usage Details

**Important!** <br/>`If you are new to this movie, you better read all of this stuff before you start development your quest.`

- [Movie Overview](docs/movie-overview.md)
- [Configurations & Secrets](docs/configurations.md)
- [Migrations](docs/migrations.md)
- [Project Assembly & Solution Name](docs/assembly-name.md)
- [Unit Testing (xUnit)](docs/unit-testing.md)
- [SonarQube Integration](docs/sonar-scanner-cli-implementation.md)

## Tools

To be able to run this project you probably need install some tools and IDE and start developing.<br/>
`*Note : if you have expert with this before, you can savely skip this section`

- **If you use Visual Studio IDE**

  - If you use latest Visual Studio, make sure you have installed **ASP.NET and web development** modules for Visual Studio. If you haven't please see this step below :
    - Open visual studio installer and with your visual studio selected, choose Modify.
    - Look in **Web & Cloud** section in Worload tab, find **ASP.NET and web development** then make sure to check it
    - Then click install
  - If you have **ASP.NET and web development** installed, now you have to check your **dotnet SDKs**. This movie is now using .NET version 6.0 so you have to install the coresponded SDKs to be able to run the app. Follow this step :
    - Open visual studio installer and with your visual studio selected, choose Modify.
    - In tab **Individual Components** then search for **.NET** section and then choose **.NET 6.0 Runtime**
    - If this **.NET 6.0 Runtime** have already checked then you are ready to go, but if not try to check it and then click Install
    - This will install the .NET SDK and all of the corresponding runtimes into your Visual Studio, so you dont have to be worries about the runtimes.<br/>

- **If you use VS Code or else<br/>**
  You need to do things manually, but this time you will need to install the **runtimes** and the SDKs as well - First, check your list **dotnet SDKs** by typing `dotnet --list-sdks` in CMD - You need to find in the list like **6.0.\* [*path*]** if doesnt any, you have to install it later - Now you need to check dotnet runtimes by typing `dotnet --list-runtimes` in CMD - Then look for this things - **Microsoft.AspNetCore.App 6.0.1** - **Microsoft.NETCore.App 6.0.1** - **Microsoft.WindowsDesktop.App 6.0.1** (if only you use Windows) - If thoose things haven't installed (including the SDK) please download and install the SDKs and the runtimes here https://dotnet.microsoft.com/en-us/download/dotnet/6.0

## Quick Start

How to start Local Development :

1. Restore the Solution
   - Open Command Prompt, and locate to the root folder of this project
   - Type `dotnet restore`,
   - Then hit Enter
2. Start mysql services **version 8** or newest
   - To check your mysql version execute this sql `SHOW VARIABLES LIKE 'version';`
3. Point your connection string to your services at `appsettings.Common.Debug.json` located in `src/Game.Common/`
   - This configs are **shared** and used by both admin-api and gameserver-api.
   - You can separate appsettings between admin-api and gameserver-api by creating new `appsettings.Debug.Json` in `src/Game.Api` or `src/Admin.Api`
4. Configure launchSetting.json if needed
5. Configure [User Secret](docs/configurations.md)
6. Start your server
   - using Visual Studio :
     - set Startup project to what service do you want to run
     - select launch setting profiles to `AdminServer_Service` or `GameAPI-Services`. **not IIS Express**
   - using CLI, `dotnet watch run`

## Contribution

1. Fork the Project
2. Create Merge Request to master branch of this repo and assign to Maintainer (@unickverse)
3. Wait for Approval or Revision

Force Pipeline
