using System;
using TMPro;
using UnityEngine;

namespace CardMatchGame.Gameplay.UI.Utils
{
  [RequireComponent(typeof(TextMeshProUGUI))]
  public class PrintfText : MonoBehaviour
  {
    public TextMeshProUGUI Text;
    
    [TextArea]
    public string TextFormat;

    public void UpdateView(params object[] args) => 
      Text.text = string.Format(TextFormat, args);

    private void OnValidate()
    {
      if(Text)
        Text.text = TextFormat;
    }
  }
}