using UnityEngine;
using UnityEngine.EventSystems;

public class EventCoral: EventTrigger {



	public override void OnPointerClick( PointerEventData data )
	{
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		ReefTagger.hasChosen = true;
		camera.GetComponent<ReefTagger> ().DestroyMenu ();
		camera.GetComponent<ReefTagger> ().MaterialType (4);
	}

	public override void OnPointerEnter( PointerEventData data )
	{
	//	Debug.Log ("OnPointerEnter called");
		//GameObject water = GameObject.FindGameObjectWithTag ("Water");
		Raycast rock = GetComponent <Raycast> ();
		rock.GetComponent <Raycast> ().RayCastEnter (4);


	}

	public override void OnPointerExit( PointerEventData data )
	{
		Raycast rock = GetComponent <Raycast> ();
		rock.GetComponent <Raycast> ().RayCastExit ();
	}
}