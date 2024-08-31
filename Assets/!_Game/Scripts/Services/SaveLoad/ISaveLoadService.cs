using CardMatchGame.Services.Progress;
using CardMatchGame.Services.Settings;

namespace CardMatchGame.Services.SaveLoad
{
  public interface ISaveLoadService
  {
    void Save(ProgressData data);
    ProgressData LoadProgressData();

    void Save(SettingsData data);
    SettingsData LoadSettingsData();
  }
}