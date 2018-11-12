using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpView : MonoBehaviour
{
	[SerializeField]
	private GameObject popUp;

	[SerializeField]
	private Button closePopUpButton;

	void Reset()
	{
		popUp = GameObject.Find("CatchPopUp");
		closePopUpButton = GetComponent<Button>();
	}

	void Start()
	{
		closePopUpButton.onClick.AddListener(OnButtonClick);
	}

	public void SetActive(bool state)
	{
		popUp.SetActive(state);
		closePopUpButton.enabled = true;
	}

	private void OnButtonClick()
	{
		popUp.SetActive(false);
		closePopUpButton.enabled = false;
	}
}
