using UnityEngine;

namespace Manager
{
    public class SceneNameManager : MonoBehaviour
    {
        public static string SceneName = "Start";
        // Start is called before the first frame update
        void Awake()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void SetSceneName(string name)
        {
            SceneName = name;
        }

        public static string GetSceneName()
        {
            return SceneName;
        }
    }

}