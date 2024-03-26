using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
	// Change active scene
	public void MoveToScene(int sceneID)
	{
		SceneManager.LoadScene(sceneID);
	}
}
