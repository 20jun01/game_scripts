using Manager;
using UnityEngine;

namespace UIHandler
{
    public class ActionHandler : MonoBehaviour
    {
        private GameObject _eventSystem;
        // Start is called before the first frame update
        void Start()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }

        public void OnClickActionButton(string buttonName)
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToNextFromAction(buttonName);
        }
    }
}