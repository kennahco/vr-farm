using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sets all given colliders to ignore collisions with each other
public class CollisionIgnorer : MonoBehaviour {

	[SerializeField]
	List<Collider> collidersToIgnoreEachother = new List<Collider>();

	public GameObject ParentToIsolateAllChildren;

	// Use this for initialization
	void Start () {
		if(ParentToIsolateAllChildren != null)
		{
			collidersToIgnoreEachother.AddRange(ParentToIsolateAllChildren.GetComponentsInChildren<Collider>());
		}

		foreach (var col1 in collidersToIgnoreEachother)
		{
			foreach (var col2 in collidersToIgnoreEachother)
			{
				if (col1 != col2)
				{
					Physics.IgnoreCollision(col1,col2,true);
				}
			}
		}
	}
}
