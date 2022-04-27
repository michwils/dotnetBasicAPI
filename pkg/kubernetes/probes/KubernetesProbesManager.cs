using Microsoft.AspNetCore.Builder;

namespace Kubernetes.Probes
{

  /// <summary>
  /// Call ConfigureProbes with a ref to WebApplication to setup probes
  ///  will read .env if loaded, if env variables are not set each probe setup will be skipped.
  ///</summary
  public sealed class KubernetesProbesManager
  {
    private ProbeStateManager _stateManager = ProbeStateManager.Instance;

    public ProbeStateManager ConfigureProbes(ref WebApplication app)
    {
      configureLiveness(ref app);
      configureReadiness(ref app);
      configureStartup(ref app);

      return this._stateManager;
    }

    private void configureLiveness(ref WebApplication app)
    {
      var path = DotNetEnv.Env.GetString("LIVENESS_PROBE");

      if (path is not null)
      {
        app.MapGet(path, async (c) =>
        {
          //TODO: Update this for future use change error to support status code, good enough for demo
          c.Response.StatusCode = !_stateManager.LivenessState.Error ? 200 : 500;
          await c.Response.WriteAsJsonAsync(_stateManager.LivenessState);
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
          await c.Response.WriteAsJsonAsync(this._stateManager.ReadinessState);
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
          await c.Response.WriteAsJsonAsync(this._stateManager.StartupState);
        });
      }
    }
  }
}