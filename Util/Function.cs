using System;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class Function
    {
        public static int CalcDamage(float attack = (float)Constants.FixedDamage, float defense = 0f)
        {
            return Mathf.Max(0, Mathf.FloorToInt(attack - defense));
        }

        public static float ConvertDeBuffLevelToEffectRatio(DeBuffLevel deBuffLevel)
        {
            switch (deBuffLevel)
            {
                case DeBuffLevel.DeBuff1:
                    return Constants.EffectRatio1;
                case DeBuffLevel.DeBuff2:
                    return Constants.EffectRatio2;
                case DeBuffLevel.DeBuff1And2:
                    return Constants.EffectRatio1And2;
                default:
                    return 0f;
            }
        }

        public static int ConvertDeBuffLevelToPerc(DeBuffLevel deBuffLevel)
        {
            switch (deBuffLevel)
            {
                case DeBuffLevel.DeBuff1:
                    return (int) ((1.0f - Constants.DeBuffRatio1) * 100f);
                case DeBuffLevel.DeBuff2:
                    return (int)((1.0f - Constants.DeBuffRatio2) * 100f);
                case DeBuffLevel.DeBuff1And2:
                    return (int)(((1.0f - Constants.DeBuffRatio2) * 100f));
                default:
                    return -1;
            }
        }

        public static string ConvertEffectTypeOnDeathToString(EffectTypeOnDeath effectType)
        {
            switch (effectType)
            {
                case EffectTypeOnDeath.Attack:
                    return "Power";
                case EffectTypeOnDeath.Health:
                    return "HP";
                case EffectTypeOnDeath.Defense:
                    return "defence";
                default:
                    return "None";
            }
        }
    }

    public class EffectOnDeath
    {
        public EffectTypeOnDeath Type;
        public int Value;
        
        public EffectOnDeath(EffectTypeOnDeath type = EffectTypeOnDeath.None, int value = 0)
        {
            Type = type;
            Value = value;
        }
    }

    [System.Serializable]
    public class ItemObject
    {
        public string name;
        public EffectType type;
        public int value;
        public string description;

        public ItemObject()
        {
            this.name = Names.ItemName;
            type = EffectType.None;
            value = 0;
            description = "";
        }
        
        public ItemObject(string name = Names.ItemName, EffectType type = EffectType.None, int value = 0, string description = "This item has no description.")
        {
            this.name = name;
            this.type = type;
            this.value = value;
            this.description = description;
        }
    }

    [System.Serializable]
    public class Monster
    {
        public string name;
        public int maxHp;
        public int hp;
        public int atk;
        public int def;
        public int turn;
        public int defaultTurn;
        public string dropItem;
        public AliveType alive;
        public EffectTypeOnDeath effectOnDeath;
        public HpRatio hpRatio;
        public string[] ancestors;
        public List<Tuple<EffectTypeOnDeath, DeBuffLevel>> DeBuffList;

        public Monster()
        {
            this.name = Names.MonsterName;
            this.maxHp = Constants.DefaultHp;
            this.hp = Constants.DefaultHp;
            this.atk = Constants.DefaultAtk;
            this.def = Constants.DefaultDef;
            this.turn = Constants.DefaultTurn;
            this.defaultTurn = Constants.DefaultTurn;
            this.dropItem = "";
            this.alive = AliveType.Alive;
            this.effectOnDeath = EffectTypeOnDeath.None;
            this.hpRatio = HpRatio.None;
            this.ancestors = null;
            this.DeBuffList = new List<Tuple<EffectTypeOnDeath, DeBuffLevel>>();
        }

        public Monster(string name = Names.MonsterName, int hp = Constants.DefaultHp,
            int atk = Constants.DefaultAtk, int defs = Constants.DefaultDef,
            int turn = Constants.DefaultTurn, string[] ancestors = null,
            EffectTypeOnDeath effectOnDeath = EffectTypeOnDeath.None, HpRatio hpRatio = HpRatio.None
            , string dropItem = "")
        {
            this.name = name;
            this.hp = hp;
            this.maxHp = hp;
            this.atk = atk;
            this.def = defs;
            this.turn = turn;
            this.defaultTurn = turn;
            this.dropItem = dropItem;
            alive = AliveType.Alive;
            this.effectOnDeath = effectOnDeath;
            this.ancestors = ancestors;
            this.hpRatio = HpRatio.None;
            this.DeBuffList = new List<Tuple<EffectTypeOnDeath, DeBuffLevel>>();
        }

        public Monster(Monster monster)
        {
            this.name = monster.name;
            this.hp = monster.hp;
            this.maxHp = monster.maxHp;
            this.atk = monster.atk;
            this.def = monster.def;
            this.turn = monster.turn;
            this.defaultTurn = monster.defaultTurn;
            this.dropItem = monster.dropItem;
            alive = AliveType.Alive;
            effectOnDeath = monster.effectOnDeath;
            hpRatio = monster.hpRatio;
            ancestors = monster.ancestors;
            DeBuffList = monster.DeBuffList;
        }
    }
    [System.Serializable]
    public class Chara
    { 
        public int hp;
        public int maxHp;
        public int mp;
        public int maxMp;
        public int atk;
        public int def;
        
        public Dictionary<string, int> ItemBag;
        public Dictionary<AttackCode, Attack> AttackList;
        public Chara(int hp = Constants.DefaultHp,
            int mp = Constants.DefaultMp, int atk = Constants.DefaultAtk, int defs = Constants.DefaultDef
            , Dictionary<string, int> itemBag = null, Dictionary<AttackCode, Attack> attackList = null)
        {
            this.hp = hp;
            maxHp = hp;
            this.mp = mp;
            maxMp = mp;
            this.atk = atk;
            def = defs;
            ItemBag = itemBag ?? new Dictionary<string, int>();
            AttackList = attackList ?? new Dictionary<AttackCode, Attack>();
        }

        public void SetAttack(AttackCode code, Attack attack)
        {
            AttackList.Add(code, attack);
        }
        
        public Attack GetAttack(AttackCode code)
        {
            return AttackList[code];
        }
    }

    public class Attack
    {
        public int Atk;
        public int Mp;
        public bool IsAll;
        public bool IsMpHeal;
        public Attack(int atk = 0, int mp= 10, bool isAll = true, bool isMpHeal = false)
        {
            Atk = atk;
            Mp = mp;
            IsAll = isAll;
            IsMpHeal = isMpHeal;
        }
    }

    public class MonsterList
    {
        public Dictionary<string, int> Monsters;
        public int Count;
        public MonsterList()
        {
            Monsters = new Dictionary<string, int>();
            Count = 0;
        }
        public void Add(string name)
        {
            if (Monsters.ContainsKey(name))
            {
                Monsters[name]++;
            }
            else
            {
                Monsters.Add(name, 1);
            }
            Count++;
        }
    }

    public class TimeFunction
    {
        public static Timeline CastSceneNameToTimeline(string sceneName)
        {
            if (sceneName.Contains(Timeline.Future.ToString()))
            {
                return Timeline.Future;
            }
            else if (sceneName.Contains(Timeline.Present.ToString()) || sceneName.Contains("Now"))
            {
                return Timeline.Present;
            }
            else if (sceneName.Contains(Timeline.Past.ToString()))
            {
                return Timeline.Past;
            }
            else
            {
                return Timeline.Present;
            }
        }
        
        public static int CastTimelineToInt(Timeline timeline)
        {
            switch (timeline)
            {
                case Timeline.Past:
                    return 0;
                case Timeline.Present:
                    return 1;
                case Timeline.Future:
                    return 2;
                case Timeline.MorePast:
                    return 2;
                default:
                    return 1;
            }
        }
        
        public static Timeline CastIntToTimeline(int timeline)
        {
            switch (timeline)
            {
                case -1:
                    return Timeline.MorePast;
                case 0:
                    return Timeline.Past;
                case 1:
                    return Timeline.Present;
                case 2:
                    return Timeline.Future;
                default:
                    return Timeline.Present;
            }
        }
    }
    public class PrefabFunction : MonoBehaviour
    {
        /// <summary>
        /// プレハブを作成する(pos, Quaternion有)
        /// </summary>
        /// <returns>GameObject</returns>
        /// <param name="path">プレハブのパス</param>
        /// <param name="pos">Position.</param>
        /// <param name="q">Quaternion.</param>
        public static GameObject CreatePrefab(string path) {
            GameObject prefabObj = (GameObject)Resources.Load (path);
            GameObject prefab = Instantiate (prefabObj) as GameObject;
            return prefab;
        }

        public static GameObject CreatePrefab(string path, Transform parent)
        {
            GameObject prefabObj = (GameObject)Resources.Load(path);
            GameObject prefab = Instantiate(prefabObj, parent) as GameObject;
            return prefab;
        }
    }

    public class RatioFunction
    {
        public static float GetRatio(int num, int max)
        {
            return (float)num / (float)max;
        }
        
        public static float GetRatio(float num, float max)
        {
            return num / max;
        }
        
        public static float GetRatio(int num, float max)
        {
            return (float)num / max;
        }
        
        public static float GetRatio(float num, int max)
        {
            return num / (float)max;
        }
        
        public static DeBuffLevel GetDeBuffLevel(int hp, int maxHp, HpRatio hpRatio)
        {
            float ratio = GetRatio(hp, maxHp);
            switch (hpRatio)
            {
                case HpRatio.None:
                    if (ratio <= Constants.DeBuffRatio1)
                    {
                        if (ratio <= Constants.DeBuffRatio2)
                        {
                            return DeBuffLevel.DeBuff1And2;
                        }
                        else
                        {
                            return DeBuffLevel.DeBuff1;
                        }
                    }
                    else
                    {
                        return DeBuffLevel.None;
                    }
                case HpRatio.DeBuff1Area:
                    if (ratio <= Constants.DeBuffRatio2)
                    {
                        return DeBuffLevel.DeBuff2;
                    }
                    else
                    {
                        return DeBuffLevel.None;
                    }
                default:
                    return DeBuffLevel.None;
            }
        }
    }
}