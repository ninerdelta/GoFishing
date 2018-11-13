using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: (matt) could be made up of component for creating boundary
// and componenent for filling with water and both those 
// could have statistical elements for creation

// TODO: (matt) need field for setting center

public class PondGenerator : MonoBehaviour
{
	public GameObject WallElement;
	public GameObject CornerElement;
	public GameObject WaterElement;

	// TODO: (matt) make these get set statistically instead of manually
	public int PondWidth;
	public int PondHeight;

	[SerializeField]
	private float pondDistance = 1.0f;	
	private GameObject pondParent;
	private GameObject waterParent;

	private Quaternion flipDirection = Quaternion.AngleAxis(180, Vector3.up);
	private Quaternion rotateNinety = Quaternion.AngleAxis(90, Vector3.up);
	private Quaternion rotateZero = Quaternion.identity;

	void Start()
	{
		pondParent = GameObject.Find("PondParent");
		if(pondParent == null)
		{
			print("Pond parent missing from scene");
			return;
		}

		waterParent = GameObject.Find("WaterParent");
		if(waterParent == null)
		{
			print("Water parent missing from scene");
			return;
		}
		
		pondDistance = PondHeight/2.0f;
		OnPlanePlaced(new Vector3(0, 0, PondHeight));
	}

	private void OnPlanePlaced(Vector3 position)
	{
		StartCoroutine(CreatePond());
		StartCoroutine(CreateWater());
	}

	// TODO: (matt) break some of the loops into separate functions
	private IEnumerator CreatePond()
	{		
		// closest
		for(int width = 0; width < PondWidth; ++width)
		{
			var elementPosition = new Vector3(-(PondWidth/2.0f) + width, 0, 0);

			if(width == 0)
			{
				CreatePondElement(CornerElement, elementPosition, rotateNinety);
			}
			else if(width == (PondWidth - 1))
			{
				CreatePondElement(CornerElement, elementPosition, rotateZero);
			}
			else
			{
				CreatePondElement(WallElement, elementPosition, rotateZero);
			}
		}		

		// farthest
		for(int width = 0; width < PondWidth; ++width)
		{
			var elementPosition = new Vector3(-(PondWidth/2.0f) + width, 0 , PondHeight);
			if(width == 0)
			{
				CreatePondElement(CornerElement, elementPosition, flipDirection);				
			}
			else if(width == (PondWidth - 1))
			{
				CreatePondElement(CornerElement, elementPosition, flipDirection * rotateNinety);
			}
			else
			{
				CreatePondElement(WallElement, elementPosition, flipDirection);				
			}
		}

		// left
		for(int height = 1; height < PondHeight; ++height)
		{
			var elementPosition = new Vector3(-(PondWidth/2.0f), 0, height);			
			CreatePondElement(WallElement, elementPosition, rotateNinety);
		}

		// right
		for(int height = 1; height < PondHeight; ++height)
		{
			var elementPosition = new Vector3((PondWidth/2.0f)-1, 0 , height);
			CreatePondElement(WallElement, elementPosition, rotateNinety * flipDirection);
		}

		yield return null;
	}

	private IEnumerator CreateWater()
	{
		var result = Instantiate(WaterElement, 
														 new Vector3(-0.5f, 0, PondHeight/2.0f), 
														 rotateZero) as GameObject;

		result.transform.localScale = new Vector3(PondWidth-2, 1, PondHeight-1);
		result.transform.SetParent(waterParent.transform, false);
		waterParent.transform.localScale = Vector3.one;
		
		yield return null;
	}


	private void CreatePondElement(GameObject element, Vector3 position, Quaternion rotation)
	{
		var result = Instantiate(element, position, rotation) as GameObject;
		result.transform.SetParent(pondParent.transform, false);
		pondParent.transform.localScale = Vector3.one;
	}
}
