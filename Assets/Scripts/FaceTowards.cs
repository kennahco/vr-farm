using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotate attached object to always face towards a direction, position, object, or the player
public class FaceTowards : MonoBehaviour {

	public enum Type
    {
		Direction,
		Position,
		Object,
		Player
	}
	
	[Tooltip("Face a direction, position, object, or the player. your choice!")]
	public Type type;

	[Tooltip("Direction to face, if you picked direction")]
	public Vector3 direction;

    [Tooltip("instead of using direction, just save the starting rotation of the object")]
	public bool useStartRotation;

	//gets from one of above depening on selectiong
	private Quaternion internalDirection;

	
	[Tooltip("position to face, if you picked position")]
	public Vector3 position;

	
	[Tooltip("GameObject to face, if you picked GameObject")]
	public GameObject objectToFace;


    [Tooltip("Offset To Apply")]
    public Vector3 Offset;

    [Tooltip("stops this script from changing the x axis")]
	public bool ignoreX;

	[Tooltip("stops this script from changing the y axis")]
	public bool ignoreY;

	[Tooltip("stops this script from changing the z axis")]
	public bool ignoreZ;
	

	// Use this for initialization
	void Start () {
		if(useStartRotation)
		{
			//save start rotation for ease of use
			internalDirection = transform.rotation;
		}
		else
		{
			//or use given
			internalDirection = Quaternion.Euler(direction);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Save Current Rotation
		Vector3 oldRot = transform.eulerAngles;
		switch (type)
		{
			case Type.Direction:
				//Set to given rotation
				transform.rotation = internalDirection;
			break;
			case Type.Position:
				//look at given position
				transform.LookAt(position);
			break;
			case Type.Object:
				//look at given object
				transform.LookAt(objectToFace.transform);
			break;
			case Type.Player:
				//look at player containter
				transform.LookAt(Camera.main.transform);
			break;
		}

        transform.Rotate(Offset);

		//Reset X
		if(ignoreX)
		{
			transform.eulerAngles = new Vector3(oldRot.x,transform.eulerAngles.y,transform.eulerAngles.z);
		}
		//Reset Y
		if(ignoreY)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,oldRot.y,transform.eulerAngles.z);
		}
		//Reset Z
		if(ignoreZ)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,oldRot.z);
		}
	}
}
