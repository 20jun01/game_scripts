using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

namespace UIHandler
{
    public class MySelectHandler : MonoBehaviour
    {
        [SerializeField]private GameObject defaultAttackButton;
        [SerializeField]private GameObject defaultActionButton;
        [SerializeField]private GameObject defaultItemButton;
        [SerializeField]private Dictionary<Timeline, GameObject> defaultTMButtons;
        [SerializeField]private GameObject defaultEnemyButton;
        [SerializeField]private GameObject defaultConfirmButton;
        [SerializeField]private GameObject defaultItemConfirmButton;
        private GameObject _genealogy;
        private GameObject _genealogyZoom;
        private GameObject _defaultReloadButton;
        
        private void Awake()
        {
            _genealogy = GameObject.FindWithTag("Genealogy");
            _genealogyZoom = GameObject.FindWithTag("GenealogyZoom");
            defaultTMButtons = new Dictionary<Timeline, GameObject>();
            foreach(var timeline in Enum.GetValues(typeof(Timeline)))
            {
                var buttonObj = GameObject.FindWithTag("DefaultTMButton_" + timeline);
                defaultTMButtons.Add((Timeline)timeline, buttonObj);
            }
            _defaultReloadButton = GameObject.FindWithTag("DefaultReloadButton");
        }

        void Start()
        {
            defaultActionButton.GetComponent<UnityEngine.UI.Button>().Select();
        }

        public void SelectDefaultAttack()
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultAttackButton.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectDefaultAction()
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultActionButton.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectDefaultItem()
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultItemButton.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectDefaultTM(Timeline timeline)
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultTMButtons[timeline].GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectDefaultEnemy()
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultEnemyButton.GetComponent<UnityEngine.UI.Button>().Select();
        }

        public void SelectNone()
        {
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void SelectDefaultConfirm()
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultConfirmButton.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SetDefaultEnemyButton(GameObject enemyButton)
        {
            defaultEnemyButton = enemyButton;
        }

        public void SelectGenealogy()
        {
            EventSystem.current.SetSelectedGameObject(null);
            _genealogy.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectGenealogyZoom()
        {
            EventSystem.current.SetSelectedGameObject(null);
            _genealogyZoom.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectDefaultReloadButton()
        {
            EventSystem.current.SetSelectedGameObject(null);
            _defaultReloadButton.GetComponent<UnityEngine.UI.Button>().Select();
        }
        
        public void SelectDefaultItemConfirmButton()
        {
            EventSystem.current.SetSelectedGameObject(null);
            defaultItemConfirmButton.GetComponent<UnityEngine.UI.Button>().Select();
        }
    }
}