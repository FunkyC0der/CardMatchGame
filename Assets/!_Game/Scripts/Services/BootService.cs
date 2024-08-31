using CardMatchGame.Services.Progress;
using Zenject;

namespace CardMatchGame.Services
{
  public class BootService : IInitializable
  {
    private readonly IProgressService m_progressService;

    public BootService(IProgressService progressService) => 
      m_progressService = progressService;

    public void Initialize() => 
      m_progressService.Load();
  }
}