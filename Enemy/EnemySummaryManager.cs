using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Enemy
{
    public class EnemySummaryManager : MonoBehaviour
    {
        private List<GameObject> _enemiesSummary;

        void Awake()
        {
            _enemiesSummary = new List<GameObject>();
            _enemiesSummary.Add(GameObject.FindWithTag("EnemiesPast"));
            _enemiesSummary.Add(GameObject.FindWithTag("EnemiesPresent"));
        }

        public void AtMonsterDeBuff(string monster, DeBuffLevel deBuffLevel)
        {
            for (int i = 0; i < _enemiesSummary.Count; i++)
            {
                _enemiesSummary[i].GetComponent<EnemiesManager>().AtMonsterDeBuff(monster, deBuffLevel);
            }
        }

        public void AtMonsterDeath(Timeline timeline, string monster)
        {
            _enemiesSummary[TimeFunction.CastTimelineToInt(timeline)].GetComponent<EnemiesManager>()
                .AtMonsterDeath(monster);
        }

        public void StartEnemyTurn(Timeline timeline)
        {
            _enemiesSummary[TimeFunction.CastTimelineToInt(timeline)].GetComponent<EnemiesManager>().StartEnemyTurn();
        }

        public void AttackToAllPred(Timeline timeline, int atk)
        {
            _enemiesSummary[TimeFunction.CastTimelineToInt(timeline)].GetComponent<EnemiesManager>().AttackToAllPred(atk);
        }

        public void ResetAllPred(Timeline timeline)
        {
            _enemiesSummary[TimeFunction.CastTimelineToInt(timeline)].GetComponent<EnemiesManager>().ResetAllPred();
        }
        
        public void AttackToAll(Timeline timeline, int atk)
        {
            _enemiesSummary[TimeFunction.CastTimelineToInt(timeline)].GetComponent<EnemiesManager>().AttackToAll(atk);
        }

        public void PlaceEnemies(Timeline timeline)
        {
            var timeInt = TimeFunction.CastTimelineToInt(timeline);
            for(int i=0; i<_enemiesSummary.Count; i++)
            {
                if (i == timeInt)
                {
                    _enemiesSummary[i].SetActive(true);
                    _enemiesSummary[i].GetComponent<EnemiesManager>().PlaceEnemies();
                }
                else _enemiesSummary[i].SetActive(false);
            }
        }

        public void SetEnemy(Timeline timeline, Monster enemy)
        {
            var timeInt = TimeFunction.CastTimelineToInt(timeline);
            _enemiesSummary[timeInt].GetComponent<EnemiesManager>().SetEnemy(enemy);
        }

        public void SetEnemy(Monster enemy)
        {
            foreach(var timeline in typeof(Timeline).GetEnumValues())
            {
                var timeInt = TimeFunction.CastTimelineToInt((Timeline)timeline);
                if (timeInt >= _enemiesSummary.Count) continue;
                _enemiesSummary[timeInt].GetComponent<EnemiesManager>().SetEnemy(enemy);
            }
        }
        
        public void SetEnemies(Timeline timeline, List<Monster> enemies)
        {
            foreach (var enemy in enemies)
            {
                SetEnemy(timeline, enemy);
            }
        }

        public void ApplyItem(Timeline timeline, ItemObject item)
        {
            _enemiesSummary[TimeFunction.CastTimelineToInt(timeline)].GetComponent<EnemiesManager>().ApplyItem(item);
        }
    }
}