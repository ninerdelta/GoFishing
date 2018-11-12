using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: (matt) use standard C# event pattern

public class LineBobber : MonoBehaviour
{
	public static event Action PondCollision = () => {};
	public static event Action RodCollision = () => {};

	void Start()
	{
		
	}

  void OnCollisionEnter(Collision collision)
	{				
		// fire off event when hits pond
		// TODO: manage this in a less "implementation aware" way
		if(collision.gameObject.transform.parent.name == "WaterParent")
		{
			PondCollision();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "BringBack")
		{
			RodCollision();
		}
	}
}
