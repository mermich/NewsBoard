Ce projet sert a gerer la couche business, qui s'expose par lib mais aussi par api rest pour les test notamment.

pour la migration:
dans package manager console
se placer dans le repertoire NewBoardRestApi a coups de cd

pour l'initialisation faire:
dotnet ef migrations add init

pour un update:
dotnet ef migrations add <nomDeUpdate>


pour migreer la databse:
dotnet ef database update



sur vs2017
se placer dans le repertoire NewBoardRestApi/db a coups de cd
Add-Migration <nomDeUpdate>
Update-Database