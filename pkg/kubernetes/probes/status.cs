using Microsoft.AspNetCore.Builder;

namespace Kubernetes.Probes
{

  public class ProbeStatus
  {
    public bool Error { get; set; } = false;
    public string Description { get; set; } = string.Empty;

  }
}