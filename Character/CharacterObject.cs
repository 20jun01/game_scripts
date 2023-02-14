using System;
using System.Collections.Generic;
using DG.Tweening;
using UIHandler;
using Manager;
using UnityEngine;
using Util;

namespace Character
{
    public class CharacterObject : MonoBehaviour
    {
        private GameObject _eventSystem;
        private GameObject _charaObj;
        private Vector3 _initPos;
        private Tweener _shakeTweener;
        private Chara _thisChara;

        void Awake()
        {
            _eventSystem = GameObject.Find("EventSystem");
            _charaObj = this.gameObject;
        }

        public void Death()
        {
            _eventSystem.GetComponent<BattleManager>().GameOver();
        }

        public void SetChara(Chara chara)
        {
            _thisChara = chara;
            _charaObj.GetComponent<GaugeCharaHandler>().ResetBothGauge();
        }

        public Chara GetChara()
        {
            return _thisChara;
        }
        
        public Dictionary<string, int> GetItemDict()
        {
            return _thisChara.ItemBag;
        }

        public void UseItem(string itemName)
        {
            _thisChara.ItemBag[itemName]--;
            if (_thisChara.ItemBag[itemName] <= 0)
            {
                _thisChara.ItemBag.Remove(itemName);
            }
        }
        
        public void AddItem(string itemName)
        {
            if (_thisChara.ItemBag.ContainsKey(itemName))
            {
                _thisChara.ItemBag[itemName]++;
            }
            else
            {
                _thisChara.ItemBag.Add(itemName, 1);
            }
        }

        public bool CanUseAttack(AttackCode attackCode)
        {
            var attack = _thisChara.AttackList[attackCode];
            return attack.Mp <= _thisChara.mp;
        }

        public void Damage(int attack)
        {
            var damage = Function.CalcDamage((float)attack, (float)_thisChara.def);
            _charaObj.GetComponent<GaugeCharaHandler>().MinusHpWithValue(damage, _thisChara.maxHp);
            _thisChara.hp -= damage;
            Vibrate(3f);
            if (_thisChara.hp <= 0)
            {
                Death();
            }
        }
        
        public bool IsDeath()
        {
            return _thisChara.hp <= 0;
        }

        public bool IsHealMp(AttackCode attackCode)
        {
            return _thisChara.AttackList[attackCode].IsMpHeal;
        }

        public void ConsMp(int mp)
        {
            _charaObj.GetComponent<GaugeCharaHandler>().MinusMpWithValue(mp, _thisChara.maxMp);
            _thisChara.mp -= mp;
        }

        public void HealMp(int mp)
        {
            _thisChara.mp = Math.Min(_thisChara.maxMp, _thisChara.mp + mp);
            _charaObj.GetComponent<GaugeCharaHandler>().MinusMpWithValue(0 - mp, _thisChara.maxMp);
        }

        public void HealMpWithAttackCode(AttackCode attackCode)
        {
            var attack = _thisChara.AttackList[attackCode];
            HealMp(attack.Mp);
        }

        public void AddAttackKind(AttackCode attackCode, Attack attack)
        {
            _thisChara.SetAttack(attackCode, attack);
        }

        public bool IsAttackAll(AttackCode attackCode)
        {
            return _thisChara.AttackList[attackCode].IsAll;
        }

        public void Vibrate(float strength = 1f)
        {
            if (_shakeTweener != null)
            {
                _shakeTweener.Kill();
                _charaObj.gameObject.transform.localPosition = _initPos;
            }

            _initPos = _charaObj.transform.localPosition;
            _shakeTweener = _charaObj.transform.DOShakePosition(0.5f, strength, 10, 90, false, true);
        }
    }
}