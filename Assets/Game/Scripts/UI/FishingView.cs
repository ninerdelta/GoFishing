using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingView : MonoBehaviour
{
	[SerializeField]
	private ScoreValue scoreValue;

	[SerializeField] PopUpView popUpView;	

	private void Reset()
	{
		scoreValue = GameObject.Find("ScoreValue").GetComponent<ScoreValue>();
		popUpView = GameObject.Find("UpperPanel").GetComponent<PopUpView>();
	}

	void Start()
	{
		CatchController.FishLanded += OnFishLanded;
	}	
	
	private void OnFishLanded(Fish fish)
	{
		popUpView.SetActive(true);
		scoreValue.UpdateValue(fish.Score);
	}
}
