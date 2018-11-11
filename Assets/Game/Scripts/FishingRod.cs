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
  private Rigidbody bobberRb;
  public Transform TopOfFishingRod;
  public Transform BringBack;

  public Vector3 lineDistance;
  public float lastArcLength;

  private float reelRadius = 1.5f;
  private float reelCircumference;

  public GameObject TestBlock;
  public Rigidbody TestRB;
  public Vector3 testDir = new Vector3(0, 1, 1);
  [SerializeField]
  public float testForce = 100.0f;
  public float testDistance;
  float lastTestDistance;
  public Transform KinGuide;
  public Vector3 lastPosition;
  public Vector3 guideStartPosition;
  Vector3 anchorStartPosition;

  public Animator testMachine; 


  // TODO: (matt) sign that a state machine is needed
  bool inPond = false;

  void Start()
  {
    testMachine = GetComponent<Animator>();    

    sideToSideGesture = gameObject.GetComponent<SideToSideGesture>();    
    circleInputGesture = gameObject.GetComponent<CircleInputGesture>();

    bobberTransform = Bobber.GetComponent<Transform>();
    bobberSpring = Bobber.GetComponent<SpringJoint>();
    bobberRb = Bobber.GetComponent<Rigidbody>();

    guideStartPosition = TopOfFishingRod.position;

    // 2 * pi * r with r = 1.5
    reelCircumference =  2 * Mathf.PI * reelRadius;
    rodPosition = transform.position;
    
    TestRB = TestBlock.GetComponent<Rigidbody>();
    TestRB.isKinematic = true;

    lastPosition = TestRB.position;
    anchorStartPosition = TestRB.position;

    LineBobber.PondCollision += OnPondCollision;
    LineBobber.RodCollision += OnBringBack;    
  }
  
  void Update()
  {
    // align the forward axis of the bobber
    // to the fishing rod
    bobberTransform.LookAt(TopOfFishingRod);

    ReelingUpdate();



    // get the change in position of the "throw" anchor
    Vector3 heyo = TestRB.position;
    Vector3 deltaPosition = heyo - lastPosition;
    lastPosition = heyo;

    // get the distance of the "fishing line"
    testDistance = (TopOfFishingRod.position - heyo).magnitude;
    float deltaDistance = testDistance - lastTestDistance;
    lastTestDistance = testDistance;

    // float dt = arcLength - lastArcLength;
    // lastArcLength = arcLength;
    // lineDistance = new Vector3(0,0,dt);
    bobberTransform.Translate(lineDistance); 
    
    KinGuide.Translate(deltaPosition);
    bobberSpring.maxDistance -= (lineDistance.z);
  }

  public void ReelingUpdate()
  {
    CircleInput t = circleInputGesture.GetGestureInput();
    ReelHandle.transform.localRotation = Quaternion.AngleAxis(t.Angle, Vector3.right);
    float arcLength = 1.5f * t.GetAngleInRadians();
    float dt = arcLength - lastArcLength;
    lastArcLength = arcLength;
    lineDistance = new Vector3(0,0,dt);
  }

  void FixedUpdate()
  {
    if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.A))
    {
      testMachine.SetBool("isCast", true);
      TestRB.isKinematic = false;      
      TestRB.AddForce(testDir * testForce);
    }
  }

  void OnPondCollision()
  {
    if(!inPond)
    {      
      bobberSpring.maxDistance = testDistance + 1.0f;

      TestRB.isKinematic = true;
      TestRB.position = anchorStartPosition;
      TestRB.rotation = Quaternion.identity;
      lastPosition = TestRB.position;

      KinGuide.position = guideStartPosition;
      inPond = true;
    }   
  }

  void OnBringBack()
  {    
    inPond = false;    
  }
}
