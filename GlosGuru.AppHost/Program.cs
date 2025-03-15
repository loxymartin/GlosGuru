using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
                        .WithDataVolume(isReadOnly: false)
                        .WithPgAdmin();
                        
var glosgurudb = postgres.AddDatabase("GlosGuruDb");

var apiProj = builder.AddProject<GlosGuru_Api>("glosguru-api")
                                    .WithReference(glosgurudb);;

builder.AddProject<GlosGuru_Web>("glosguru-web")
                                    .WithReference(apiProj);

builder.Build().Run();
