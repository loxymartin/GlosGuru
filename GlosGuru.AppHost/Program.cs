var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GlosGuru>("glosguru");

builder.Build().Run();
