using Manager;
using UnityEngine;

namespace UIHandler.Button
{
    public class UndoSelectAttackHandler : MonoBehaviour
    {
        private GameObject _eventSystem;
        // Start is called before the first frame update
        void Start()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        public void UndoSelectAttack()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectAttack();
        }
    }
}