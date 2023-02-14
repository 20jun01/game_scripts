using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UIHandler
{
    public class ResourceLoadHandler : MonoBehaviour
    {
        private Dictionary<string, Sprite> _imageDict;
        
        private const string ItemPrefabPath = "Item/ItemPrefab";
        
        private const string ItemIconPath = "Item/ItemIcon";
        
        private GameObject _itemPrefab;

        // Start is called before the first frame update
        void Awake()
        {
            _imageDict = new Dictionary<string, Sprite>();
            LoadItemImage();
            LoadItemPrefab();
        }

        // Update is called once per frame
        private void LoadItemImage()
        {
            var itemIcons = Resources.LoadAll(ItemIconPath, typeof(Sprite));
            foreach (var itemIcon in itemIcons)
            {
                _imageDict.Add(itemIcon.name, (Sprite) itemIcon);
            }
        }
        
        private void LoadItemPrefab()
        {
            _itemPrefab = Resources.Load<GameObject>(ItemPrefabPath);
        }

        public Sprite GetItemImage(string name)
        {
            return _imageDict[name];
        }

        public GameObject GetItemPrefab()
        {
            return _itemPrefab;
        }
    }
}