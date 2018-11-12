using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
  public abstract class CollisionTarget : MonoBehaviour
  {
    public string TargetName;
    public abstract bool TestCollision(GameObject obj);
  }
}
