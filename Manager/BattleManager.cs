using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UIHandler;
using DataLoader;
using Enemy;
using UIHandler.Button;
using UnityEngine;
using Util;

namespace Manager
{
    public class BattleManager : MonoBehaviour
    {
        private GameObject _chara;
        private GameObject _enemySummary;
        private GameObject _eventSystem;
        private GameObject _itemRelation;
        private AttackCode _attackCode;
        private Timeline _timeline;
        private Timeline _objectiveTimeline = Constants.ObjectiveTimeline;
        [SerializeField] private MonsterData monsterData;
        [SerializeField] private ItemData itemData;
        private Dictionary<string, ItemObject> _itemDict;
        private List<Tuple<int, string>> _monsterAtkQueue;
        private string _itemNameToUse;
        private bool _controllable = true;
        private bool _cantAttackOnThisTimeLine = false;
        
        void Awake()
        {
            _attackCode = AttackCode.None;
            _eventSystem = this.gameObject;
            _enemySummary = GameObject.FindWithTag("EnemySummary");
            _itemRelation = GameObject.FindWithTag("ItemRelation");
            _monsterAtkQueue = new List<Tuple<int, string>>();
        }

        private void Start()
        {
            var chara = new Chara(150, 10, 10, 10);
            chara.SetAttack(AttackCode.Attack1, new Attack(30, 2, false));
            chara.SetAttack(AttackCode.Attack2, new Attack(10, 1));
            chara.SetAttack(AttackCode.Attack3, new Attack(0, 2, false, true));
            _chara = GameObject.FindWithTag("Character");
            _chara.GetComponent<CharacterObject>().SetChara(chara);
            _chara.GetComponent<GaugeCharaHandler>().ResetHpGaugeTo((float)chara.hp/ (float)chara.maxHp);
            _chara.GetComponent<GaugeCharaHandler>().ResetMpGaugeTo((float)chara.mp / (float)chara.maxMp);
            foreach(var monster in monsterData.MonsterList)
            {
                _enemySummary.GetComponent<EnemySummaryManager>().SetEnemy(monster);
            }

            _timeline = Timeline.Present;
            _enemySummary.GetComponent<EnemySummaryManager>().PlaceEnemies(_timeline);
            _itemDict = new Dictionary<string, ItemObject>();
            foreach(var item in itemData.ItemList)
            {
                _itemDict.Add(item.name, item);
            }
            _chara.GetComponent<CharacterObject>().AddItem("debuffA");
        }

        public bool CanGetControl()
        {
            return _controllable;
        }
        
        public void SetObjectiveTimeline(Timeline timeline)
        {
            _objectiveTimeline = timeline;
        }
        
        public void SetTimeline(Timeline timeline)
        {
            _timeline = timeline;
        }
        
        public Timeline GetTimeline()
        {
            return _timeline;
        }

        public void SetCharacter(Chara chara)
        {
            _chara.GetComponent<CharacterObject>().SetChara(chara);
        }
        
        public void GetCharacter()
        {
            _chara.GetComponent<CharacterObject>().GetChara();
        }
        
        public void SetItem(string item)
        {
            _chara.GetComponent<CharacterObject>().AddItem(item);
        }
        
        public Dictionary<string, int> GetItemBag()
        {
            return _chara.GetComponent<CharacterObject>().GetItemDict();
        }

        public ItemObject GetItem(string itemName)
        {
            return _itemDict[itemName];
        }

        public void SetEnemy(Timeline timeline, Monster enemy)
        {
            _enemySummary.GetComponent<EnemySummaryManager>().SetEnemy(timeline, enemy);
        }

        public void SetEnemies(Timeline timeline,List<Monster> enemies)
        {   
            _enemySummary.GetComponent<EnemySummaryManager>().SetEnemies(timeline, enemies);
        }
        
        public void SetAttackCode(AttackCode attackCode)
        {
            _attackCode = attackCode;
        }
        
        public AttackCode GetAttackCode()
        {
            return _attackCode;
        }

        public　Attack GetAttack()
        {
            return _chara.GetComponent<CharacterObject>().GetChara().AttackList[_attackCode];
        }

        public void AtMonsterDeBuff(string monster, DeBuffLevel deBuffLevel)
        {
            _enemySummary.GetComponent<EnemySummaryManager>().AtMonsterDeBuff(monster, deBuffLevel);
        }

        public void AtMonsterDeath(string monster, string dropItem)
        {
            _chara.GetComponent<CharacterObject>().AddItem(dropItem);
            _enemySummary.GetComponent<EnemySummaryManager>().AtMonsterDeath(_timeline, monster);
        }

        public void StartEnemyTurn()
        {
            _controllable = false;
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToEnemyTurn();
            _enemySummary.GetComponent<EnemySummaryManager>().StartEnemyTurn(_timeline);
        }

