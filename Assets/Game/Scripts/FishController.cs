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
  // NOTE: (matt) public for debugging visibility
  public int[] ActiveFishTypes;
  public int[] ActiveBiteFrequency;  

  // Use this for initialization
  void Start()
  {
    GenerateFish();		
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void CheckForBite()
  {
    int biteIndex = RandomProbability.Rand(ActiveFishTypes, ActiveBiteFrequency, 9);        
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
}
