using System.Collections.Generic;

namespace Util
{
    public static class Constants
    {
        public const float TextSpeed = 0.05f;
        public const int MaxTextLength = 200;
        public const float ContText = 1f;
        public const int TMMp = 3;
        public const int DefaultHp = 100;
        public const int DefaultMp = 100;
        public const int DefaultAtk = 15;
        public const int DefaultDef = 10;
        public const int DefaultTurn = 10;
        public const int FixedDamage = 10;
        public const float DamageTime = 2f;
        public const float StartHpRatio = 0.91f;
        public const float StartMpRatio = 0.91f;
        public const float LastHpRatio = 0.09f;
        public const float LastMpRatio = 0.09f;
        public const float DeBuffRatio1 = 0.7f;
        public const float DeBuffRatio2 = 0.4f;
        public const float EffectRatio1 = 0.7f;
        public const float EffectRatio2 = 0.4f / 0.7f;
        public const float EffectRatio1And2 = 0.4f;
        public const Timeline ObjectiveTimeline = Timeline.Present;
    }
    
    public static class CharaParams
    {
        public const float Chara1Attack = 10f;
        public const float Chara1Defense = 10f;
        public const int Chara1Health = 10;
        public const int Chara1MagicPoint = 10;
    }

    public static class EnemyRelatedConstants
    {
        public static readonly Dictionary<string, EffectTypeOnDeath> EffectTypeOnDeathDict = new Dictionary<string, EffectTypeOnDeath>() { {"taka", EffectTypeOnDeath.Health}, {"lion", EffectTypeOnDeath.Defense}, {"hebi", EffectTypeOnDeath.Attack} };
    }
    
    public enum SceneNumber
    {
        Title,
        Now,
        Past
    }
    
    public enum EffectType
    {
        None,
        Damage,
        Heal,
        Buff,
        DeBuff
    }

    public enum EffectTypeOnDeath
    {
        None,
        Attack,
        Defense,
        Health,
    }

    public enum HpRatio
    {
        None,
        DeBuff1Area,
        DeBuff2Area
    }
    
    public static class SceneNames
    {
        public const string Title = "StartScene";
        public const string Now = "NowGameScene";
        public const string Past = "PastGameScene";
        public const string Med1 = "MedScene";
        public const string MedTest = "TestMedScene";
    }

    public static class Names
    {
        public const string MonsterName = "Monster";  
        public const string ItemName = "Item";
        
        public const string Action1 = "Action1";
        public const string Action2 = "Action2";
        public const string Action3 = "Action3";
    }

    public enum Timeline
    {
        MorePast = -1,
        Past,
        Present,
        Future
    }

    public enum AttackCode
    {
        None,
        Attack1,
        Attack2,
        Attack3,
        Heal
    }

    public enum AliveType
    {
        Alive,
        Dead,
        Disappear
    }
    
    public enum DeBuffLevel
    {
        None,
        DeBuff1,
        DeBuff2,
        DeBuff1And2
    }

    public static class Texts
    {
        public const string TextAttack1 = "Attack to an enemy with 30 damage";
        public const string TextAttack2 = "Attack to all enemies with 10 damage";
        public const string TextAttack3 = "Charge your MP 2 points";
        public const string TextWrong = "You Make Some Mistakes";

        public static string ReturnTextWithNum(int number)
        {
            switch (number)
            {
                case 0:
                    return TextAttack1;
                case 1:
                    return TextAttack2;
                case 2:
                    return TextAttack3;
                case 3:
                    return "";
                default:
                    return TextWrong;
            }
        }
    }
}