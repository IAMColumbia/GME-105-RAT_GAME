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
        SceneManager.LoadScene("LevelDemo");
    }

    public void CharacterScene()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
