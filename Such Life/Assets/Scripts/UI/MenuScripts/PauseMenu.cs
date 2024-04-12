using ICSharpCode.NRefactory.Ast;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlasticPipe.Server.MonitorStats;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;

    [SerializeField] private playerAction UIController;
    [SerializeField] private InputAction pause;

    private void Start()
    {
        UIController = new playerAction();
    }

    private void OnEnable()
    {
        UIController.UI.Enable();

        pause = UIController.UI.Pause;
        pause.Enable();
        pause.performed += PauseFunct();

    }

    // Remove disabled functions
    private void OnDisable()
    {
        UIController.UI.Disable();
        pause.performed -= PauseFunct;
    }

    private void PauseFunct(InputAction.CallbackContext context)
    {
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }
}
