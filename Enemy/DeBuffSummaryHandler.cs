using TMPro;
using UnityEngine;
using Util;

namespace Enemy
{
    public class DeBuffSummaryHandler : MonoBehaviour
    {
        private GameObject _deBuffSummary;

        private void Start()
        {
            _deBuffSummary = this.gameObject;
            for (int i = 0; i < _deBuffSummary.transform.childCount; i++)
            {
                var iconObj = _deBuffSummary.transform.GetChild(i).gameObject;
                iconObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0";
            }
        }

        public void ChangeDeBuffIconWithRatio(EffectTypeOnDeath effectType, float ratio)
        {
            string effect = Function.ConvertEffectTypeOnDeathToString(effectType);
            for(int i = 0;i < _deBuffSummary.transform.childCount;i++)
            {
                var iconObj = _deBuffSummary.transform.GetChild(i).gameObject;
                if(iconObj.name == effect)
                {
                    var nowPer = iconObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                    var nowPerInt = int.Parse(nowPer);
                    var newPerInt = 100 - (int)(((100 - nowPerInt) * (int)(ratio * 100)) / 100);
                    iconObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = newPerInt.ToString();
                }
            }
        }
    }
}