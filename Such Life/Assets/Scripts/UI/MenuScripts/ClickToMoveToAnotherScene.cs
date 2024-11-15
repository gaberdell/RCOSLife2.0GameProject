using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToMoveToAnotherScene : MonoBehaviour
{
    [SerializeField]
    Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(MoveToScene);
    }

    void MoveToScene()
    {
        SceneManager.LoadScene(1);
    }

}
