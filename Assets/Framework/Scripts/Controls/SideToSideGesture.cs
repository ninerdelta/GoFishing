using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Controls
{
  public class SideToSideGesture : MonoBehaviour
  {
    private Vector2 screenCenter;
    private Vector2 lastInput;
    Vector2 deltaInput;
    private bool isDown = false;
    private Camera mainCamera;

    void Start()
    {
      screenCenter.x = Screen.width / 2;
      screenCenter.y = Screen.height / 2;

      mainCamera = Camera.main;
    }

    void Update()
    {
      if (Input.GetMouseButtonDown(0) && !isDown)
      {
        isDown = true;
      }
      else if (isDown)
      {

        Vector3 p = Input.mousePosition;
        p.z = -1.0f * mainCamera.transform.position.z;

        // convert screen space input to world space
        // and make a delta
        Vector3 t = mainCamera.ScreenToWorldPoint(p);
        deltaInput = (Vector2)t - lastInput;
        lastInput = t;
      }

      if (Input.GetMouseButtonUp(0))
      {
        isDown = false;
      }
    }

    public Vector2 GetPosition()
    {
      return lastInput;
    }
  }
}