using CardMatchGame.UI.Utils;
using CardMatchGame.Utils;
using UnityEngine;

namespace CardMatchGame.Gameplay.UI.PopUps.GameOver
{
  public class WinText : MonoBehaviour, IPayloaded<float>
  {
    public PrintfText TimeText;
    
    public void Payload(float timeElapsed) => 
      TimeText.UpdateView(timeElapsed.ToMinutesAndSeconds());
  }
}