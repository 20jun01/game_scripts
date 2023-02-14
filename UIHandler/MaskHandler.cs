using UnityEngine;

namespace UIHandler
{
    public class MaskHandler : MonoBehaviour
    {
        private GameObject _mask;
        // Start is called before the first frame update
        private void Awake()
        {
            _mask = GameObject.FindWithTag("Mask");
        }

        void Start()
        {
            _mask.SetActive(false);
        }

        public void EnableMask()
        {
            _mask.SetActive(true);
        }
        
        public void DisableMask()
        {
            _mask.SetActive(false);
        }
    }
}
