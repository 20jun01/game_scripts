using Manager;
using UnityEngine;
using Util;

namespace UIHandler
{
    public class ExitHandler : MonoBehaviour
    {
        private bool _isQuit = false;

        private bool _isReload = false;

        private GameObject _eventSystem;
        // Start is called before the first frame update
        void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        // Update is called once per frame
        void Update()
        {
            _isQuit = Input.GetKeyDown(KeyCode.Q);
            _isReload = Input.GetKeyDown(KeyCode.R);
            if (_isQuit)
            {
                // ここでゲーム終了かをきくwindowを表示する
                
                // isFirstを全てtrueにし、全モンスターを消去する
                _eventSystem.GetComponent<MySceneManager>().MyLoadScene(SceneNames.Title);
            }
            else if (_isReload)
            {
                DoReload();
            }
                
        }

        public void DoReload()
        {
            // ここでリロードかをきくwindowを表示する
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToReloadWindow();
        }

        public void GameOver()
        {
            // ゲームオーバー表示をする
            _eventSystem.GetComponent<UIManager>().SetActiveGameOver();
            DoReload();
        }

        public void GameClear()
        {
            _eventSystem.GetComponent<UIManager>().SetActiveGameClear();
        }
    }
}