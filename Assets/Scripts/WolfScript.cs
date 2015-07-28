using UnityEngine;
using System.Collections;

public class WolfScript : Animal {

public static GameObject myMarkInBoat;

  void Start(){
    myMarkInBoat = GameObject.FindWithTag("wolf_mark_in_boat");
    base.GetMyState();
  }

	// Update is called once per frame
	void Update () {
	
	}

  public override void PlaceInBoat(){
    gameObject.transform.position = myMarkInBoat.transform.position;
  }

}
