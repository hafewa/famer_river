using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerStatus { None, DraggingBoat, EngagedWithAnimal };

public class Player_LP : MonoBehaviour {

  public PlayerStatus playerStatus;

  public static Player_LP Instance;

  public GameObject thingToPull;

  public bool pullingBoat;



  void Awake(){
    if (Instance == null)
      Instance = this;
    else
      Destroy(Instance);
  }

  void Update()
  {
    if(pullingBoat){
      PullBoat();
    }

  }

  public void PullBoat(){
    if (thingToPull)
    {
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
        pullF += pullForDist;
        // Now apply to pull force, using standard meters/sec converted
        //    into meters/frame:
        thingToPull.transform.GetComponent<Rigidbody>().velocity += pullDir * (pullF * Time.deltaTime);
      }
    }
  }

}
