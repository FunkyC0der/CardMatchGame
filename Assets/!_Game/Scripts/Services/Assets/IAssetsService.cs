using CardMatchGame.Services.Levels;
using CardMatchGame.Services.UI;

namespace CardMatchGame.Services.Assets
{
  public interface IAssetsService
  {
    void Load();
    LevelsData LevelsData();
    UIAssetsData UIAssetsData();
  }
}