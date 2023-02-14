using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
    public class MedSceneManager : MonoBehaviour
    {
        void Awake()
        {
            Invoke("_callNextScene", 1f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void _callNextScene()
        {
            SceneManager.LoadScene(SceneNameManager.GetSceneName());
        }
    }
}
