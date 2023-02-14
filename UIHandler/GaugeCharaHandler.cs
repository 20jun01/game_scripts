using UnityEngine;
using UnityEngine.UI;
using Util;

namespace UIHandler
{
    public class GaugeCharaHandler : MonoBehaviour
    {
        [SerializeField] private GameObject hpGauge;
        [SerializeField] private GameObject mpGauge;

        private int _countResForHp = 0;
        private int _countResForMp = 0;
        
        private float _damageRes = 0f;
        private float _consMpRes = 0f;
       
        private readonly float _ratio = Constants.StartHpRatio - Constants.LastHpRatio;
        // private readonly float _ratioMp = Constants.StartMpRatio - Constants.LastMpRatio;
        
        // Start is called before the first frame update
        void Start()
        {
            hpGauge.GetComponent<Image>().fillAmount = Constants.StartHpRatio;
            mpGauge.GetComponent<Image>().fillAmount = Constants.StartMpRatio;
        }

        // 50回で1秒
        void FixedUpdate()
        {
            if (_countResForHp > 0)
            {
                var updateValue = _damageRes / (float)_countResForHp;
                hpGauge.GetComponent<Image>().fillAmount -= updateValue;
                _damageRes -= updateValue;
                _countResForHp--;
            }

            if (_countResForMp > 0)
            {
                var updateValue = _consMpRes / (float)_countResForMp;
                mpGauge.GetComponent<Image>().fillAmount -= updateValue;
                _consMpRes -= updateValue;
                _countResForMp--;
            }
        }

        public void MinusHpGauge(float value)
        {
            _damageRes += value * _ratio;
            _countResForHp = (int)(Constants.DamageTime * 50f);
        }

        public void MinusMpGauge(float value)
        {
            _consMpRes += value * _ratio;
            _countResForMp = (int)(Constants.DamageTime * 50f);
        }
       
        public void ResetHpGauge()
        {
            hpGauge.GetComponent<Image>().fillAmount = Constants.StartHpRatio;
        }

        public void ResetMpGauge()
        {
            mpGauge.GetComponent<Image>().fillAmount = Constants.StartMpRatio;
        }

        public void ResetHpGaugeTo(float value)
        {
            hpGauge.GetComponent<Image>().fillAmount = value * _ratio + Constants.LastHpRatio;
        }

        public void ResetMpGaugeTo(float value)
        {
            mpGauge.GetComponent<Image>().fillAmount = value * _ratio + Constants.LastMpRatio;
        }

        public void MinusHpWithValue(int damage, int maxHp)
        {
            var value = (float)damage / (float)maxHp;
            MinusHpGauge(value);
        }

        public void MinusMpWithValue(int cons, int maxMp)
        {
            var value = (float)cons / (float)maxMp;
            MinusMpGauge(value);
        }

        public void ResetBothGauge()
        {
            ResetHpGauge();
            ResetMpGauge();
        }
    }
}