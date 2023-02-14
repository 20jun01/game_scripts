using UnityEngine;

namespace UIHandler
{
    public class StartUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject defaultButton;

        private void Start()
        {
            defaultButton.GetComponent<UnityEngine.UI.Button>().Select();
        }

        void Update()
        {
            // クリックされたら初期状態(SelectAction)に戻る
            if(Input.GetMouseButton(0))
            {
                defaultButton.GetComponent<UnityEngine.UI.Button>().Select();
            }
        }
    }
}

