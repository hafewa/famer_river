using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaze : MonoBehaviour {


  public Animal_LP objectOfMyGaze;
  public enum GazeStatus { None, Wolf, Chicken, Cabbage, Boat };
  public static GazeStatus myGazeStatus;

  //Responsibilities:
  /// <summary>
  /// This script's bgi responsibility is shooting out a ray from the players gaze (ie the forward direction of the normal)
  /// and seeing if it hits an object of type Animal_LP and if so, which animal it is...
  /// </summary>


	// Use this for initialization
	void Start () {
    objectOfMyGaze = null;
    myGazeStatus = GazeStatus.None;

	}
	
	// Update is called once per frame
	void Update () {
		
    RaycastHit hit;
    // Does the ray intersect any objects excluding the player layer
    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4.0f))
    {
      Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      Debug.Log("Did Hit");
      switch(hit.collider.gameObject.name){
        case "Wolf":
          objectOfMyGaze = hit.collider.gameObject.GetComponent<Animal_LP>();
          break;
        case "Chicken":
          objectOfMyGaze = hit.collider.gameObject.GetComponent<Animal_LP>();
          break;
        case "Cabbage":
          objectOfMyGaze = hit.collider.gameObject.GetComponent<Animal_LP>();
          break;
      }
    }
    else
    {
      Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.white);
      Debug.Log("Did not Hit");
    }


	}
}
