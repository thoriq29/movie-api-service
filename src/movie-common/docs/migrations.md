# Migrations
This project is come with default **Player model** (entity) for example purpose. This model may not be match with what you inteded to. So in this section you will have to look in to your project requirements, and update this model by adding migration.
This example on how to add migration to update Player model :
1. In ``src/Game.Common/Models/PlayerModel`` you will find the model and the type configuration
2. Edit them both according to your requirements
3. Try to fix error(s) if there some of them appears.
<br/>***note**: 
<br/>you can delete ***PlayerController*** or ***PlayerService*** in **admin-api** and ***PlayerRepository*** in **Game.Common** if you feel they are useless. (don't panic, this stuff are for example purpose, so you can savely remove them from your project. If you need example, you can take a look on Admin services instead)
4. Once the model are updated, open CMD and locate in **Game.Common**
5. Input command `dotnet ef migrations add <MigrationName>`
<br/>example : `dotnet ef migrations add UpdatePlayerModel`
6. Once it success try to run your service, then take a look in your player table in the database. It should be automacaly update according to what you update in the player model.
7. Okay youre done, go get some coffee and happy coding!

**Note** <br/>
To be able use `ef command` you probably need to install dotnet-ef tools, simply by execute this command :
```cmd
dotnet tool install --global dotnet-ef
```

More info : https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/