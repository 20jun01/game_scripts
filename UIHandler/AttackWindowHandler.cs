using TMPro;
using UnityEngine;
using Util;

namespace UIHandler
{
    public class AttackWindowHandler : MonoBehaviour
    {
        private GameObject _attackWindow;
        [SerializeField] private TextMeshProUGUI text;
        private int _visibleLen = 0;

        void Awake()
        {
            _attackWindow = GameObject.FindGameObjectWithTag("AttackWindow");
        }

        public void PlotText(string plotText)
        {
            _attackWindow.SetActive(true);
            text.text = plotText;
            _visibleLen = plotText.Length;
            text.maxVisibleCharacters = _visibleLen;
        }
        
        public void PlotTextWithButtonNumber(int buttonNumber)
        {
            _attackWindow.SetActive(true);
            text.text = Texts.ReturnTextWithNum(buttonNumber);
            _visibleLen = text.text.Length;
            text.maxVisibleCharacters = _visibleLen;
        }

        public void CloseText()
        {
            _visibleLen = 0;
            _attackWindow.SetActive(false);
        }
    }
}