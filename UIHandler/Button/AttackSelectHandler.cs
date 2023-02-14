using Manager;
using UnityEngine;
using Util;

namespace UIHandler.Button
{
    public class AttackSelectHandler : MonoBehaviour
    {
        private GameObject _eventSystem;

        void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        public void OnPressed(int attackIndex)
        {
            _eventSystem.GetComponent<BattleManager>().SetAttackCode((AttackCode)(attackIndex + 1));
            _eventSystem.GetComponent<BattleManager>().TransitionToNextFromSelectAttack();
        }
    }
}