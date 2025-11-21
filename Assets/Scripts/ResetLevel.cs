using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    // resets current scene.
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // loads game scene.
    public void GameScene()
    {
        SceneManager.LoadScene("level_one_testings_ver1");
    }

    // loads title scene.
    public void TitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    // exits play mode when in engine, exits game when build.
    public void CloseGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

}
