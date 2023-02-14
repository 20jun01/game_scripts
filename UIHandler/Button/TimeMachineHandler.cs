using Manager;
using UnityEngine;
using Util;

namespace UIHandler.Button
{
    public class TimeMachineHandler : MonoBehaviour
    {
        private GameObject _eventSystem;

        void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
        }
        
        public void TimeMachineToPast()
        {
            _eventSystem.GetComponent<BattleManager>().UseTimeMachine(Timeline.Past);
        }
        
        public void TimeMachineToFuture()
        {
            _eventSystem.GetComponent<BattleManager>().UseTimeMachine(Timeline.Future);
        }
        
        public void TimeMachineToPresent()
        {
            _eventSystem.GetComponent<BattleManager>().UseTimeMachine(Timeline.Present);
        }
        
        public void TimeMachineToMorePast()
        {
            _eventSystem.GetComponent<BattleManager>().UseTimeMachine(Timeline.MorePast);
        }

        public void Undo()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ResetToSelectAction();
        }
    }
}