using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchSceneFunction(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void Quit(string quit)
    {
        Application.Quit();
    }
}
