namespace TheApp
{

  public class AppState
  {
    public bool IsError { get; set; } = false;
    public string Description { get; set; } = string.Empty;

    public string SomeOtherAppDetail { get; set; } = string.Empty;
  }
}