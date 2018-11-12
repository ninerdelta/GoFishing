using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchController : MonoBehaviour
{
	[SerializeField]
	private bool lineInWater = false;

	[SerializeField]
	private bool lineCleared = true;

	public LineBobber LineBobber;
	
	public GameObject FishPrototype;

	// NOTE: (matt) SUPER RIGGED
	public Material[] FishMaterials = new Material[5];

  
  void Start()
  {
		LineBobber.PondCollision += ToggleLineInWater;
		LineBobber.PondCollisionExit += ToggleLineInWater;
		LineBobber.RodCollision += ToggleCatchEligible;

		FishController.FishBite += OnFishBite;

		LineBobber = GameObject.Find("Bobber").GetComponent<LineBobber>();
		if(LineBobber == null)
		{
			print("Bummer");
		}
  }

	void OnFishBite(int fishIndex, int score)
	{
		if(lineInWater && lineCleared)
		{
			print("Caught!");
			lineCleared = false;
			var fish = Instantiate(FishPrototype) as GameObject;
			fish.GetComponent<MeshRenderer>().material = FishMaterials[fishIndex];
			LineBobber.AddCatch(fish);			
		}
		else
		{
			print("MISSED!");
		}
	}  

	void ToggleLineInWater()
	{
		lineInWater = !lineInWater;		
	}

	void ToggleCatchEligible()
	{
    if (!lineCleared)
    {
      lineCleared = !lineCleared;
    }
  }
}
