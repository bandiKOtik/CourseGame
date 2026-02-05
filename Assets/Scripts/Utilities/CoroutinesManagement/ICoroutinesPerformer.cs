using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utilities.CoroutinesManagement
{
    public interface ICoroutinesPerformer
    {
        Coroutine StartPerform(IEnumerator coroutineFunction);

        void StopPerform(Coroutine coroutine);
    }
}