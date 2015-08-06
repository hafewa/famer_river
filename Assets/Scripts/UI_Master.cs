using UnityEngine;
using System.Collections;

public abstract class UI_Master : MonoBehaviour {


	public delegate void RestartEvent ();
	public static RestartEvent OnRestart;

	public virtual void StartTryAgain(){
		OnRestart ();
		Application.LoadLevel ("Main");
	}

	public virtual void ExitToDesktop(){
		Application.Quit ();
	}

}
