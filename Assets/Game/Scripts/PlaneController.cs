using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class PlaneController : MonoBehaviour
{
  [SerializeField]
  private ARPlaneManager planeManager;

  [SerializeField]
  private ARSessionOrigin arSession;

  private List<ARRaycastHit> arHitResults = new List<ARRaycastHit>();

  private bool isPlaced = false;

  public Text ShowMessage;

  public GameObject planePrefab;

  public static event Action<Vector3> PlacedPlane = (Vector3 position) => { };

  void Reset()
  {
    planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
    arSession = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
  }

  void Start()
  {
    if (planeManager == null)
    {
      planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
    }

    if (arSession == null)
    {
      arSession = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>();
    }

    planeManager.planeAdded += OnPlaneAdded;		
  }

	// adds planes if they are horizontal
  void OnPlaneAdded(ARPlaneAddedEventArgs args)
  {
    if (args.plane.boundedPlane.Alignment == PlaneAlignment.Horizontal && !isPlaced)
    {
			ShowMessage.text = "Placing! " + args.plane.boundedPlane.Pose.position.ToString();
			StartCoroutine(AutoHitTest());
			isPlaced = true;
    }
  }   

  private IEnumerator AutoHitTest()
  {
		float centerX = Screen.width/2.0f;
		float centerY = Screen.height/2.0f;

    yield return new WaitUntil(
      () => {
					ShowMessage.text = "Auto placing";
					return arSession.Raycast(new Vector3(centerX, centerY, 0), arHitResults, TrackableType.PlaneWithinInfinity);
			});

    ShowMessage.text = "Auto placed";
    isPlaced = true;
    var positionOfInterest = arHitResults[0].pose.position;
    PlacedPlane(positionOfInterest);
  }
}
