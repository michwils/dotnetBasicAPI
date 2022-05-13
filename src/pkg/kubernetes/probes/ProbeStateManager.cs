using Microsoft.AspNetCore.Builder;

namespace Kubernetes.Probes
{
  public sealed class ProbeStateManager
  {
    private ProbeStateManager() { }

    private static readonly Lazy<ProbeStateManager> _instance = new Lazy<ProbeStateManager>(
      () => new ProbeStateManager());

    public static ProbeStateManager Instance
    {
      get
      {
        return _instance.Value;
      }
    }

    public ProbeStatus LivenessState
    {
      get; set;
    } = new ProbeStatus();

    public ProbeStatus ReadinessState
    {
      get; set;
    } = new ProbeStatus();

    public ProbeStatus StartupState
    {
      get; set;
    } = new ProbeStatus();
  }
}