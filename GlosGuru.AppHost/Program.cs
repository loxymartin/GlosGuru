using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                        .WithPgAdmin();
                        
var postgresdb = postgres.AddDatabase("postgresdb");

var apiProj = builder.AddProject<GlosGuru_Api>("glosguru-api")
                                    .WithReference(postgresdb);;

builder.AddProject<GlosGuru_Web>("glosguru-web")
                                    .WithReference(apiProj);

builder.Build().Run();
