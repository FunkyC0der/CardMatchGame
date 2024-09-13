using CardMatchGame.Services.Levels;

namespace CardMatchGame.Services.Assets
{
  public interface IAssetsService
  {
    void Load();
    LevelsData LevelsData();
  }
}