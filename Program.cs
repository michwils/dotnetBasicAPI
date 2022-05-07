using Kubernetes.Probes;
using TheApp;

//init
DotNetEnv.Env.Load();

var probes = new KubernetesProbesManager();

var builder = WebApplication.CreateBuilder(args);

//builder setup
builder.Host.ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(30));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

//app.Urls.Add("http://*:5555");
//app.UseHttpsRedirection();

//quick tests - line used for testing git actions

//setup
var stateManager = probes.ConfigureProbes(ref app);

var appboot = new TheApp.TheApp();

var controller = appboot.ConfigureApp(ref app, stateManager);

stateManager.LivenessState = new ProbeStatus { IsError = false, Description = "App Running" };


stateManager.StartupState = new ProbeStatus { IsError = false, Description = "App Started" };

app.Run();
