using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Util;

namespace DataLoader
{
#if UNITY_EDITOR
    [System.Serializable]
    public class LoadChara
    {
        public string stage;
        public int hp;
        public int maxHp;
        public int mp;
        public int maxMp;
        public int atk;
        public int def;

        public Dictionary<string, int> ItemBag;
        public Dictionary<AttackCode, Attack> AttackList;
        
        public LoadChara()
        {
            this.hp = Constants.DefaultHp;
            this.maxHp = Constants.DefaultHp;
            this.mp = Constants.DefaultMp;
            this.maxMp = Constants.DefaultMp;
            this.atk = Constants.DefaultAtk;
            this.def = Constants.DefaultDef;
            ItemBag = new Dictionary<string, int>();
            AttackList = new Dictionary<AttackCode, Attack>();
        }

        public LoadChara(int hp = Constants.DefaultHp,
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
        
        public Chara ToChara()
        {
            return new Chara(hp, mp, atk, def, ItemBag, AttackList);
        }
    }

    public class LoadCharacterData : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string str in importedAssets)
            {
                //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
                if (str.IndexOf("/CharacterData.csv") != -1)
                {
                    LoadChara[] charaDatas;
                    //　エディタ内で読み込むならResource.Loadではなくこちらを使うこともできる。
                    TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                    //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                    string assetfile = str.Replace(".csv", ".asset");
                    //　※"MonsterDataBase"はScriptableObjectのクラス名に合わせて変更する。
                    CharacterData md = AssetDatabase.LoadAssetAtPath<CharacterData>(assetfile);
                    if (md == null)
                    {
                        md = new CharacterData();
                        AssetDatabase.CreateAsset(md, assetfile);
                    }

                    //　※"MonsterData"はScriptableObjectに入れるデータのクラス名に合わせて変更。
                    //　※"datas"もScriptableObjectが保有する配列名に合わせる。
                    charaDatas = CSVSerializer.Deserialize<LoadChara>(textasset.text);
                    md.characterDict = new Dictionary<string, Chara>();
                    foreach(var data in charaDatas)
                    {
                        md.characterDict.Add(data.stage, data.ToChara());
                    }
                    EditorUtility.SetDirty(md);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
#endif
}