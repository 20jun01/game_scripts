using Manager;
using UnityEngine;

namespace UIHandler.Button
{
    public class CheckAttackAllHandler : MonoBehaviour
    {
        private GameObject _eventSystem;
        
        void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        public void OnConfirmed()
        {
            _eventSystem.GetComponent<BattleManager>().AttackToAll();
        }

        public void OnCanceled()
        {
            _eventSystem.GetComponent<BattleManager>().ResetAllPred();
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectAttack();
        }

        public void OnSelected()
        {
            _eventSystem.GetComponent<BattleManager>().AttackToAllPred();
        }
        
        public void UnSelected()
        {
            _eventSystem.GetComponent<BattleManager>().ResetAllPred();
        }
    }
}