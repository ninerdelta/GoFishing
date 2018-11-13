using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
  // Use this for initialization
  void Start()
  {
		gameObject.GetComponent<Button>().onClick.AddListener(ResetGame);
  }

	void ResetGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
