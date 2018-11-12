using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework;

public class FishingRodCollisionTarget : CollisionTarget
{
	public override bool TestCollision(GameObject obj)
	{
		if(obj.name == TargetName)
		{
			return true;
		}

		return false;
	}
}
