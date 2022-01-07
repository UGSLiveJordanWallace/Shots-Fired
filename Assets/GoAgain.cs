using UnityEngine;
using UnityEngine.SceneManagement;

public class GoAgain : MonoBehaviour
{
    public void RetryLevel() {
        SceneManager.LoadScene(SceneManager.sceneCount);
    }
}
