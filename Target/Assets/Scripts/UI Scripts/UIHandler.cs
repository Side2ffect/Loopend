using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public void GoToGameSelection()
    {
        SceneManager.LoadScene(TagManager.GAMEPLAY_SELECTION_SCENE);
    }
    public void GoToEnemyGameSelection()
    {
        SceneManager.LoadScene(TagManager.ENEMY_GAMEPLAY_SCENE);
    }
    public void GoToRideGameSelection()
    {
        SceneManager.LoadScene(TagManager.RIDE_GAMEPLAY_SCENE);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(TagManager.MAINMENU_SCENE);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
