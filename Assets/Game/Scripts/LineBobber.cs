using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBobber : MonoBehaviour
{
	private WaterCollisionTarget waterTarget;
	private FishingRodCollisionTarget fishingRodTarget;

// TODO: (matt) update to use standard C# event pattern
	public static event Action PondCollision = () => {};
	public static event Action PondCollisionExit = () => {};
	public static event Action RodCollision = () => {};

	void Start()
	{
		var obj = GameObject.Find("WaterParent");
		if(obj == null)
		{
			print("Water parent missing from scene");
			return;
		}

		waterTarget = obj.GetComponent<WaterCollisionTarget>();
		fishingRodTarget = transform.parent.GetComponentInChildren<FishingRodCollisionTarget>(); 
		if(fishingRodTarget == null)
		{
			print("Fishing rod target not found");
		}

		CatchController.FishCaught += AddCatch;
		CatchController.FishLanded += RemoveCatch;
	}

  private void OnCollisionEnter(Collision collision)	
	{				
		if(waterTarget.TestCollision(collision.gameObject))
		{
			PondCollision();
		}		
	}

	private void OnCollisionExit(Collision collision)
	{
    if (waterTarget.TestCollision(collision.gameObject))
    {
      PondCollisionExit();
    }
  }

	private void OnTriggerEnter(Collider other)
	{
		if(fishingRodTarget.TestCollision(other.gameObject))
		{
			RodCollision();			
		}
	}

	private void AddCatch(Fish fish)
	{
		fish.Caught.transform.SetParent(transform);
		fish.Caught.transform.localPosition = new Vector3(0, 0, -1.0f);
	}

	private void RemoveCatch(Fish fish)
	{
		var obj = transform.GetChild(0);
		Destroy(obj.gameObject);
	}
}
