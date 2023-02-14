using System.Collections;
using UIHandler;
using UIHandler.Button;
using UnityEngine;
using Util;

namespace Manager
{
    public class SelectPhaseChangeManager : MonoBehaviour
    {
        private GameObject _eventSystem;
        private GameObject _itemRelation;

        private void Awake()
        {
            _eventSystem = this.gameObject;
            _itemRelation = GameObject.FindWithTag("ItemRelation");
        }

        public void ChangeToNextFromAction(string buttonName)
        {
            SetActiveWithAction(buttonName);
        }
        
        public void SetActiveWithAction(string action)
        {
            switch (action)
            {
                case Names.Action1:
                    ChangeToSelectAttack();
                    break;
                case Names.Action2:
                    ChangeToSelectTimeMachine();
                    break;
                case Names.Action3:
                    ChangeToSelectItem();
                    break;
                default:
                    break;
            }
        }

        public void ResetToSelectAction()
        {
            _itemRelation.GetComponent<ItemListHandler>().Disable();
            if (_eventSystem.GetComponent<BattleManager>().IsCharaDeath())
            {
                return;
            }
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveWithoutAction();
            _eventSystem.GetComponent<MaskHandler>().DisableMask();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultAction();
        }

        public void ChangeToSelectAction()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveAttackRelation();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultAction();
            _eventSystem.GetComponent<MaskHandler>().DisableMask();
        }

        public void ChangeToSelectAttack()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetActiveAttackRelation();
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveConfirmWindow();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultAttack();
            _eventSystem.GetComponent<MaskHandler>().EnableMask();
        }
        
        public void ChangeToSelectConfirmAttackAll()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveAttackButtonAndWindow();
            _eventSystem.GetComponent<MaskHandler>().DisableMask();
            _eventSystem.GetComponent<ActiveHandler>().SetActiveConfirmWindow();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultConfirm();
        }

        public void ChangeToSelectEnemy()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveAttackRelation();
            _eventSystem.GetComponent<MaskHandler>().DisableMask();
            _eventSystem.GetComponent<ActiveHandler>().SetActiveUndoAttackButton();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultEnemy();
        }
        
        public void ChangeToSelectTimeMachine()
        {
            var timeline = _eventSystem.GetComponent<BattleManager>().GetTimeline();
            _eventSystem.GetComponent<ActiveHandler>().SetActiveTMWindow(timeline);
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultTM(timeline);
            _eventSystem.GetComponent<MaskHandler>().EnableMask();
        }

        public void ChangeToSelectItem()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetActiveItemRelation();
            _eventSystem.GetComponent<BattleManager>().SetItemList();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultItem();
            _eventSystem.GetComponent<MaskHandler>().EnableMask();
        }

        public void ChangeToGenealogyZoom()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetActiveGenealogyZoom();
            _eventSystem.GetComponent<MySelectHandler>().SelectGenealogyZoom();
            _eventSystem.GetComponent<MaskHandler>().EnableMask();
        }
        
        public void ChangeToGenealogy()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetActiveGenealogy();
            _eventSystem.GetComponent<MySelectHandler>().SelectGenealogy();
            _eventSystem.GetComponent<MaskHandler>().DisableMask();
        }

        public void ChangeToEnemyTurn()
        {
            _eventSystem.GetComponent<MaskHandler>().DisableMask();
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveConfirmWindow();
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveAttackRelation();
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveUndoAttackButton();
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveItemConfirmWindow();
            _eventSystem.GetComponent<ActiveHandler>().SetNonActiveItemRelation();
            _eventSystem.GetComponent<MySelectHandler>().SelectNone();
        }

        public void ChangeToCharacterTurn()
        {
            ChangeToSelectAction();
        }

        public void ChangeToSelectAttackAfterCoolTime(float coolTime)
        {
            _eventSystem.GetComponent<MySelectHandler>().SelectNone();
            StartCoroutine(SelectAttackAfterCoolTimeCoroutine(coolTime));
        }

        public void ChangeToReloadWindow()
        {
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultReloadButton();
            _eventSystem.GetComponent<ActiveHandler>().SetActiveReloadWindow();
            _eventSystem.GetComponent<MaskHandler>().EnableMask();
        }
        
        private IEnumerator SelectAttackAfterCoolTimeCoroutine(float coolTime)
        {
            yield return new WaitForSeconds(coolTime);
            ChangeToSelectAttack();
        }

        public void ChangeToSelectConfirmUseItem()
        {
            _eventSystem.GetComponent<ActiveHandler>().SetActiveItemConfirmWindow();
            _eventSystem.GetComponent<MySelectHandler>().SelectDefaultItemConfirmButton();
            _itemRelation.GetComponent<ItemListHandler>().Disable();
        }
    }
}