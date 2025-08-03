var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql").WithLifetime(ContainerLifetime.Persistent);
var db = sql.AddDatabase("auth");

var username = builder.AddParameter("username", secret: true);
var password = builder.AddParameter("password", secret: true);
var rabbitmq = builder.AddRabbitMQ("rabbitmq", username, password).WithManagementPlugin();

builder
    .AddProject<Projects.Api>("api")
    .WithReference(rabbitmq)
    .WaitFor(rabbitmq)
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
