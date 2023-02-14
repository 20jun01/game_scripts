using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UIHandler
{
    public class GaugeHandler : MonoBehaviour
    {
        private GameObject _gauge;
        private GameObject _gaugePred;
        private TextMeshProUGUI _hpText;
        private TextMeshProUGUI _maxHpText;

        private int _countRes = 0;

        private float _damageRes = 0f;

        private int _maxHp;
        
        private int _hp;
        
        private float _updateValue;
        
        void Awake()
        {
            var enemy = this.gameObject;
            _gauge = enemy.transform.GetChild(1).GetChild(3).GetChild(0).GetChild(0).gameObject;
            _gaugePred = enemy.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(0).gameObject;
            _maxHpText = enemy.transform.GetChild(1).GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            _hpText = enemy.transform.GetChild(1).GetChild(1).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        }

        // 50回で1秒
        void FixedUpdate()
        {
            if (_countRes > 0)
            {
                _gaugePred.GetComponent<Image>().fillAmount -= _updateValue;
                _damageRes -= _updateValue;
                _countRes--;
            }
        }
        
        public void MinusPredictGauge(int damage, int maxHp)
        {
            _gauge.GetComponent<Image>().fillAmount -= (float)damage / (float)maxHp;
            _hp -= damage;
        }

        public void ChangeToPredictGauge(int hp, int maxHp)
        {
            _hp = hp;
            _maxHp = maxHp;
            _gauge.GetComponent<Image>().fillAmount = (float)hp / (float)maxHp;
        }

        public void ChangeToBothGauge(int hp, int maxHp)
        {
            _gauge.GetComponent<Image>().fillAmount = hp / (float)maxHp;
            _gaugePred.GetComponent<Image>().fillAmount = hp / (float)maxHp;
            _hp = hp;
            _maxHp = maxHp;
            _maxHpText.text = "/" + _maxHp.ToString();
            _hpText.text = _hp.ToString();
        }

        public void MinusGauge(int hp, int maxHp)
        {
            _damageRes = ((_gaugePred.GetComponent<Image>().fillAmount * maxHp) - hp)/ (float)maxHp;
            _countRes = (int)(Constants.DamageTime * 50f);
            _updateValue = _damageRes / (float)_countRes;
            _hp = hp;
            _maxHp = maxHp;
            _hpText.text = _hp.ToString();
            _maxHpText.text = "/" + _maxHp.ToString();
        }

        public void ResetText(int hp, int maxHp)
        {
            _hp = hp;
            _maxHp = maxHp;
            _hpText.text = _hp.ToString();
            _maxHpText.text = "/" + _maxHp.ToString();
        }
    }
}