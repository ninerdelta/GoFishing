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
  public GameObject LineBobber;
  private Transform bobberTransform;
  private SpringJoint bobberSpring;
  private Rigidbody bobberRb;
  public Transform TopOfFishingRod;

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

  // Use this for initialization
  void Start()
  {
    sideToSideGesture = gameObject.GetComponent<SideToSideGesture>();    
    circleInputGesture = gameObject.GetComponent<CircleInputGesture>();

    bobberTransform = LineBobber.GetComponent<Transform>();
    bobberSpring = LineBobber.GetComponent<SpringJoint>();
    bobberRb = LineBobber.GetComponent<Rigidbody>();

    // 2 * pi * r with r = 1.5
    reelCircumference =  2 * Mathf.PI * reelRadius;
    rodPosition = transform.position;
    
    TestRB = TestBlock.GetComponent<Rigidbody>();
    TestRB.isKinematic = true;

    lastPosition = TestRB.position;
  }

  // Update is called once per frame
  void Update()
  {
    CircleInput t = circleInputGesture.GetGestureInput();
    ReelHandle.transform.localRotation = Quaternion.AngleAxis(t.Angle, Vector3.right);

    // align the forward axis of the bobber
    // to the fishing rod
    bobberTransform.LookAt(TopOfFishingRod);

    float arcLength = 1.5f * t.GetAngleInRadians();
    float dt = arcLength - lastArcLength;
    lastArcLength = arcLength;

    Vector3 heyo = TestRB.position;
    Vector3 deltaPosition = heyo - lastPosition;
    lastPosition = heyo;

    testDistance = (heyo).magnitude;
    float deltaDistance = testDistance - lastTestDistance;
    lastTestDistance = testDistance;

    lineDistance = new Vector3(0,0,dt);
    bobberTransform.Translate(lineDistance); 
    
    KinGuide.Translate(deltaPosition);    
    
    bobberSpring.maxDistance -= (lineDistance.z);
  }

  void FixedUpdate()
  {
    if (Input.GetMouseButtonDown(0) && Input.GetKeyDown(KeyCode.A))
    {
      TestRB.isKinematic = false;
      testDir = testForce * new Vector3(0,1,1);
      TestRB.AddForce(testDir * testForce);
    }
  }
}
