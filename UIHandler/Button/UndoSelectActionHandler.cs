using Manager;
using UnityEngine;

namespace UIHandler.Button
{
    public class UndoSelectActionHandler : MonoBehaviour
    {
        private GameObject _eventSystem;

        void Start()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        public void UndoSelectAction()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectAction();
        }
    }
}