var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql");

var db = sql.AddDatabase("auth");

builder.AddProject<Projects.Api>("api").WithReference(db).WaitFor(db);

builder.Build().Run();
