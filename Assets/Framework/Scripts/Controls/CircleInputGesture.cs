using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Controls
{
  public struct CircleInput
  {
    public int RepCount;
    // angle is in degrees
    public float Angle;    
    
    public CircleInput(int reps, float angle)
    {
      RepCount = reps;
      Angle = angle;      
    }

    public float GetAngleInRadians()
    {
      return Angle * Mathf.Deg2Rad;
    }
  }

  // TODO: (matt) when is a good time to reset the angle?
  public class CircleInputGesture : MonoBehaviour
  {
    private Vector2 currentInput;
    private Vector2 startInput;
    // create virtual center point to measure
    // rotations around
    private readonly Vector2 gestureCenterOffset = new Vector2(0.0f, 5.0f);
    private Vector2 gestureCenter;
    private Vector2 gestureStart;
    private Vector2 currentGesture;
    private Vector2 lastGesture;
    private bool isDown = false;
    private float angleSum;    
    private int repCount;
    
    void Update()
    {
      if (Input.GetMouseButtonDown(0) && !isDown)
      {
        startInput = Input.mousePosition;

        // place a "center point" to measure rotation about
        gestureCenter = startInput - gestureCenterOffset;
        // radius to center
        gestureStart = startInput - gestureCenter;
        lastGesture = gestureStart;

        isDown = true;
      }
      else if (isDown)
      {
        // prep inputs for angle calculation
        currentInput = Input.mousePosition;
        currentGesture = currentInput - gestureCenter;

        // angle calculation
        float dotProd = Vector2.Dot(currentGesture.normalized, lastGesture.normalized);
        // check wich direction the rotation is
        Vector3 signCheck = Vector3.Cross(currentGesture, lastGesture);
        // switch signs         
        float flipDirection = ((signCheck.z < 0) ? -1.0f : 1.0f);      

        // dot product may be 1 if no change i.e. lastGesture = currentGesture
        if (dotProd < 1.0f && dotProd > -1.0f)
        {
          float deltaAngle = flipDirection * (Mathf.Acos(dotProd) * Mathf.Rad2Deg);
          angleSum += deltaAngle;
        }

        lastGesture = currentGesture;
      }

      if (Input.GetMouseButtonUp(0))
      {        
        isDown = false;
      }

      if (isDown && angleSum > 359)
      {
        // circle completed	
        ++repCount;
      }
    }

    public CircleInput GetGestureInput()
    {
      return new CircleInput(repCount, angleSum);
    }
  }
}
