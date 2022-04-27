using Kubernetes.Probes;

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

app.UseHttpsRedirection();

//setup
var stateManager = probes.ConfigureProbes(ref app);

stateManager.LivenessState = new ProbeStatus { Error = false, Description = "App Running" };

stateManager.ReadinessState = new ProbeStatus { Error = false, Description = "App ready for traffic" };
stateManager.StartupState = new ProbeStatus { Error = false, Description = "App Started" };

app.Run();
