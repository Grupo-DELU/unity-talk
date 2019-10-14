using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CrustyCrabController : MonoBehaviour {

	public Transform rightDoor, leftDoor;
	public bool open = false;
	private bool lastOpen;
	// Use this for initialization
	void Start () {
		lastOpen = open;
		StartCoroutine(UpdateCO());
	}

	private IEnumerator Open(){
		rightDoor.DORotate(rightDoor.eulerAngles + Vector3.forward*-80, 1f).SetEase(Ease.InQuad);
		leftDoor.DORotate(leftDoor.eulerAngles + Vector3.forward*+80, 1f).SetEase(Ease.InQuad);
		yield return new WaitForSeconds(1.05f);
	}

	private IEnumerator Close(){
		rightDoor.DORotate(rightDoor.eulerAngles + Vector3.forward*80, 1f).SetEase(Ease.OutBack);
		leftDoor.DORotate(leftDoor.eulerAngles + Vector3.forward*-80, 1f).SetEase(Ease.OutBack);
		yield return new WaitForSeconds(1.05f);
	}
	
	// Update is called once per frame
	IEnumerator UpdateCO() {
		while (true){
		if (lastOpen != open){
			if (open){
				yield return Close();
			} else { 
				yield return Open();
			}
			lastOpen = open;
		}
		yield return new WaitForSeconds(.1f);
		}
	}
}
