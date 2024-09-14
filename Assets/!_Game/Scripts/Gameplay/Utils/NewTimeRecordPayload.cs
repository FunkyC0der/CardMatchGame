namespace CardMatchGame.Gameplay.Utils
{
  public struct NewTimeRecordPayload
  {
    public readonly float OldTimeRecord;
    public readonly float NewTimeRecord;

    public NewTimeRecordPayload(float oldTimeRecord, float newTimeRecord)
    {
      OldTimeRecord = oldTimeRecord;
      NewTimeRecord = newTimeRecord;
    }
  }
}