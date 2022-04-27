using Microsoft.AspNetCore.Builder;

namespace Kubernetes.Probes
{
  public class KubernetesProbes
  {
    private ProbeStatus _livnessStatus = new ProbeStatus();
    public void ConfigureProbes(ref WebApplication app)
    {
      configureLiveness(ref app);
    }

    private void configureLiveness(ref WebApplication app)
    {
      app.MapGet("/healthz", async (c) =>
      {
        //TODO: Update this for future use change error to support status code, good enough for demo
        c.Response.StatusCode = !_livnessStatus.Error ? 200 : 500;
        await c.Response.WriteAsJsonAsync(_livnessStatus);
      });
    }
  }
}