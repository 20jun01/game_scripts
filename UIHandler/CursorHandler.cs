using Manager;
using UnityEngine;

namespace UIHandler
{
    public class CursorHandler : MonoBehaviour
    {
        private GameObject _eventSystem;

        private GameObject _enemySummary;
        // Start is called before the first frame update
        void Awake()
        {
            _eventSystem = this.gameObject;
            _enemySummary = GameObject.FindWithTag("EnemySummary");
        }

        private void Start()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectAction();
        }
        
        void Update()
        {
            // クリックされたら初期状態(SelectAction)に戻る
            // GetControlできない(敵ターンとか)ならしない
            if(Input.GetMouseButton(0) && _eventSystem.GetComponent<BattleManager>().CanGetControl()){
                _eventSystem.GetComponent<SelectPhaseChangeManager>().ResetToSelectAction();
            }
        }
    }
}