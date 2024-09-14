using System.Collections.Generic;
using CardMatchGame.Services.Levels;
using CardMatchGame.Services.UI;
using UnityEngine;

namespace CardMatchGame.Services.Assets
{
  public interface IAssetsService
  {
    void Load();
    LevelsData LevelsData();
    UIAssetsData UIAssetsData();
  }
}