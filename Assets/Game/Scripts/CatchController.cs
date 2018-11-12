using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
	public readonly GameObject Caught;
	public readonly int Score;
	public readonly string Name;

	public Fish(GameObject obj, int score, string name)
	{
		Caught = obj;
		Score = score;
		Name = name;
	}
}

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

	// NOTE: (matt) SUPER RIGGED
  private string[] fishTypeNames = new string[] { "Bluegill", 
                                                  "Catfish", 
                                                  "Snapper", 
                                                  "Snake", 
                                                  "Tire" };

	private Fish fishOnLine;

	public static event Action<Fish> FishCaught = (Fish caught) => {};
	public static event Action<Fish> FishLanded = (Fish landed) => {};
	public static event Action FishMissed = () => {};
  
  void Start()
  {
		LineBobber.PondCollision += ToggleLineInWater;
		LineBobber.PondCollisionExit += ToggleLineInWater;
		LineBobber.RodCollision += ToggleCatchEligible;

		FishController.FishBite += OnFishBite;

		LineBobber = GameObject.Find("Bobber").GetComponent<LineBobber>();
		if(LineBobber == null)
		{
			print("Missing Bobber!");
		}
  }

	void OnFishBite(int fishIndex, int score)
	{
		if(lineInWater && lineCleared)
		{			
			lineCleared = false;
			var fish = Instantiate(FishPrototype) as GameObject;
			fish.GetComponent<MeshRenderer>().material = FishMaterials[fishIndex];
			fishOnLine = new Fish(fish, score, fishTypeNames[fishIndex]);
			FishCaught(fishOnLine);
		}
		else
		{
			print("MISSED");
			FishMissed();			
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
			FishLanded(fishOnLine);
			fishOnLine = null;
      lineCleared = !lineCleared;
    }
  }
}
