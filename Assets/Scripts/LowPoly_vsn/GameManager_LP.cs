using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BankStatus { None, RedBank, YellowBank, Boat};

public class GameManager_LP : MonoBehaviour {

  public enum GameStatus { None, NeedBringBoat, BoatNear, Won, Lost};
  public static GameStatus gameStatus;

  public Transform RedBankMarker;
  public Transform YellowBankMarker;

  public static GameManager_LP Instance;

  //[HideInInspector]
  public Vector3 wolfSpotYellowBank;
  public Vector3 wolfSpotRedBank;

  //[HideInInspector]
  public Vector3 wolfRotationYellowBank;
  public Vector3 wolfRotationRedBank;

  //[HideInInspector]
  public Vector3 chickenSpotYellowBank;
  public Vector3 chickenSpotRedBank;
  //[HideInInspector]
  public Vector3 chickenRotationYellowBank;
  public Vector3 chickenRotationRedBank;

  //[HideInInspector]
  public Vector3 cabbageSpotYellowBank;
  public Vector3 cabbageSpotRedBank;
  //[HideInInspector]
  public Vector3 cabbageRotationYellowBank;
  public Vector3 cabbageRotationRedBank;

  public List<Animal_LP> AnimalList;

  public Animal_LP wolf;
  public Animal_LP chicken;
  public Animal_LP cabbage;

	// Use this for initialization
	void Awake () {
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
