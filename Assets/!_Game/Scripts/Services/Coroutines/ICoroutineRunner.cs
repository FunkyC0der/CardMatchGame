using System.Collections;
using UnityEngine;

namespace CardMatchGame.Services.Coroutines
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
    void StopCoroutine(Coroutine coroutine);
  }
}