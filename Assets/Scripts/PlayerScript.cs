using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
  
  public Text myText;
  public static bool inBoat;
  public static bool beStill;
  public delegate void BoatEvent();
  public static event BoatEvent OnBoatLaunch;
//  public delegate void PlaceInBoat(string name);
//  public static event PlaceInBoat OnPlace; 
  public delegate void ChxToBoat();
  public static event ChxToBoat OnChxToBoat;
  public delegate void WolfToBoat();
  public static event WolfToBoat OnWolfToBoat;
  public delegate void CabToBoat();
  public static event CabToBoat OnCabToBoat;

  public void OnCollisionEnter(Collision other){
    switch(other.gameObject.tag){
      case "Wolf": 
        Debug.Log("Wolf Collider");
        myText.text = "Press \"w\" to place the wolf in the boat";
        break;
      case "Chicken":
        Debug.Log("Chicken Collider");        
        myText.text = "Press \"c\" to place the chicken in the boat";
        break;
      case "Cabbage":
        Debug.Log("Cabbage Collider");
        myText.text = "Press \"g\" to place the cabbage in the boat"; 
        break;
      case "River_Wall":
        Debug.Log("River Collider");
        if(!inBoat)
          myText.text = "The water is too deep to cross on foot";
        break;
      case "Boat":
        Debug.Log("Boat Collider");
        inBoat = true;
        beStill = true;
        myText.text = "Push off?";
        break;
      default: 
        Debug.Log("No collider identity");      
        break;
    }
  }
 
  public void OnCollisionExit(Collision other){
    myText.text = ""; 
    if(other.gameObject.tag == "Boat"){
      inBoat = false;
    }
      
  }
  void Update(){
    if(Input.GetKeyUp("w")){
      OnWolfToBoat(); 
    }
    else if (Input.GetKeyUp("c")) {
      OnChxToBoat();
    }
    else if (Input.GetKeyUp("g")) {
      OnCabToBoat();
    }

  } 
 
  public void ChxToBoat(){

  }

  public void WolfToBoat(){

  }

  public void CabToBoat(){

  } 

  //Boat launch script subscribes to this event. 
  public void BoatLaunch(){
    OnBoatLaunch();
    Debug.Log("Boat launch");
  }
}
