using System.Runtime.Intrinsics.Arm;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ColorsX_ApiService>("apiservice");

var webfrontend = builder.AddProject<Projects.ColorsX_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

var webfrontendx = builder.AddProject<Projects.ColorsX_WebX>("webfrontendx")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
