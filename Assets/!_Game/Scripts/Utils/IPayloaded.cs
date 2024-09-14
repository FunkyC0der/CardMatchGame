namespace CardMatchGame.Utils
{
  public interface IPayloaded<in TPayload>
  {
    void Payload(TPayload payload);
  }
}