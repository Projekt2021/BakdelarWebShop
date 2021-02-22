# ITHS-Webshop
Bakeshop Project

Project framework version :
Bakdelar - version  .Net5.0
Bakdelar_API - version  .Net Core3.1
DataAccess - version .Net Core3.1

Steps
1.Initially execute command :
    - add-migration initialcreate
    - update-database
  You need to execute this command twice, once for Bakdelar project and then for Bakdelar_API.
  Reason being - In Bakdelar project Authentication tables are created 
               - And in Bakdelar_API project, project tables are created.
               
2.In Bakdelar project in appsettings.json - configure your API endpoint Url on which your API service is running.
    "APIEndpoint": "https://localhost:5005/"

3. All product images will be saved in this folder -
       "ProducImagePath":  "images\\product\\"



