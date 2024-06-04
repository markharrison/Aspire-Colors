# Aspire-Colors

.NET Aspire is an opinionated, cloud-ready stack for building observable, production-ready, distributed applications.

This project is a .NET Aspire / Blazor version of [ColorsWeb](https://github.com/markharrison/ColorsWeb) & [ColorsAPI](https://github.com/markharrison/ColorsAPI). 


## ColorsX

It includes:

- ColorsX API app ... API includes returning a random color

- ColorsX Web ... displays random light colors driven by calls to API.  Uses Blazor Server. 

- ColorsX WebX ... displays random light colors driven by calls to API.  Uses Blazor WebAssembly. 

![alt text](docs/image-1.png)


## Azure Developer CLI

You can use the Azure Developer CLI to easily provision Azure Container Apps.

Either clone the repo to desktop or use the CodeSpaces.


Use a command prompt at the route directory

Install the aspire workload (sudo may not be required)

```
sudo dotnet workload update
sudo dotnet workload install aspire
sudo dotnet workload list
```

Logon to Azure and initialise Azure Developer CLI to inspect the application.

```
azd auth login
azd init
```

![alt text](docs/image-2.png)

To provision the Azure Container Apps environment 

```
azd provision
```

![alt text](docs/image-3.png)

To deploy the application to Azure Container Apps 

```
azd deploy
```

![alt text](docs/image-4.png)

The output will give links to 
- ColorsX Web application
- Aspire Dashboard

![alt text](docs/image-5.png)

The ColorsX API has its ingress set to not allow access from outside the Azure Container Apps environment.  This needs to be changed if you want to see the Swagger page.  

The ColorsX Web app uses Blazor Server - the API call is within the Azure Container Apps environment.

The ColorsX WebX app uses Blazor WebAssembly - the APIs calls are from the web browser and cannot acces the API app. To allow this, the ingress needs amending to allow external access.  Will need to change the environment variable to point to the new url of API-service.


![alt text](docs/image-6.png)


# Scale up 

You can change the number of lights by amending the Colors URL - append `/{numberoflights}`

Example - for 500 lights

https:// webfrontend.icybush-c6c1e5ad.uksouth.azurecontainerapps.io/colors/500


Use this to drive a high transaction rate.  Notice that the number of replicas of the API App scale up to accommodate the high transaction rate.  it is set to allow between 1 and 10.

## Tidy up 

You can shutdown / delete Azure resources with 

```
azd down
```
