using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollisionTarget : CollisionTarget
{
	public override bool TestCollision(GameObject obj)
	{
		if(obj.transform.parent.transform.name == TargetName)
		{
			return true;
		}

		return false;
	}  
}
