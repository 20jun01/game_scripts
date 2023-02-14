using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
namespace test
{
    public class ScrollViewTest : MonoBehaviour
    {
        [SerializeField] private GameObject scrollView;
        [SerializeField] private GameObject returnButton;
        private Transform _content;
        private Transform _viewPort;

        private KeyInputManager _keyInputManager;

        private Dictionary<string, Sprite> _imageDict;
        private Selectable _lastsel;
        private int _cnt = 0;
        private int _keyCnt = 0;

        // Start is called before the first frame update
        void Start()
        {
            _keyInputManager = KeyInputManager.Instance;
            _viewPort = scrollView.transform.GetChild(0);
            _content = _viewPort.transform.GetChild(0);
            _imageDict = new Dictionary<string, Sprite>();
            _lastsel = returnButton.GetComponent<Selectable>();
            AsyncOperationHandle<IList<Sprite>> loadWithSingleKeyHandle = Addressables.LoadAssetsAsync<Sprite>(
                "ItemIcon", sprite =>
                {
                    //Gets called for every loaded asset
                    _imageDict.Add(sprite.name, sprite);
                    Debug.Log(sprite.name);
                });

            loadWithSingleKeyHandle.WaitForCompletion();
            
            Addressables.Release(loadWithSingleKeyHandle);
        }

        // Update is called once per frame
        void Update()
        {
            if (_keyInputManager.DownInput)
            {
                _keyCnt -= Math.Max(0, 1);
            }

            if (_keyInputManager.UpInput)
            {
                _keyCnt += Math.Min(20, 1);
            }

            if (_keyCnt < 0)
            {
                Vector3 localPos = _content.transform.localPosition;
                localPos.y += returnButton.GetComponent<RectTransform>().sizeDelta.y + 45f;
                _content.transform.localPosition = localPos;
                
                Debug.Log(returnButton.GetComponent<RectTransform>().sizeDelta.y);
                _keyCnt = 0;
            }

            if (_keyCnt > 2)
            {
                Vector3 localPos = _content.transform.localPosition;
                localPos.y -= returnButton.GetComponent<RectTransform>().sizeDelta.y + 45f;
                _content.transform.localPosition = localPos;
                
                Debug.Log(returnButton.GetComponent<RectTransform>().sizeDelta.y + 45f);
                _keyCnt = 2;
            }
        
            if (_cnt >= 20) return;
            AddItemForList("debuffA", 1);
            _cnt += 1;
        }

        public void AddItemForList(string itemName, int count)
        {

            Selectable sel1;
            Selectable sel2;
            sel1 = _lastsel;
            Navigation navi1;
            Navigation navi2;
            navi1 = _lastsel.navigation;
            
            // 今まで通りロードメソッドを呼ぶ
            var op = Addressables.LoadAssetAsync<GameObject>("Item");

            // WaitForCompletionで同期的にロード完了を待機
            var itemPrefab = op.WaitForCompletion();
            itemPrefab = Instantiate(itemPrefab, _content) as GameObject;

            itemPrefab.transform.localScale = Vector2.one;
            itemPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
            itemPrefab.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = " x " + count;
            itemPrefab.name = itemName;
            itemPrefab.transform.GetChild(1).GetComponent<Image>().sprite = _imageDict[itemName];

            // 使い終わったらリリース（今まで通り）
            Addressables.Release(op);
            
            sel2 = itemPrefab.GetComponent<Selectable>();
            navi2 = sel2.navigation;
            navi2.mode = Navigation.Mode.Explicit;
            navi2.selectOnUp = sel1;
            sel2.navigation = navi2;
            navi1.selectOnDown = sel2;
            sel1.navigation = navi1;
            _lastsel = sel2;
        }
    }
}