using CardMatchGame.Gameplay.Utils;
using CardMatchGame.UI.Utils;
using CardMatchGame.Utils;
using UnityEngine;

namespace CardMatchGame.Gameplay.UI.PopUps.GameOver
{
  public class WinWithTimeRecordText : MonoBehaviour, IPayloaded<NewTimeRecordPayload>
  {
    public PrintfText RecordTimeText;

    public void Payload(NewTimeRecordPayload payload) =>
      RecordTimeText.UpdateView(
        payload.OldTimeRecord.ToMinutesAndSeconds(),
        payload.NewTimeRecord.ToMinutesAndSeconds());
  }
}