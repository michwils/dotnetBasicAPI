using Microsoft.AspNetCore.Builder;
using Kubernetes.Probes;

namespace TheApp
{

  public sealed class AppController
  {
    public AppController(TheApp context)
    {

      //TODO: Leaving empty for now but can change the context to be something more meaningful
      //context can be used to control state ie shutdown app or do some hard restart
    }

  }
}