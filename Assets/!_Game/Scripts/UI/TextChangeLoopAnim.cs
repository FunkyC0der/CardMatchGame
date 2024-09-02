using System.Collections;
using TMPro;
using UnityEngine;

namespace CardMatchGame.UI
{
  public class TextChangeLoopAnim : MonoBehaviour
  {
    public TextMeshProUGUI Text;
    public string[] TextValues;
    public float FrameDuration = 0.5f;

    private void OnEnable() => 
      StartCoroutine(AnimLoop());

    private IEnumerator AnimLoop()
    {
      int i = 0;
      while (true)
      {
        Text.text = TextValues[i];
        yield return new WaitForSeconds(FrameDuration);

        i = (i + 1) % TextValues.Length;
      }
    }
  }
}