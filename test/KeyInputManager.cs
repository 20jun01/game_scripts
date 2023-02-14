using UnityEngine;

namespace test
{
    public class KeyInputManager : MonoBehaviour
    {
        private static KeyInputManager _instance;

        public static KeyInputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<KeyInputManager>();
                }

                return _instance;
            }

        }

        private const KeyCode UpKeyCodeWASD = KeyCode.W;
        private const KeyCode UpKeyCode = KeyCode.UpArrow;

        private const KeyCode DownCodeWASD = KeyCode.S;
        private const KeyCode DownKeyCode = KeyCode.DownArrow;

        private bool _upInput;
        private bool _downInput;

        public bool UpInput => _upInput;
        public bool DownInput => _downInput;
        
        private void Update()
        {
            _upInput = Input.GetKeyDown(UpKeyCode) || Input.GetKeyDown(UpKeyCodeWASD);
            _downInput = Input.GetKeyDown(DownCodeWASD) || Input.GetKeyDown(DownKeyCode);
        }
    }
    
}