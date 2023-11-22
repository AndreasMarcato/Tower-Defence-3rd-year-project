using UnityEngine;

public class UIManager_Home : MonoBehaviour
{
    public void LoadScene(int sceneBuildIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneBuildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
