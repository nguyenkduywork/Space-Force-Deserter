using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    
    private Canvas main;
    public HowToPlay howTo;
    private void Start()
    {
        main = GetComponent<Canvas>();
        
    }

    public void OnPlayButton ()
    {
        SceneManager.LoadScene(1);
    }
    public void OnQuitButton ()
    {
        Application.Quit();
    }

    public void OnHowToButton ()
    {
        main.enabled = false;
        howTo.Enable();
    }

    public void Enable()
    {
        main.enabled = true;
    }
}
