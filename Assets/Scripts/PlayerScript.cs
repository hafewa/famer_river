using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
  
  public Text myText;
  private bool inBoat;
  private bool beStill;
  public delegate void BoatEvent();
  public static event BoatEvent OnBoatLaunch;

  public void OnCollisionEnter(Collision other){
    switch(other.gameObject.tag){
      case "Wolf": 
        Debug.Log("Wolf Collider");
        myText.text = "Pick up the wolf";
        break;
      case "Chicken":
        Debug.Log("Chicken Collider");        
        myText.text = "Pick up the chicken";
        break;
      case "Cabbage":
        Debug.Log("Cabbage Collider");
        myText.text = "Pick up the cabbage"; 
        break;
      case "River_Wall":
        Debug.Log("River Collider");
        if(!inBoat)
          myText.text = "The water is too deep to cross.";
        break;
      case "Boat":
        Debug.Log("Boat Collider");
        beStill = true;
        myText.text = "Launch boat?";
        break;
      default: 
        Debug.Log("No collider identity");      
        break;
    }
  }
 
  public void OnCollisionExit(Collision other){
       myText.text = " "; 
    }
 
  //Boat launch script subscribes to this event. 
  public void BoatLaunch(){
    OnBoatLaunch();
    Debug.Log("Boat launch");
  }


}
