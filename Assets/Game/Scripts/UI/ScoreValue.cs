using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreValue : MonoBehaviour
{
  [SerializeField]
	private Text scoreValue;
	private int scoreIntValue = 0;

	void Reset()
	{
		scoreValue = GetComponent<Text>();
		scoreValue.text = scoreIntValue.ToString();		
	}

	public void UpdateValue(int value)
	{
		scoreIntValue += value;
		scoreValue.text = scoreIntValue.ToString();
	}
}
