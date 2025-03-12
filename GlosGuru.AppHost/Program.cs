var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GlosGuru_Web>("glosguru");

builder.AddProject<Projects.GlosGuru_Api>("glosguru-api");

builder.Build().Run();
