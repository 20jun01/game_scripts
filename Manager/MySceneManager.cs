using UnityEngine;
using UnityEngine.SceneManagement;
using Util;

namespace Manager
{
    public class MySceneManager : MonoBehaviour
    {
        public void MyLoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        public void MyLoadSceneWithMed1(string sceneName)
        {
            SceneNameManager.SetSceneName(sceneName);
            SceneManager.LoadScene(SceneNames.Med1);
        }

        public void MyLoadSceneWithMedTest(string sceneName)
        {
            SceneNameManager.SetSceneName(sceneName);
            SceneManager.LoadScene(SceneNames.MedTest);
        }
    }
}