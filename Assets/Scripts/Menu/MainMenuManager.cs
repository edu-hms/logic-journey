using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private string levelName;

    [SerializeField]
    private GameObject menuInitial;

    [SerializeField]
    private GameObject menuOptions;

    public void Play()
    {
        SceneManager.LoadScene(levelName);
    }

    public void OpenOptions()
    {
        menuInitial.SetActive(false);
        menuOptions.SetActive(true);
    }

    public void ExitOptions()
    {
        menuInitial.SetActive(true);
        menuOptions.SetActive(false);
    }

    public void Controls()
    {

    }


}
