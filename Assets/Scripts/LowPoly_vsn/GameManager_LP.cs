using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_LP : MonoBehaviour {

  public static GameManager_LP Instance;
  [HideInInspector]
  public Vector3 wolfSpotYellowBank;
  public Vector3 wolfSpotRedBank;

  [HideInInspector]
  public Vector3 wolfRotationYellowBank;
  public Vector3 wolfRotationRedBank;

  [HideInInspector]
  public Vector3 chickenSpotYellowBank;
  public Vector3 chickenSpotRedBank;
  [HideInInspector]
  public Vector3 chickenRotationYellowBank;
  public Vector3 chickenRotationRedBank;

  [HideInInspector]
  public Vector3 cabbageSpotYellowBank;
  public Vector3 cabbageSpotRedBank;
  [HideInInspector]
  public Vector3 cabbageRotationYellowBank;
  public Vector3 cabbageRotationRedBank;

	// Use this for initialization
	void Start () {
    Instance = this;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
