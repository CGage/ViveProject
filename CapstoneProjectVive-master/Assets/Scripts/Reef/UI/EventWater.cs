using UnityEngine;
using UnityEngine.EventSystems;

public class EventWater : EventTrigger {



	public override void OnPointerClick( PointerEventData data )
	{
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		ReefTagger.hasChosen = true;
		camera.GetComponent<ReefTagger> ().DestroyMenu ();
		camera.GetComponent<ReefTagger> ().MaterialType (1);
	}
		
	public override void OnPointerEnter( PointerEventData data )
	{
	//	Debug.Log ("OnPointerEnter called");
		//GameObject water = GameObject.FindGameObjectWithTag ("Water");
		Raycast water = GetComponent <Raycast> ();
		water.GetComponent <Raycast> ().RayCastEnter (1);
	}

	public override void OnPointerExit( PointerEventData data )
	{
		Raycast water = GetComponent <Raycast> ();
		water.GetComponent <Raycast> ().RayCastExit ();
	}
}