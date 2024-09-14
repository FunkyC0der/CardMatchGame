using System.Collections;
using UnityEngine;

namespace CardMatchGame.Utils
{
  public class CoroutineWait
  {
    private bool m_waitDone;

    public IEnumerator Wait()
    {
      yield return new WaitUntil(() => m_waitDone);
    }

    public void WaitDone() => 
      m_waitDone = true;
  }
}