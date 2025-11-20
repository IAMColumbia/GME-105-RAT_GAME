using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    // resets current scene.
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameScene()
    {
        SceneManager.LoadScene("level_one_testings_ver1");
    }
}
