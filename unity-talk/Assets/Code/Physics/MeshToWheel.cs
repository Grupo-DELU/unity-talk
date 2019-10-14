using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshToWheel : MonoBehaviour {

	public WheelCollider wheelC;

	private Vector3 wheelCCenter;
	private RaycastHit hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		wheelCCenter = wheelC.transform.TransformPoint(wheelC.center);

		//Debug.DrawRay(wheelCCenter, -wheelC.transform.up, Color.red);
		if (Physics.Raycast(wheelCCenter, -wheelC.transform.up, out hit, wheelC.suspensionDistance + wheelC.radius))
		{
			transform.position = hit.point + (wheelC.transform.up * wheelC.radius);
		}
		else
		{
			transform.position = wheelCCenter - (wheelC.transform.up * wheelC.suspensionDistance);
		}
	}
}
