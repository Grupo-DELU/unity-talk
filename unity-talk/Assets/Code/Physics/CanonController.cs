using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CanonController : MonoBehaviour {

	public GameObject projectile;
	public Rigidbody canon;

	public Transform spawnPoint;
	public float recoilForce;

	public GameObject Vfx;

	public bool launch;
	// Use this for initialization
	void Start () {
		
	}
	
	IEnumerator DestroyProjectile(GameObject projectile, float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(projectile);
	}


	// Update is called once per frame
	void Update () {
		if (launch)
		{
			launch = false;
			canon.AddForce(canon.transform.forward * recoilForce);
			GameObject p = Instantiate(projectile, spawnPoint.position, canon.rotation);
			GameObject v = Instantiate(Vfx, spawnPoint.position, canon.rotation);
			p.GetComponent<Rigidbody>().AddForce(-p.transform.forward * recoilForce/100);
			StartCoroutine(DestroyProjectile(p, 30));
			StartCoroutine(DestroyProjectile(v, 2.5f));

			
		}
	}
}
