using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Util;

namespace Manager
{
    public class TextSendManager : MonoBehaviour
    {
        private GameObject _messageWindow;
        [SerializeField] private TextMeshProUGUI text;
        private float _t = 0f;
        private int _visibleLen = 0;
        private int _textLen = 0;
        private Queue<string> _textQueue = new Queue<string>();
        private string _text = "";
        private int _textQueueCount = 0;
        private bool _closed = false;

        void Awake()
        {
            _messageWindow = GameObject.FindGameObjectWithTag("MessageWindow");
            text.text = "";
        }

        private void Start()
        {
            _messageWindow.SetActive(false);
            text.maxVisibleCharacters = 0;
            _visibleLen = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (_visibleLen < _textLen)
            {
                _t += Time.deltaTime;
                if (_t >= Constants.TextSpeed)
                {
                    _t -= Constants.TextSpeed;
                    _visibleLen++;
                    _textQueueCount--;
                    text.maxVisibleCharacters = _visibleLen; // 表示を1文字ずつ増やす
                }
            }
            else if(_textQueue.Count > 0)
            {
                _visibleLen = 0;
                text.maxVisibleCharacters = _visibleLen;
                _text = _textQueue.Dequeue();
                text.text = _text;
                _textLen = _text.Length;
                _t = 0f;
                _closed = false;
                _messageWindow.SetActive(true);
            }
            else if (_visibleLen == _textLen)
            {
                _t += Time.deltaTime;
                if (_t >= Constants.ContText)
                {
                    CloseText();
                }
            }
        }

        public void PlotText(string plotText)
        {
            _messageWindow.SetActive(true);
            _textQueue.Enqueue(plotText);
            _textQueueCount += plotText.Length;
        }

        public void CloseText()
        {
            if (_closed) return;
            _visibleLen = 0;
            _textLen = -1;
            _textQueueCount = 0;
            _messageWindow.SetActive(false);
            _closed = true;
        }
        
        public float PlotTextWithPlotSeconds(string plotText)
        {
            _messageWindow.SetActive(true);
            _textQueue.Enqueue(plotText);
            return plotText.Length * Constants.TextSpeed + Constants.ContText;
        }
        
        public float PlotTextWithPlotAllSeconds(string plotText)
        {
            _messageWindow.SetActive(true);
            _textQueue.Enqueue(plotText);
            _textQueueCount += plotText.Length;
            return _textQueueCount * Constants.TextSpeed + Constants.ContText;
        }
    }
}