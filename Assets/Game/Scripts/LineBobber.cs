using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: (matt) needs a state machine
public class LineBobber : MonoBehaviour
{
	public static event Action PondCollision = () => {};
	public static event Action RodCollision = () => {};

  void OnCollisionEnter(Collision collision)
	{
		// fire off event when hits pond		
		if(collision.gameObject.name == "Pond")
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
