using System.Runtime.Intrinsics.Arm;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ColorsX_ApiService>("apiservice");

var webfrontend = builder.AddProject<Projects.ColorsX_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

var webfrontendx = builder.AddProject<Projects.ColorsX_WebX>("webfrontendwasm")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

//webfrontendwasm.WithEnvironment("api_url", "https://apiservice.internal.livelyforest-318b32fc.uksouth.azurecontainerapps.io/");

builder.Build().Run();
