namespace CardMatchGame.Services.Settings
{
  public interface ISettingsService
  {
    SettingsData Settings { get; }

    void Save();
    void Load();
  }
}