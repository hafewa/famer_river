using UnityEngine;
using System.Collections;

public class Animal : MonoBehaviour {

  public enum MyState{EastBank, WestBank, InBoat, Dead};
  public MyState my_state;

	// Use this for initialization
  void Start () {
	 
   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public virtual void GetMyState(){
    my_state = MyState.EastBank;
    Debug.Log("my state = "+my_state);
  }

}
