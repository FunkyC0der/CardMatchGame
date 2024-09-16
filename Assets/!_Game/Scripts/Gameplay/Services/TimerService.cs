using CardMatchGame.Gameplay.Utils;
using CardMatchGame.Services.Levels;
using Zenject;

namespace CardMatchGame.Gameplay.Services
{
  public class TimerService : ITickable
  {
    public float TimeLeft => m_timer.TimeLeft;
    public float TimeElapsed => m_timer.TimeElapsed;
    public bool IsFinished => m_timer.IsReady;
    
    public bool IsActive { get; private set; }
    
    private readonly Cooldown m_timer = new();
    
    private TimerService(ICurrentLevelDataProvider currentLevelData) =>
      m_timer.Activate(currentLevelData.Data.TimerDuration);

    public void Start() => 
      IsActive = true;

    public void Stop() =>
      IsActive = false;

    public void Tick()
    {
      if (IsActive)
        m_timer.Update();
    }
  }
}