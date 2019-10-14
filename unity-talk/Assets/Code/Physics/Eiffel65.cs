using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eiffel65 : MonoBehaviour {

	public Material newMaterial;

	public void OnCollisionEnter()
	{
		GetComponent<Renderer>().material = newMaterial;

	}
}
