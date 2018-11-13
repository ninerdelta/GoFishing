using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
  // Use this for initialization
  void Start()
  {
		gameObject.GetComponent<Button>().onClick.AddListener(ResetGame);
  }

	void ResetGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
