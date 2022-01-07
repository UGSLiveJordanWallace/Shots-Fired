using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void LevelUp()
    {
        if (SceneManager.sceneCount == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(1);
        } else
        {
            SceneManager.LoadScene(SceneManager.sceneCount + 1);
        }
    }
}
