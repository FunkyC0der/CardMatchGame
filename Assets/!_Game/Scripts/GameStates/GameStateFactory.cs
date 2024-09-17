using Zenject;

namespace CardMatchGame.GameStates
{
  public class GameStateFactory
  {
    private readonly DiContainer m_diContainer;

    public GameStateFactory(DiContainer diContainer) => 
      m_diContainer = diContainer;

    public T Create<T>() where T : GameState => 
      m_diContainer.Resolve<T>();
  }
}