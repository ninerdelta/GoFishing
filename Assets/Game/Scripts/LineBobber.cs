using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: (matt) update to use standard C# event pattern

public class LineBobber : MonoBehaviour
{
	private WaterCollisionTarget waterTarget;
	private FishingRodCollisionTarget fishingRodTarget;

	public static event Action PondCollision = () => {};
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
	}

  void OnCollisionEnter(Collision collision)	
	{				
		if(waterTarget.TestCollision(collision.gameObject))
		{
			PondCollision();
		}		
	}

	void OnTriggerEnter(Collider other)
	{
		if(fishingRodTarget.TestCollision(other.gameObject))
		{
			RodCollision();
		}
	}
}
