using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour {

	private Animator _animator;
	private Rigidbody[] _rg;
	private Collider[] _c;

	public bool KILL = false;
	public float projectileForceMultiplier;

	void Start()
	{
		_rg = GetComponentsInChildren<Rigidbody>();
		_c = GetComponentsInChildren<Collider>();
		_animator = GetComponent<Animator>();
	} 
	public void activateRagdoll(Vector3 force)
	{
		_animator.enabled = false;
		
		foreach(var c in _c)
		{
			c.enabled = true;
		}

		foreach (var rg in _rg)
		{
			rg.isKinematic = false;
			rg.useGravity = true;
			rg.AddForce(force);
		}
	}

	void Update()
	{
		if (KILL)
		{
			activateRagdoll(-transform.forward );
		}
	}

	void OnCollisionEnter(Collision other)
	{		

		if (other.collider.GetComponent<Rigidbody>() && other.relativeVelocity.magnitude > 10f)
		{
			GetComponent<Collider>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;
			activateRagdoll(other.relativeVelocity * projectileForceMultiplier);
		}	

		if (other.collider.tag == "Terrain" && other.relativeVelocity.magnitude > 10f)
		{
			GetComponent<Collider>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = true;
			activateRagdoll(Vector3.zero);
		}	

	}
}
