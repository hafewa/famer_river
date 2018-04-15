using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GazeStatus { None, Wolf, Chicken, Cabbage, Boat };

public class PlayerGaze : MonoBehaviour
{
  public GameObject objectOfMyGaze;
  public GazeStatus myGazeStatus;

  public static PlayerGaze Instance;

  public delegate void GazeHitWolf();
  public static event GazeHitWolf OnGazeHitWolf;

  public delegate void GazeHitBoat();
  public static event GazeHitBoat OnGazeHitBoat;

  public delegate void GazeHitChicken();
  public static event GazeHitChicken OnGazeHitChicken;

  public delegate void GazeHitCabbage();
  public static event GazeHitCabbage OnGazeHitCabbage;

  //Responsibilities:
  /// <summary>
  /// This script's bgi responsibility is shooting out a ray from the players gaze (ie the forward direction of the normal)
  /// and seeing if it hits an object of type Animal_LP and if so, which animal it is...
  /// </summary>

  void Awake(){
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
  }

  // Use this for initialization
  void Start()
  {
    //objectOfMyGaze = null;
    myGazeStatus = GazeStatus.None;
  }


  // Update is called once per frame
  void Update()
  {

    RaycastHit hit = new RaycastHit();
    // Does the ray intersect any objects excluding the player layer
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f))
    {
      Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      switch (hit.collider.gameObject.name)
      {
        case "Wolf":
          if (OnGazeHitWolf != null)
          {
            OnGazeHitWolf();
            objectOfMyGaze = hit.collider.gameObject;
          }
          break;
        case "Chicken":
          if (OnGazeHitChicken != null)
          {
            OnGazeHitChicken();
            objectOfMyGaze = hit.collider.gameObject;
          }
          break;
        case "Cabbage":
          if (OnGazeHitCabbage != null)
          {
            OnGazeHitCabbage();
            objectOfMyGaze = hit.collider.gameObject;
          }
          break;
        case "Boat":
          if (OnGazeHitBoat != null)
          {
            OnGazeHitBoat();
            objectOfMyGaze = hit.collider.gameObject;
          }
          break;
      }

      //if(hit.collider.gameObject.name != "Wolf" && hit.collider.gameObject.name != "Chicken" && hit.collider.gameObject.name != "Cabbage" && hit.collider.gameObject.name != "Boat" ){
      //  RemoveText();
      //}

    } else {
      if (myGazeStatus != GazeStatus.None)
      {
        ClearGaze();
      }
    }
  }

  public void ClearGaze()
  {
    if (Player_LP.Instance.playerStatus != PlayerStatus.DraggingBoat)
    {
      if (UIManager_LP.Instance.textInstructionsPresent)
      {
        Debug.Log("Removing Text instructions");
        myGazeStatus = GazeStatus.None;
        StartCoroutine(UIManager_LP.Instance.InstructionsTextOutgoing());
        objectOfMyGaze = null;
      }
    }
  }


}


