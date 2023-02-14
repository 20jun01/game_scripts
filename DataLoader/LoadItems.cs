using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Util;

namespace DataLoader
{
#if UNITY_EDITOR

    public class LoadItems: AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string str in importedAssets)
            {
                //　IndexOfの引数は"/(読み込ませたいファイル名)"とする。
                if (str.IndexOf("/ItemData.csv") != -1)
                {
                    //　エディタ内で読み込むならResource.Loadではなくこちらを使うこともできる。
                    TextAsset textasset = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
                    //　同名のScriptableObjectファイルを読み込む。ない場合は新たに作る。
                    string assetfile = str.Replace(".csv", ".asset");
                    //　※"MonsterDataBase"はScriptableObjectのクラス名に合わせて変更する。
                    ItemData md = AssetDatabase.LoadAssetAtPath<ItemData>(assetfile);
                    if (md == null)
                    {
                        md = new ItemData();
                        AssetDatabase.CreateAsset(md, assetfile);
                    }
                    Debug.Log("Loading ItemData");

                    //　※"MonsterData"はScriptableObjectに入れるデータのクラス名に合わせて変更。
                    //　※"datas"もScriptableObjectが保有する配列名に合わせる。
                    md.ItemList = CSVSerializer.Deserialize<ItemObject>(textasset.text);
                    Debug.Log(md.ItemList);
                    EditorUtility.SetDirty(md);
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
#endif
}