using CardMatchGame.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CardMatchGame.UI
{
  [RequireComponent(typeof(Button))]
  public class LoadSceneButton : MonoBehaviour
  {
    public EScene Scene;

    [Inject]
    private void Construct(SceneLoader sceneLoader) => 
      GetComponent<Button>().onClick.AddListener(() => sceneLoader.LoadScene(Scene));
  }
}