using Microsoft.AspNetCore.Builder;

namespace Kubernetes.Probes
{
  public class KubernetesProbes
  {
    private ProbeStatus _livnessStatus = new ProbeStatus();
    private ProbeStatus _readinessStatus = new ProbeStatus();
    private ProbeStatus _startupStatus = new ProbeStatus();
    public void ConfigureProbes(ref WebApplication app)
    {
      configureLiveness(ref app);
      configureReadiness(ref app);
      configureStartup(ref app);
    }

    private void configureLiveness(ref WebApplication app)
    {
      var path = DotNetEnv.Env.GetString("LIVENESS_PROBE");

      if (path is not null)
      {
        app.MapGet(path, async (c) =>
        {
          //TODO: Update this for future use change error to support status code, good enough for demo
          c.Response.StatusCode = !_livnessStatus.Error ? 200 : 500;
          await c.Response.WriteAsJsonAsync(_livnessStatus);
        });
      }
    }

    private void configureReadiness(ref WebApplication app)
    {
      var path = DotNetEnv.Env.GetString("READINESS_PROBE");

      if (path is not null)
      {
        app.MapGet(path, async (c) =>
        {
          //TODO: Correct below
          c.Response.StatusCode = 200;
          await c.Response.WriteAsJsonAsync(_readinessStatus);
        });
      }
    }

    private void configureStartup(ref WebApplication app)
    {
      var path = DotNetEnv.Env.GetString("STARTUP_PROBE");

      if (path is not null)
      {
        app.MapGet(path, async (c) =>
        {
          //TODO: Correct below
          c.Response.StatusCode = 200;
          await c.Response.WriteAsJsonAsync(_startupStatus);
        });
      }
    }
  }
}