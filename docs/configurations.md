# **Configurations & Secrets**
Appsettings.json & secrets.json

# Configurations
Configuration files are used to store configuration settings such as database connections strings, any application scope global variables, and etc.<br/>
The general configuration file that we all have already known is `appsettings.json`. In this template this `appsettings` is **centralized and shared between projects**. It means that default configuration is identically same between `(Admin)Movie.Api` and `Movie.Api`.<br/>
You can find config files in `Movie.Common` with each environment :
- Appsettings.Common.json
- Appsettings.Common.Debug.json
- Appsettings.Common.Development.json
- and so on.

## How To
Lets say if you want to change connections string for only `(Admin)Movie.Api` or only `Movie.Api` please create new file json in the on of the project with name :
- `Appsettings.json` (Release environment)
- `Appsettings.Debug.json` (Debug environment / local)
- `Appsettings.Development.json` (Development environment)
- and so on
Then you can start to register what config do you want to change (lets say connections string) and leave the other value. The default/other value config will remain using from Common so you dont have to register all of those configs (except for what value you want to change). <br/>

For more understanding this, you can open `WebHost.cs` in `gameserver-core/AspNetCore/Hosting/WebHost.cs` end see what happend in this file started in line 119. You can also find the list of environment we used in this template from there.

# User Secrets
Unlike appsettings, user secrets should hold sensitive configuration for the projects such as passwords or secret keys or etc. This secret will mount locally on computer machine and not built-in with source code. <br/>The simplest example is that `appsettings will be pushed into git (because it is a part of source code) so the value can be revealed, but user secrets isn't`. This how we can hide our sensitive configuration savely.

## How To
In this template there are several configs that we need to hide. You can peek in `secret.json.TEMPLATE.txt` in `Movie.Common` to see the list and the format of them.<br/>
There are several ways to set user secrets.
- If you are using Visual Studio IDE 
    - Just right click on `Movie.Common` or `(Admin)Movie.Api` or `Movie.Api` and choose Manage User Secrets <br/>
        *this will open up a file called `secrets.json`* 
    - Copy and paste configs from `secrets.json.TEMPLATE.txt` into that file
    - Dont forget to change its value
    - Then Save it
- If you are Visual Studio Code *lover*
    - Using CLI 
        - Open CMD located in `Movie.Common` or `(Admin)Movie.Api` or `Movie.Api` path
        - Type `dotnet user-secrets list` then Enter.<br/>
        you should see no secrets configured for the apps, because it is. You need to add the configs one by one 
        - What you need to set is this below : 
            - `dotnet user-secrets set "ConnectionStrings:DbPassword" "5rPQvkBWjFBEpzjyYDBBvWV4F_yrBH"`
            - `dotnet user-secrets set "MovieServerSecret:ChecksumKey" "XXX"`
            - `dotnet user-secrets set "MovieServerSecret:JWTSecret" "abcdefghijklmnoprstuvwxyz11234567890"`
        <br/> as you can see `dotnet user secrets set` command takes two parameters, first param is for the name/key of the config and second param is its value. So you need to change all of the value into something else. Do not set it with this dummy values. *You just need generate random strings then do the command above, it will overwrites the configs.*
        - After you finish set it all, type `dotnet user-secrets list` again, and the list will be like this
        ```
        MovieServerSecret:JWTSecret = abcdefghijklmnoprstuvwxyz11234567890
        MovieServerSecret:ChecksumKey = XXX
        ConnectionStrings:DbPassword = 5rPQvkBWjFBEpzjyYDBBvWV4F_yrBH
        ```
    - Using Visual Studio Extension *(the simple one)*
        - Open Visual Studio Code Extensions (`Ctrl+Shift+X`)
        - Search for `.Net Core User Secrets` by *Adrian Wilczyńsk*
        - Then click install
        - Now you can simply rigth click on `Movie.Common.csproj` or `(Admin)Movie.Api.csproj` or `Movie.Api.csproj` then click Manage User Secret
        - Copy and paste configs from `secrets.json.TEMPLATE.txt` into that file
        - Dont forget to change its value
        - Then Save it

# Using Google Secret (GCP Secrets)
To use GCP secret instead of conventional user secret in your debug environment, you need to follow this step :
- First, open CI/CD variables in your git repository (https://gategit.agate.id/tech/agate-open-source/backend-template/game-backend-template/-/settings/ci_cd)
- Look for variable named `GCP_SERVICE_ACCOUNT`
- *Reveal* it values, then `copy` it
- Create new .json file (ex `service-account.json`) in your computer
- Paste the value from `GCP_SERVICE_ACCOUNT` then Save it into any directory you want (NOT in the project) <br/>
  this file is used for authorize your project into GCP service, so your project can access secrets value on it. Now we need to register this file into our project.
- Go back to project, open file **launchSetting.json** both in `(Admin)Movie.Api` and `Movie.Api` : <br/>
  `- src/game-api/Properties/launchSetting.json`<br/>
  `- src/admin-api/Properties/launchSetting.json`<br/>
- Change the path of `GOOGLE_APPLICATION_CREDENTIALS` into your new path
- In this `launchSetting.json` file you can see your default `PROJECT_ID` too, you can ask `devops team` if you need to change this or not
- Save the file, and you are set
- Remember to inform `devops team` the values you want to store in GCP secret, the default value of the secret is this below : <br/>
```json
{
    "ConnectionStrings:DbPassword": "5rPQvkBWjFBEpzjyYDBBvWV4F_yrBH",
    "MovieServerSecret:JWTSecret": "abcdefghijklmnoprstuvwxyz11234567890",
    "MovieServerSecret:ChecksumKey": "XXX"
}
```


> **Important Note** 
> This user secrets is currently shared between project. It means only one config available for both (Admin)Movie.Api and Movie.Api. (Unlike appsettings > that can be separated/overwrited in (Admin)Movie.Api or Movie.Api).
> <br/> But we are still looking forward to improve this issue.