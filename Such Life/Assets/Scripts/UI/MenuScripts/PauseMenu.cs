using ICSharpCode.NRefactory.Ast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlasticPipe.Server.MonitorStats;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    private PlayerControl UIController;
    private void Start()
    {
        UIController = new PlayerControl();
    }

    private void OnEnable()
    {
        UIController.Enable();
        UIController.UI.Pause.performed += PauseFunct;
    }

    // Remove disabled functions
    private void OnDisable()
    {
        UIController.Disable();
        UIController.UI.Pause.performed -= PauseFunct;
    }

    private void PauseFunct(InputAction.CallbackContext context)
    {
        Debug.Log("pause function");
        if (GamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Debug.Log("RESUME");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        Debug.Log("PAUSE");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
}
