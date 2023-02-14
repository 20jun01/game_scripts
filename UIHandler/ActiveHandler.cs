using System;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace UIHandler
{
    public class ActiveHandler : MonoBehaviour
    {
        private Dictionary<Timeline, GameObject> _tMWindows;

        private GameObject _attackRelation;
        
        private GameObject _undoAttackButton;

        private GameObject _confirmWindow;

        private GameObject _genealogyZoom;

        private GameObject _genealogy;

        private GameObject _attackButtons;

        private GameObject _attackWindow;

        private GameObject _reloadWindow;

        private GameObject _itemRelation;
        
        private GameObject _itemConfirmWindow;
        
        private void Awake()
        {
            _undoAttackButton = GameObject.FindWithTag("UndoSelectAttack");
            _tMWindows = new Dictionary<Timeline, GameObject>();
            foreach (var timeline in Enum.GetValues(typeof(Timeline)))
            {
                var tMWindowObj =  GameObject.FindWithTag("TMWindow_" + timeline);
                _tMWindows.Add((Timeline)timeline, tMWindowObj);
            }
            _attackRelation = GameObject.FindWithTag("AttackRelation");
            _confirmWindow = GameObject.FindWithTag("ConfirmWindow");
            _genealogy = GameObject.FindWithTag("Genealogy");
            _genealogyZoom = GameObject.FindWithTag("GenealogyZoom");
            _attackButtons = GameObject.FindWithTag("AttackButtons");
            _attackWindow = GameObject.FindWithTag("AttackWindow");
            _reloadWindow = GameObject.FindWithTag("ReloadWindow");
            _itemConfirmWindow = GameObject.FindWithTag("ItemConfirmWindow");
            _itemRelation = GameObject.FindWithTag("ItemRelation");
        }

        private void Start()
        {
            foreach(var tMWindow in _tMWindows)
            {
                if (tMWindow.Value == null)
                    continue;
                tMWindow.Value.SetActive(false);
            }
            _attackRelation.SetActive(false);
            _undoAttackButton.SetActive(false);
            _confirmWindow.SetActive(false);
            _genealogyZoom.SetActive(false);
            _reloadWindow.SetActive(false);
            _itemConfirmWindow.SetActive(false);
            _itemRelation.SetActive(false);
        }

        public void SetNonActive()
        {
            foreach(var tMWindow in _tMWindows)
            {
                if (tMWindow.Value == null)
                    continue;
                tMWindow.Value.SetActive(false);
            }
            _attackButtons.SetActive(true);
            _attackWindow.SetActive(true);
            _attackRelation.SetActive(false);
            _undoAttackButton.SetActive(false);
            _confirmWindow.SetActive(false);
            _genealogyZoom.SetActive(false);
            _genealogy.SetActive(false);
            _reloadWindow.SetActive(false);
            _itemConfirmWindow.SetActive(false);
            _itemRelation.SetActive(false);
        }

        public void SetNonActiveWithoutAction()
        {
            SetNonActive();
            _genealogy.SetActive(true);
        }

        public void SetActiveItemRelation()
        {
            _itemRelation.SetActive(true);
            return;
        }
        
        public void SetNonActiveItemRelation()
        {
            _itemRelation.SetActive(false);
            return;
        }
        
        public void SetActiveItemConfirmWindow()
        {
            _itemConfirmWindow.SetActive(true);
            return;
        }
        
        public void SetNonActiveItemConfirmWindow()
        {
            _itemConfirmWindow.SetActive(false);
            return;
        }
        
        public void SetActiveAttackRelation()
        {
            _attackRelation.SetActive(true);
            _attackButtons.SetActive(true);
            _attackWindow.SetActive(true);
        }

        public void SetNonActiveAttackButtonAndWindow()
        {
            _attackButtons.SetActive(false);
            _attackWindow.SetActive(false);
        }
        
        public void SetActiveAttackButtonAndWindow()
        {
            _attackButtons.SetActive(true);
            _attackWindow.SetActive(true);
        }
        
        public void SetActiveUndoAttackButton()
        {
            _undoAttackButton.SetActive(true);
        }

        public void SetNonActiveAttackRelation()
        {
            _attackRelation.SetActive(false);
        }
        
        public void SetNonActiveUndoAttackButton()
        {
            _undoAttackButton.SetActive(false);
        }
        
        public void SetActiveTMWindow(Timeline timeline)
        {
           _tMWindows[timeline].SetActive(true);
        }
        
        public void SetNonActiveTMWindow(Timeline timeline)
        {
            _tMWindows[timeline].SetActive(false);
        }

        public void SetActiveConfirmWindow()
        {
            _confirmWindow.SetActive(true);
        }
        
        public void SetNonActiveConfirmWindow()
        {
            _confirmWindow.SetActive(false);
        }
        
        public void SetActiveGenealogyZoom()
        {
            _genealogyZoom.SetActive(true);
            _genealogy.SetActive(false);
        }

        public void SetActiveGenealogy()
        {
            _genealogy.SetActive(true);
            _genealogyZoom.SetActive(false);
        }
        
        public void SetActiveReloadWindow()
        {
            _reloadWindow.SetActive(true);
        }
        
        public void SetNonActiveReloadWindow()
        {
            _reloadWindow.SetActive(false);
        }
    }
}