        public IEnumerator EndEnemyTurn()
        {
            foreach (var (enemyAtk,enemyName) in _monsterAtkQueue)
            {
                _chara.GetComponent<CharacterObject>().Damage(enemyAtk);
                var plotSeconds =  _eventSystem.GetComponent<TextSendManager>().PlotTextWithPlotSeconds(enemyName + "'s attack!!");
                yield return new WaitForSeconds(plotSeconds + 0.5f);
            }
            _monsterAtkQueue.Clear();
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToCharacterTurn();
            _controllable = true;
        }

        public void AttackToEnemy(GameObject enemyObj)
        {
            var attack = GetAttack();
            var enemy = enemyObj.GetComponent<EnemyObject>().GetEnemy();
            enemyObj.GetComponent<EnemyObject>().Damage(Function.CalcDamage(attack.Atk, enemy.def));
            _chara.GetComponent<CharacterObject>().ConsMp(attack.Mp);
        }

        public void AttackToOneEnemy(GameObject enemyObj)
        {
            AttackToEnemy(enemyObj);
            StartEnemyTurn();
        }

        public void AttackToAllPred()
        {
            var attack = GetAttack();
            _enemySummary.GetComponent<EnemySummaryManager>().AttackToAllPred(_timeline, attack.Atk);
        }

        public void ResetAllPred()
        {
            _enemySummary.GetComponent<EnemySummaryManager>().ResetAllPred(_timeline);
        }

        public void AttackToAll()
        {
            var attack = GetAttack();
            _enemySummary.GetComponent<EnemySummaryManager>().AttackToAll(_timeline, attack.Atk);
            _chara.GetComponent<CharacterObject>().ConsMp(attack.Mp);
            StartEnemyTurn();
        }
        public void AttackToCharacter(Monster enemy)
        {
            _monsterAtkQueue.Add(new Tuple<int, string>(enemy.atk, enemy.name));
        }

        public void TransitionToNextFromSelectAttack()
        {
            if (_cantAttackOnThisTimeLine)
            {
                var plotSeconds = _eventSystem.GetComponent<TextSendManager>().PlotTextWithPlotAllSeconds("You can,t attack due to enemies are dead.");
                _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectAttackAfterCoolTime(plotSeconds);
            }
            if (_chara.GetComponent<CharacterObject>().IsHealMp(_attackCode))
            {
                _chara.GetComponent<CharacterObject>().HealMpWithAttackCode(_attackCode);
                StartEnemyTurn();
            }
            
            else if (!_chara.GetComponent<CharacterObject>().CanUseAttack(_attackCode))
            {
                var plotSeconds = _eventSystem.GetComponent<TextSendManager>().PlotTextWithPlotAllSeconds("You can,t select this attack due to lack of MP.");
                _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectAttackAfterCoolTime(plotSeconds);
            }

            else if (_chara.GetComponent<CharacterObject>().IsAttackAll(_attackCode))
            {
                _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectConfirmAttackAll();
            }
            else
            {
                _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectEnemy();
            }
        }

        public void UseTimeMachine(Timeline timeline)
        {
            if(_chara.GetComponent<CharacterObject>().GetChara().mp < Constants.TMMp)
            {
                _eventSystem.GetComponent<TextSendManager>().PlotText("You can,t use time machine due to lack of MP.");
                return;
            }
            SetTimeline(timeline);
            _cantAttackOnThisTimeLine = false;
            _eventSystem.GetComponent<UIManager>().ChangeUITo(timeline);
            _enemySummary.GetComponent<EnemySummaryManager>().PlaceEnemies(timeline);
            _chara.GetComponent<CharacterObject>().ConsMp(Constants.TMMp);
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ResetToSelectAction();
        }

        public void GameOver()
        {
            _eventSystem.GetComponent<ExitHandler>().GameOver();
        }
        
        public void GameClear()
        {
            enabled = false;
            _eventSystem.GetComponent<MySelectHandler>().SelectNone();
            _eventSystem.GetComponent<ExitHandler>().GameClear();
        }

        public void NotifyEnemiesDeathNow()
        {
            _cantAttackOnThisTimeLine = true;
            if (_timeline == _objectiveTimeline)
            {
                GameClear();
            }
        }

        public bool IsCharaDeath()
        {
            return _chara.GetComponent<CharacterObject>().IsDeath();
        }

        public void TransitionToConfirmUseItem(string itemName)
        {
            _itemNameToUse = itemName;
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectConfirmUseItem();
        }

        public void UseItem()
        {
            var item = _itemDict[_itemNameToUse];
            _chara.GetComponent<CharacterObject>().UseItem(item.name);
            if (item.type != EffectType.Heal)
            {
                _enemySummary.GetComponent<EnemySummaryManager>().ApplyItem(_timeline, item);
            }
            StartEnemyTurn();
        }

        public void SetItemList()
        {
            _itemRelation.GetComponent<ItemListHandler>().SetItemWithItemBag(GetItemBag());
        }
    }
}