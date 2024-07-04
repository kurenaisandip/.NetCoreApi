# Game Store API

## Starting the SQL Server
``` powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -e "MSSQL_PID=Evaluation" -v sqlvolume:/var/opt/mssql -p 1433:1433  --name sqlpreview --hostname sqlpreview -d --rm --name mssql mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04
```