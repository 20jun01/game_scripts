using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIHandler
{
    public class ReloadHandler : MonoBehaviour
    {
        private GameObject _eventSystem;


        private void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        public void OnPressed()
        {
            // 現在のSceneを取得
            Scene loadScene = SceneManager.GetActiveScene();
            // 現在のシーンを再読み込みする
            SceneManager.LoadScene(loadScene.name);
        }

        public void OnCanceled()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ResetToSelectAction();
        }
    }
}