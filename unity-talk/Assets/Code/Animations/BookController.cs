using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BookController : MonoBehaviour {

	public Material[] posiblePages;
	public Transform page;
	public GameObject frontPage, backPage;
	
	[Range(-1,1)]
	public float force;
	public float maxPagePerSecond = 10;
	[Tooltip("True = Close, False = Open")]
	public bool isClose = true;
	private Renderer frontPageMR, backPageMR, movingPageMR;
	public Material[] frontPageML, backPageML, movingPageML; 

	void Start(){
		page.gameObject.SetActive(false);
		
		frontPageMR = frontPage.GetComponent<Renderer>();
		backPageMR = backPage.GetComponent<Renderer>();
		movingPageMR = page.GetComponent<Renderer>();

		frontPageML = frontPageMR.materials;
		backPageML = backPageMR.materials;
		movingPageML = new Material[2];

		StartCoroutine(UpdateCO());
	}

	private IEnumerator UpdateCO () {
		while (true){
			if (force != 0 && !isClose){
				float time = (Mathf.Abs(force)*maxPagePerSecond);
				if (force > 0){

					yield return flipRight(time >= 1 ? 1/time : 1);
				}
				else{
					yield return flipLeft(time >= 1 ? 1/time : 1);
				}
			}
			yield return new WaitForEndOfFrame();
		}

	}

	private IEnumerator flipLeft(float speed){

		page.transform.localEulerAngles = Vector3.zero;
		page.localScale = Vector3.one;
	
		movingPageML[0] = backPageML[1];
		int rnd = Random.Range(0,posiblePages.Length);
		movingPageML[1] = posiblePages[rnd];
		backPageML[1] = posiblePages[rnd == 8 ? 9 : rnd == 9 ? 8 : Random.Range(0,posiblePages.Length)];	
		backPageMR.materials = backPageML;
		movingPageMR.materials = movingPageML;

		page.gameObject.SetActive(true);
		page.DOScaleZ(-1, speed);
		page.DOLocalRotate(Vector3.down*180,speed).SetEase(Ease.OutQuad);
		yield return new WaitForSeconds(speed*0.29289321881f);

		movingPageML = movingPageMR.materials;
		Material m1;
		m1 = movingPageML[0];
		movingPageML[0] = movingPageML[1];
		movingPageML[1] = m1; 
		movingPageMR.materials = movingPageML;

		yield return new WaitForSeconds(speed*0.70710678118f);

		frontPageML[1] = movingPageML[0];
		frontPageMR.materials = frontPageML;
		page.gameObject.SetActive(false);

	}

	private IEnumerator flipRight(float speed){
		page.transform.localEulerAngles = Vector3.up*180.5f;
		page.localScale = new Vector3(1,1,-1);
		page.gameObject.SetActive(true);

		movingPageML[0] = frontPageML[1];
		int rnd = Random.Range(0,posiblePages.Length);
		movingPageML[1] = posiblePages[rnd];
		frontPageML[1] = posiblePages[rnd == 9 ? 8 : rnd == 8 ? 9 : Random.Range(0,posiblePages.Length)];
		frontPageMR.materials = frontPageML;
		movingPageMR.materials = movingPageML;


		page.DOScaleZ(1, speed);
		page.DOLocalRotate(Vector3.zero,speed).SetEase(Ease.OutQuad);
		yield return new WaitForSeconds(speed*0.29289321881f);

		movingPageML = movingPageMR.materials;
		Material m1;
		m1 = movingPageML[0];
		movingPageML[0] = movingPageML[1];
		movingPageML[1] = m1; 
		movingPageMR.materials = movingPageML;

		yield return new WaitForSeconds(speed*0.70710678118f);

		backPageML[1] = movingPageML[0];
		backPageMR.materials = backPageML;
		page.gameObject.SetActive(false);
	}

	public void toggleState(){
		//When opening, choose random page to start with
		if (isClose){
			int rnd = Random.Range(0,posiblePages.Length);
			frontPageML[1] = posiblePages[rnd];
			rnd = Random.Range(0,posiblePages.Length);
			backPageML[1] = posiblePages[rnd];
			frontPageMR.materials = frontPageML;
			backPageMR.materials = backPageML;
		}

		isClose = !isClose;

	}
}
