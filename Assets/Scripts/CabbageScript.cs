using UnityEngine;
using System.Collections;

public class CabbageScript : Animal {

  public static GameObject myMarkInBoat;

	// Use this for initialization
	void Start () {
    myMarkInBoat = GameObject.FindWithTag("cab_mark_in_boat");
    base.GetMyState();	
	}
	
	// Update is called once per frame
	void Update () {
	}

  public static void PlaceInBoat(){
    
    gameObject.transform.position = myMarkInBoat.transform.position;
  }

}
