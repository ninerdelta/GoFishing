using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework.Controls;

public class FishingRod : MonoBehaviour
{
  private SideToSideGesture sideToSideGesture;
  private CircleInputGesture circleInputGesture;
  private Vector3 rodPosition;
  public Transform ReelHandle;
  public GameObject Bobber;
  private Transform bobberTransform;  
  private SpringJoint bobberSpring;  
  public Transform TopOfFishingRod;

  private Vector3 lineDistance;
  private float lastArcLength;

  private float reelRadius = 0.05f;
  private float reelCircumference;

  public GameObject LineCastAnchor;
  private Vector3 anchorStartPosition;
  private Rigidbody lineCastAnchorRB;
  public Transform LineCastGuide;
  private Vector3 guideStartPosition;
  private Vector3 lastAnchorPosition;
  private float castingLineDistance;  
  public Vector3 castDirection;
  public float castForce = 100.0f;
  
  private bool inPond = false;

  public UnityEngine.UI.Text Message;

  void Start()
  {
    // testMachine = GetComponent<Animator>();

    // cache components    
    sideToSideGesture = gameObject.GetComponent<SideToSideGesture>();    
    circleInputGesture = gameObject.GetComponent<CircleInputGesture>();

    bobberTransform = Bobber.GetComponent<Transform>();
    bobberSpring = Bobber.GetComponent<SpringJoint>();    

    lineCastAnchorRB = LineCastAnchor.GetComponent<Rigidbody>();
    lineCastAnchorRB.isKinematic = true;

    // 2 * pi * r with r = 1.5
    reelCircumference =  2 * Mathf.PI * reelRadius;
    rodPosition = transform.position;

    guideStartPosition = TopOfFishingRod.position;
    lastAnchorPosition = lineCastAnchorRB.position;
    anchorStartPosition = lineCastAnchorRB.position;

    LineBobber.PondCollision += OnPondCollision;
    LineBobber.RodCollision += OnBringBack;    
  }
  
  void Update()
  {
    ReelingUpdate();
    AnchorUpdate();
  }  

  private void ReelingUpdate()
  {
    CircleInput t = circleInputGesture.GetGestureInput();    
    ReelHandle.transform.localRotation = Quaternion.AngleAxis(t.Angle, Vector3.right);

    // TODO: (matt) seems like could be part of separate component
    float arcLength = reelCircumference * t.GetAngleInRadians();
    float dt = arcLength - lastArcLength;
    lastArcLength = arcLength;
    lineDistance = new Vector3(0,0,dt);

    // align the forward axis of the bobber
    // to the fishing rod
    bobberTransform.LookAt(TopOfFishingRod);
    bobberTransform.Translate(lineDistance);

    bobberSpring.maxDistance -= (lineDistance.z);
  }

  private void AnchorUpdate()
  {
    // get the change in position of the "throw" anchor
    Vector3 currentAnchorPosition = lineCastAnchorRB.position;
    Vector3 deltaAnchorPosition = currentAnchorPosition - lastAnchorPosition;
    lastAnchorPosition = currentAnchorPosition;

    // get the distance of the "fishing line" and get delta
    castingLineDistance = (TopOfFishingRod.position - bobberTransform.position).magnitude;   
    
    LineCastGuide.Translate(deltaAnchorPosition);
  }

  void FixedUpdate()
  {
#if UNITY_ANDROID
    var deviceInput = -Input.acceleration.y * 1000.0f;
    if (deviceInput < 200 && Input.deviceOrientation == DeviceOrientation.Portrait && !inPond)
    {
      Message.text = deviceInput.ToString();
      lineCastAnchorRB.isKinematic = false;
      lineCastAnchorRB.AddForce(castDirection * castForce);
    }
#else
    if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.A) && !inPond)
    {
      lineCastAnchorRB.isKinematic = false;      
      lineCastAnchorRB.AddForce(castDirection * castForce);
    }
#endif
  }

  void OnPondCollision()
  {
    if(!inPond)
    {      
      bobberSpring.maxDistance = castingLineDistance + 1.0f;

      lineCastAnchorRB.isKinematic = true;
      lineCastAnchorRB.position = anchorStartPosition;
      lineCastAnchorRB.rotation = Quaternion.identity;
      lastAnchorPosition = lineCastAnchorRB.position;

      LineCastGuide.position = guideStartPosition;
      inPond = true;
    }   
  }

  void OnBringBack()
  {
    inPond = false;    
  }
}
