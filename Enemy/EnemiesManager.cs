using System.Collections.Generic;
using Manager;
using UIHandler;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Image = UnityEngine.UI.Image;

namespace Enemy
{
    public class EnemiesManager : MonoBehaviour
    {
        private GameObject _enemies;
        private GameObject _eventSystem;
        private GameObject _undoSelectAttackButton;
        private Dictionary<string, DeBuffLevel> _debuffDict = new Dictionary<string, DeBuffLevel>();
        void Awake()
        {
            _enemies = this.gameObject;
            _eventSystem = GameObject.Find("EventSystem");
            _undoSelectAttackButton = GameObject.FindWithTag("UndoSelectAttack");
        }
        
        public void AtMonsterDeBuff(string monster, DeBuffLevel debuffLevel)
        {
            _debuffDict[monster] = debuffLevel;
        }

        private void ApplyMonsterDeBuff(GameObject enemy)
        {
            foreach (var (key, value) in _debuffDict)
            {
                enemy.GetComponent<EnemyObject>().DeBuffWithAncestor(key, value);
            }
        }

        public void AtMonsterDeath(string monster)
        {
            PlaceEnemies();
        }

        public void StartEnemyTurn()
        {
            for (int i = 0; i < _enemies.gameObject.transform.childCount; i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                if (child.GetComponent<EnemyObject>().DecreaseTurn())
                {
                    child.GetComponent<EnemyObject>().MakeEnemyAttack();
                }
            }
            StartCoroutine(_eventSystem.GetComponent<BattleManager>().EndEnemyTurn());
        }

        public void PlaceEnemies()
        {
            List<GameObject> selectable = new List<GameObject>();
            for (int i = 0; i < _enemies.gameObject.transform.childCount; i++)
            {
                var child = _enemies.gameObject.transform.GetChild(i);
                var enemy = child.GetComponent<EnemyObject>().GetEnemy();
                child.gameObject.GetComponent<EnemyObject>().UndoPred();
                
                switch (enemy.alive)
                {
                    case AliveType.Alive:
                        selectable.Add(child.gameObject);
                        ApplyMonsterDeBuff(child.gameObject);
                        break;
                    case AliveType.Dead:
                        // 画像入れ替え処理
                        // not to select
                        child.GetChild(0).GetComponent<Image>().color = new Color32(100, 100, 100, 200);
                        var navi = child.gameObject.GetComponent<Selectable>().navigation;
                        navi.mode = Navigation.Mode.None;
                        child.gameObject.GetComponent<Selectable>().navigation = navi;
                        break;
                    case AliveType.Disappear:
                        child.gameObject.SetActive(false);
                        break;
                }
            }

            if (selectable.Count == 1)
            {
                _eventSystem.GetComponent<MySelectHandler>().SetDefaultEnemyButton(selectable[0]);
                Navigation navi1;
                navi1 = selectable[0].GetComponent<Selectable>().navigation;
                navi1.mode = Navigation.Mode.Explicit;
                navi1.selectOnLeft = null;
                navi1.selectOnRight = _undoSelectAttackButton.GetComponent<Selectable>();
                navi1.selectOnDown = null;
                navi1.selectOnUp = null;
                selectable[0].GetComponent<Selectable>().navigation = navi1;
            }
            else if (selectable.Count > 1)
            {
                
                _eventSystem.GetComponent<MySelectHandler>().SetDefaultEnemyButton(selectable[0]);
                Navigation navi1;
                Navigation navi2;
                navi1 = selectable[0].GetComponent<Selectable>().navigation;
                navi1.mode = Navigation.Mode.Explicit;
                navi1.selectOnLeft = null;
                for (int j = 1; j < selectable.Count; j++)
                {
                    navi2 = selectable[j].GetComponent<Selectable>().navigation;
                    navi2.mode = Navigation.Mode.Explicit;
                    navi1.selectOnRight = selectable[j].GetComponent<Selectable>();
                    navi2.selectOnLeft = selectable[j - 1].GetComponent<Selectable>();
                    selectable[j - 1].GetComponent<Selectable>().navigation = navi1;
                    if (j == selectable.Count - 1)
                    {
                        navi2.selectOnRight = _undoSelectAttackButton.GetComponent<Selectable>();
                        selectable[j].GetComponent<Selectable>().navigation = navi2;
                        var navi = _undoSelectAttackButton.GetComponent<Selectable>().navigation;
                        navi.mode = Navigation.Mode.Explicit;
                        navi.selectOnRight = null;
                        navi.selectOnLeft = selectable[j].GetComponent<Selectable>();
                        _undoSelectAttackButton.GetComponent<Selectable>().navigation = navi;
                    }
                    navi1 = navi2;
                }
            }
            else
            {
                _eventSystem.GetComponent<MySelectHandler>().SetDefaultEnemyButton(_undoSelectAttackButton);
                _eventSystem.GetComponent<BattleManager>().NotifyEnemiesDeathNow();
            }

            _debuffDict.Clear();
        }

        public void HideEnemies()
        {
            for(int i=0;i<_enemies.gameObject.transform.childCount;i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
        }

        public void AttackToAllPred(int atk)
        {
            for (int i = 0; i < _enemies.gameObject.transform.childCount; i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                var enemy = child.GetComponent<EnemyObject>().GetEnemy();
                child.GetComponent<EnemyObject>().DamagePred(Function.CalcDamage(atk, enemy.def));
            }
        }

        public void ResetAllPred()
        {
            for (int i = 0; i < _enemies.gameObject.transform.childCount; i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                child.GetComponent<EnemyObject>().UndoPred();
            }
        }

        public void AttackToAll(int atk)
        {
            for (int i = 0; i < _enemies.gameObject.transform.childCount; i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                var enemy = child.GetComponent<EnemyObject>().GetEnemy();
                child.GetComponent<EnemyObject>().Damage(Function.CalcDamage(atk, enemy.def));
            }
        }

        public void SetEnemy(Monster enemy)
        {
            for(int i=0;i<_enemies.gameObject.transform.childCount;i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                if (child.name == enemy.name)
                {
                    child.GetComponent<EnemyObject>().SetEnemy(enemy);
                    break;
                }
            }
        }

        public void ApplyItem(ItemObject item)
        {
            for(int i=0;i<_enemies.gameObject.transform.childCount;i++)
            {
                GameObject child = _enemies.gameObject.transform.GetChild(i).gameObject;
                child.GetComponent<EnemyObject>().ApplyItem(item);
            }
        }
    }
}