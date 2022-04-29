using Microsoft.AspNetCore.Builder;
using Kubernetes.Probes;

namespace TheApp
{

  public class TheApp
  {
    protected AppState State { get; set; } = new AppState();

    public TheApp()
    {
      this.State = new AppState { IsError = false, Description = "App constructing...", SomeOtherAppDetail = "Stage A" };
    }

    public AppController ConfigureApp(ref WebApplication app, ProbeStateManager psm)
    {
      var path = DotNetEnv.Env.GetString("APPURL_GETDATA", "/GETDATAz");

      app.MapGet(path, async (c) =>
      {
        //TODO: Correct below
        c.Response.StatusCode = 200;
        await c.Response.WriteAsJsonAsync(this.State);
      });

      var path2 = DotNetEnv.Env.GetString("APPURL_SECONDAPI", "/APPURL_SECONDAPIz");

      app.MapGet(path2, async (c) =>
      {
        //TODO: Correct below
        c.Response.StatusCode = 200;
        await c.Response.WriteAsJsonAsync(this.State);
      });


      psm.ReadinessState = new ProbeStatus { IsError = false, Description = "App has initialized and ready for traffic." };
      this.State = new AppState { IsError = false, Description = "App Initialized ...", SomeOtherAppDetail = "Stage B" };


      return new AppController(this);
    }

  }
}