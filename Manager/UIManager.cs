using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private GameObject _backGround;
        private GameObject _gameOver;
        private GameObject _gameClear;
        private Dictionary<Timeline, Sprite> _timelineSprites;
        void Awake()
        {
            _gameOver = GameObject.FindWithTag("GameOver");
            _gameClear = GameObject.FindWithTag("GameClear");
            _backGround = GameObject.FindWithTag("BackGround");
            _timelineSprites = new Dictionary<Timeline, Sprite>();
            foreach (Timeline timeline in Enum.GetValues(typeof(Timeline)))
            {
                _timelineSprites.Add(timeline, Resources.Load<Sprite>("Sprites/Prod/back/back_" + timeline.ToString()));
            }
        }

        private void Start()
        {
            _gameOver.SetActive(false);
            _gameClear.SetActive(false);
            ChangeUITo(Timeline.Present);
        }

        public void ChangeUITo(Timeline timeline)
        {
            _backGround.GetComponent<Image>().sprite = _timelineSprites[timeline];
        }
        
        public void SetActiveGameOver()
        {
            _gameOver.SetActive(true);
        }
        
        public void SetActiveGameClear()
        {
            _gameClear.SetActive(true);
        }
    }
}