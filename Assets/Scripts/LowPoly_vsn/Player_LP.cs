using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_LP : MonoBehaviour {

  public enum PlayerStatus { None, DraggingBoat, EngagedWithAnimal};
  public PlayerStatus playerStatus;

  public static Player_LP Instance;

  public GameObject thingToPull;



  void Awake(){
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
  }

  void Update()
  {

    if (Input.GetKeyDown(KeyCode.Space))
    {

      if (playerStatus.Equals(PlayerStatus.DraggingBoat))
      {
        thingToPull = null;
        playerStatus = PlayerStatus.None;
        Debug.Log("PlayerStatus is dragging the boat...");
      }

      switch (PlayerGaze.Instance.myGazeStatus)
      {
        case PlayerGaze.GazeStatus.Boat:
          thingToPull = Boat_LP.Instance.gameObject;
          playerStatus = PlayerStatus.DraggingBoat;
          StartCoroutine(PlayerGaze.Instance.InstructionsTextIncoming(String.Format("Press Space to release the boat")));
          break;
        case PlayerGaze.GazeStatus.Wolf:
          
          break;

      }
    }



    if(thingToPull){
      Vector3 D = transform.position - thingToPull.transform.position; // line from crate to player
      float dist = D.magnitude;
      Vector3 pullDir = D.normalized; // short blue arrow from crate to player
      if (dist > 50) thingToPull = null; // lose tracking if too far
      else if (dist > 1)
      { // don't pull if too close
        // this is the same math to apply fake gravity. 10 = normal gravity
        float pullF = 1;
        // for fun, pull a little bit more if further away:
        // (so, random, optional junk):
        float pullForDist = (dist - 3) / 2.0f;
        if (pullForDist > 20) pullForDist = 20;
        //pullF += pullForDist;
        // Now apply to pull force, using standard meters/sec converted
        //    into meters/frame:
        thingToPull.transform.GetComponent<Rigidbody>().velocity += pullDir * (pullF * Time.deltaTime);
      }
    }


  }
}
