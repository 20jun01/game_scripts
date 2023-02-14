using System;
using System.Linq;
using DG.Tweening;
using Manager;
using UIHandler;
using UnityEngine;
using Util;

namespace Enemy
{
    public class EnemyObject : MonoBehaviour
    {
        private Monster _thisEnemy;
        private Vector3 _initPos;
        private Tweener _shakeTweener;
        private GameObject _eventSystem;

        private GameObject _enemy;

        private GameObject _deBuffSummary;

        void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
            _enemy = this.gameObject;
            _deBuffSummary = _enemy.transform.GetChild(1).Find("debufficon").gameObject;
        }

        public void OnEnemyPressed()
        {
            _eventSystem.GetComponent<BattleManager>().AttackToOneEnemy(_enemy);
        }

        public void OnEnemySelected()
        {
            DamagePred(Function.CalcDamage((float)_eventSystem.GetComponent<BattleManager>().GetAttack().Atk, (float)_thisEnemy.def));
        }
        
        public void OnEnemyUnselected()
        {
            UndoPred();
        }
        
        public void SetEnemy(Monster monster)
        {
            _thisEnemy = new Monster(monster);
            _enemy.GetComponent<GaugeHandler>().ChangeToBothGauge(_thisEnemy.maxHp, _thisEnemy.maxHp);
        }

        public Monster GetEnemy()
        {
            return _thisEnemy;
        }

        public void DamagePred(int damage)
        {
            _enemy.GetComponent<GaugeHandler>().MinusPredictGauge(damage, _thisEnemy.maxHp);
        }

        public void UndoPred()
        {
            _enemy.gameObject.GetComponent<GaugeHandler>().ChangeToPredictGauge(_thisEnemy.hp , _thisEnemy.maxHp);
        }
        
        public void Damage(int damage)
        {
            if (_thisEnemy.alive != AliveType.Alive) return;
            _thisEnemy.hp = Math.Max(_thisEnemy.hp - damage, 0);
            var deBufflevel = RatioFunction.GetDeBuffLevel(_thisEnemy.hp, _thisEnemy.maxHp, _thisEnemy.hpRatio);
            if (_thisEnemy.hp == 0)
            {
                _thisEnemy.alive = AliveType.Dead;
                _eventSystem.GetComponent<BattleManager>().AtMonsterDeath(_thisEnemy.name, _thisEnemy.dropItem);
            }
            _enemy.GetComponent<GaugeHandler>().ChangeToPredictGauge(_thisEnemy.hp, _thisEnemy.maxHp);
            _enemy.GetComponent<GaugeHandler>().MinusGauge(_thisEnemy.hp, _thisEnemy.maxHp);
            Vibrate();
            if (deBufflevel != DeBuffLevel.None)
            {
                _eventSystem.GetComponent<BattleManager>().AtMonsterDeBuff(_thisEnemy.name, deBufflevel);
            }
        }

        public void DeBuffWithAncestor(string monster, DeBuffLevel deBuffLevel)
        {
            if (_thisEnemy.ancestors == null) return;
            if (!_thisEnemy.ancestors.ToList().Contains(monster)) return;
            var effectTypeOnDeath = EnemyRelatedConstants.EffectTypeOnDeathDict[monster];
            var ratio = Function.ConvertDeBuffLevelToEffectRatio(deBuffLevel);
            ApplyEffect(effectTypeOnDeath, ratio);
        }
        
        private void ApplyEffect(EffectTypeOnDeath effectType, float ratio)
        {
            switch (effectType)
            {
                case EffectTypeOnDeath.Attack:
                    _thisEnemy.atk = (int)(_thisEnemy.atk * ratio);
                    break;
                case EffectTypeOnDeath.Defense:
                    _thisEnemy.def = (int)((float)_thisEnemy.def * ratio);
                    break;
                case EffectTypeOnDeath.Health:
                    _thisEnemy.hp = (int)((float)_thisEnemy.hp * ratio);
                    _thisEnemy.maxHp = (int)((float)_thisEnemy.maxHp * ratio);
                    _enemy.GetComponent<GaugeHandler>().ResetText(_thisEnemy.hp, _thisEnemy.maxHp);
                    break;
            }
            ChangeDeBuffIcon(effectType, ratio);
        }

        private void ChangeDeBuffIcon(EffectTypeOnDeath effectType, float ratio)
        {
            _deBuffSummary.GetComponent<DeBuffSummaryHandler>().ChangeDeBuffIconWithRatio(effectType, ratio);
        }

        public void MakeEnemyAttack()
        {
            _eventSystem.GetComponent<BattleManager>().AttackToCharacter(_thisEnemy);
        }

        public bool DecreaseTurn()
        {
            if (_thisEnemy.alive != AliveType.Alive) return false;
            if (_thisEnemy.turn > 1)
            {
                _thisEnemy.turn -= 1;
                return false;
            }
            else
            {
                _thisEnemy.turn = _thisEnemy.defaultTurn;
                return true;
            }
        }

        public void Vibrate()
        {
            if (_shakeTweener != null)
            {
                _shakeTweener.Kill();
                this.gameObject.transform.localPosition = _initPos;
            }
            _initPos = this.gameObject.transform.localPosition;
            _shakeTweener = this.gameObject.transform.DOShakePosition(0.5f, 5f, 10, 90, false, true);
        }

        public void ApplyItem(ItemObject item)
        {
            switch (item.type)
            {
                case EffectType.Buff:
                    ApplyEffect(EffectTypeOnDeath.Defense, (100 - item.value) / 100f);
                    break;
                case EffectType.DeBuff:
                    ApplyEffect(EffectTypeOnDeath.Attack, (100 - item.value) / 100f);
                    break;
                case EffectType.Damage:
                    Damage(item.value);
                    break;
                default:
                    break;
            }
        }
    }
}