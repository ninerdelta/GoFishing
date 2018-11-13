using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReset : MonoBehaviour
{
  private void OnCollisionEnter(Collision collision)
	{		
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
