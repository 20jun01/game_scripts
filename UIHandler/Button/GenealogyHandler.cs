using Manager;
using UnityEngine;

namespace UIHandler.Button
{
    public class GenealogyHandler : MonoBehaviour
    {
        private GameObject _genealogy;
        private GameObject _genealogyZoom;
        
        private GameObject _eventSystem;

        private void Awake()
        {
            _genealogy = GameObject.FindWithTag("Genealogy");
            _genealogyZoom = GameObject.FindWithTag("GenealogyZoom");
            _eventSystem = GameObject.Find("EventSystem");
        }

        void Start()
        {
            _genealogyZoom.SetActive(false);
        }

        public void ZoomGenealogy()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToGenealogyZoom();
        }
        
        public void UnZoomGenealogy()
        {
            _eventSystem.GetComponent<SelectPhaseChangeManager>().ChangeToGenealogy();
        }
    }
}