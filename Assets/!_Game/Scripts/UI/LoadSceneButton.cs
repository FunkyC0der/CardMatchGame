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
    private void Construct(SceneLoader sceneLoader, LoadingCurtain loadingCurtain) => 
      GetComponent<Button>().onClick.AddListener(() =>
      {
        loadingCurtain.Show();
        sceneLoader.LoadScene(Scene);
      });
  }
}