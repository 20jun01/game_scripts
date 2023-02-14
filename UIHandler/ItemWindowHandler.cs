using TMPro;
using UnityEngine;

namespace UIHandler
{
    public class ItemWindowHandler : MonoBehaviour
    {
        private GameObject _messageWindow;
        [SerializeField] private TextMeshProUGUI text;
        private int _textLen = 0;
        private string _text = "";

        void Awake()
        {
            _messageWindow = GameObject.FindGameObjectWithTag("ItemWindow");
            text.text = "";
        }

        private void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void PlotText(string plotText)
        {
            _messageWindow.SetActive(true);
            _text = plotText;
            _textLen = plotText.Length;
            text.text = _text;
            text.maxVisibleCharacters = _textLen;
        }

        public void CloseText()
        {
            text.text = "";
        }
    }
}