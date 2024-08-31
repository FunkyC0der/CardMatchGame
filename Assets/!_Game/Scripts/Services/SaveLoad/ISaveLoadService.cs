using CardMatchGame.Services.Progress;

namespace CardMatchGame.Services.SaveLoad
{
  public interface ISaveLoadService
  {
    void Save(ProgressData data);
    ProgressData LoadProgressData();
  }
}