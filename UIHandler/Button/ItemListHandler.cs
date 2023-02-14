using System.Collections;
using System.Collections.Generic;
using Manager;
using test;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Util;
using Image = UnityEngine.UI.Image;

namespace UIHandler.Button
{
    public class ItemListHandler : MonoBehaviour
    {
        [SerializeField] private GameObject returnButton;
        private GameObject _eventSystem;
        private GameObject _scrollView;
        private GameObject _itemWindow;
        private Selectable _lastsel;
        private Transform _content;
        private Transform _viewPort;
        private KeyInputManager _keyInputManager;
        private Timeline _timeline;
        private int _keyCnt = 0;
        private bool enable = false;

        private void Awake()
        {
            _scrollView = GameObject.FindWithTag("ItemList");
            _itemWindow = GameObject.FindWithTag("ItemWindow");
            _viewPort = _scrollView.transform.GetChild(0);
            _content = _viewPort.transform.GetChild(0);
            _eventSystem = GameObject.Find("EventSystem");
            _lastsel = returnButton.GetComponent<Selectable>();
            _keyInputManager = KeyInputManager.Instance;
        }

        public void AddItemForList(string itemName, int count)
        {
            Selectable sel1;
            Selectable sel2;
            sel1 = _lastsel;
            Navigation navi1;
            Navigation navi2;
            navi1 = _lastsel.navigation;

            var itemPrefab = _eventSystem.GetComponent<ResourceLoadHandler>().GetItemPrefab();

            itemPrefab = Instantiate(itemPrefab, _content) as GameObject;

            itemPrefab.transform.localScale = Vector2.one;
            itemPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
            itemPrefab.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = " x " + count;
            itemPrefab.name = itemName;
            itemPrefab.transform.GetChild(1).GetComponent<Image>().sprite =
                _eventSystem.GetComponent<ResourceLoadHandler>().GetItemImage(itemName);
            
            itemPrefab.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                OnPressedButton();
            });
            
            EventTrigger trigger = itemPrefab.GetComponent<EventTrigger>();
            EventTrigger.Entry selectEntry = new EventTrigger.Entry();
            selectEntry.eventID = EventTriggerType.Select;
            selectEntry.callback.AddListener((eventDate) => { OnSelectButton(); });
            trigger.triggers.Add(selectEntry);
            
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Deselect;
            entry.callback.AddListener((eventDate) => { OnDeSelectButton(); });
            trigger.triggers.Add(entry);

            sel2 = itemPrefab.GetComponent<Selectable>();
            navi2 = sel2.navigation;
            navi2.mode = Navigation.Mode.Explicit;
            navi2.selectOnUp = sel1;
            sel2.navigation = navi2;
            navi1.selectOnDown = sel2;
            sel1.navigation = navi1;
            _lastsel = sel2;
        }

        void Update()
        {
            if (!enable) return;
            if (_content.childCount < 5) return;
            if (_keyInputManager.DownInput)
            {
                _keyCnt -= 1;
            }

            if (_keyInputManager.UpInput)
            {
                _keyCnt += 1;
            }

            if (_keyCnt < 0)
            {
                Vector3 localPos = _content.transform.localPosition;
                localPos.y += returnButton.GetComponent<RectTransform>().sizeDelta.y + 45f;
                _content.transform.localPosition = localPos;

                _keyCnt = 0;
            }

            if (_keyCnt > 2)
            {
                Vector3 localPos = _content.transform.localPosition;
                localPos.y -= returnButton.GetComponent<RectTransform>().sizeDelta.y + 45f;
                _content.transform.localPosition = localPos;

                _keyCnt = 2;
            }
        }

        public void SetItemCount(string itemName, int count)
        {
            foreach (Transform child in _content)
            {
                if (child.name != itemName) continue;
                child.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
                child.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = " x " + count;
                return;
            }

            AddItemForList(itemName, count);
        }

        public void RemoveItem(Transform child)
        {
            var sel1 = child.GetComponent<Selectable>().navigation.selectOnUp;
            var sel2 = child.GetComponent<Selectable>().navigation.selectOnDown;
            if (sel1 != null)
            {
                Navigation navi1 = sel1.navigation;
                navi1.selectOnDown = sel2;
                sel1.navigation = navi1;
            }
            
            if (sel2 != null)
            {
                Navigation navi2 = sel2.navigation;
                navi2.selectOnUp = sel1;
                sel2.navigation = navi2;
            }
            Destroy(child.gameObject);
        }

        public void SetItemWithItemBag(Dictionary<string, int> itemBag)
        {
            Enable();
            foreach (var (itemName, itemCount) in itemBag)
            {
                SetItemCount(itemName, itemCount);
            }

            foreach (Transform child in _content)
            {
                if (itemBag.ContainsKey(child.name) || child.name == "return") continue;
                RemoveItem(child);
            }
        }

        public void OnSelectButton()
        {
            _itemWindow.GetComponent<ItemWindowHandler>()
                .PlotText(_eventSystem.GetComponent<BattleManager>()
                    .GetItem(EventSystem.current.currentSelectedGameObject.name).description);
        }

        public void OnDeSelectButton()
        {
            _itemWindow.GetComponent<ItemWindowHandler>().CloseText();
        }

        public void OnPressedButton()
        {
            var itemObject = EventSystem.current.currentSelectedGameObject;
            var itemName = itemObject.name;
            EventSystem.current.GetComponent<BattleManager>().TransitionToConfirmUseItem(itemName);
        }

        private void OnEnable()
        {
            _keyCnt = 0;
        }

        public void Enable()
        {
            enable = true;
        }

        public void Disable()
        {
            enable = false;
            _keyCnt = 0;
        }

        public void OnConfirmed()
        {
            _eventSystem.GetComponent<BattleManager>().UseItem();
        }

        public void OnConfirmCanceled()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToSelectItem();
        }
    }
}