using TMPro;
using UnityEngine;

namespace CardMatchGame.Gameplay.UI.Utils
{
  [RequireComponent(typeof(TextMeshProUGUI))]
  public class PrintfText : MonoBehaviour
  {
    private TextMeshProUGUI m_text;
    private string m_originalTextFormat;

    private void Awake()
    {
      m_text = GetComponent<TextMeshProUGUI>();
      m_originalTextFormat = m_text.text;
    }

    public void UpdateView(params object[] args)
    {
      if(m_text)
        m_text.text = string.Format(m_originalTextFormat, args);
    }
  }
}