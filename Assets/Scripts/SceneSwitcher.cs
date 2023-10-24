using UnityEngine.SceneManagement;

namespace Tuong
{
    public class SceneSwitcher
    {
        public static void SwitchToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}