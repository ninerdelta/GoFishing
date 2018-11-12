using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Framework;

// TODO: (matt) make an enum mapping out fish
// ad hoc mapping is
// 0 - bluegill
// 1 - catfish
// 2 - snapper
// 3 - snake
// 4 - tire
// 5 - none

public class FishController : MonoBehaviour
{
  public FishDescription FishDescription;
  
  // NOTE: (matt) just use a rigged value for now,
  // 9 being the number of "nearest neighboring" blocks
	private int numberOfFish = 9;
  
  private int[] fishTypeScores = new int[] { 5, 7, 13, -3, -10 };

  [SerializeField]
  private int[] ActiveFishTypes;

  [SerializeField]
  private int[] ActiveBiteFrequency;

  [SerializeField]
  private bool fishActive = false;

  public static event Action<int, int> FishBite = (int fishIndex, int score) => {};

  void Start()
  {    
    GenerateFish();
    LineBobber.PondCollision += ToggleFishActive;
    LineBobber.RodCollision += ToggleFishInActive;
  }

  void Update()
  {
    if(fishActive)
    {
      CheckForBite();
    }
  }

  // TODO: (matt) need a minimum wait time that is tunable
  private IEnumerator CheckForBite()
  {
    float secondsToWait = UnityEngine.Random.Range(1, 15);
    print("Waiting... " + secondsToWait);
    yield return new WaitForSeconds(secondsToWait);
    int biteIndex = RandomProbability.Rand(ActiveFishTypes, ActiveBiteFrequency, 9);    
    FishBite(biteIndex, fishTypeScores[biteIndex]);
  }

  private void GenerateFish()
  {
    // TODO: (matt) could probably switch to fish name here
    ActiveFishTypes = new int[numberOfFish];
    ActiveBiteFrequency = new int[numberOfFish];

    for(int i = 0; i < numberOfFish; ++i)
    {
      // figure out what fish are in the pond
      int fishTypeIndex = RandomProbability.Rand(FishDescription.FishType, 
                                                 FishDescription.OccurFrequency, 5);

      ActiveFishTypes[i] = FishDescription.FishType[fishTypeIndex];      
      ActiveBiteFrequency[i] = FishDescription.BiteFrequency[fishTypeIndex];
    }    
  }

  private void ToggleFishActive()
  {
    if (!fishActive)
    {
      fishActive = true;
      StartCoroutine(CheckForBite());
    }    
  }

  private void ToggleFishInActive()
  {
    fishActive = false;
  }
}
