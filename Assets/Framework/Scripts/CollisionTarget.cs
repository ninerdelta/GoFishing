using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionTarget : MonoBehaviour
{
	public string TargetName;
	public abstract bool TestCollision(GameObject obj);
}
