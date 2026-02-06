using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Utilities.SceneManagement
{
    public class SceneLoaderService
    {
        public IEnumerator LoadAsync(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single)
        {
            AsyncOperation sceneLoaded = SceneManager.LoadSceneAsync(sceneName, sceneMode);

            yield return new WaitWhile(() => sceneLoaded.isDone == false);
        }

        public IEnumerator UnloadAsync(string sceneName)
        {
            AsyncOperation sceneUnloaded = SceneManager.UnloadSceneAsync(sceneName);

            yield return new WaitWhile(() => sceneUnloaded.isDone == false);
        }
    }
}