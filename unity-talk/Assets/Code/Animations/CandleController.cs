using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CandleController : MonoBehaviour {
	public enum state
	{
		On, Off
	}
	//public Light light;
	public Material lightMaterial, darkMaterial;
	public state s ;
	public Animator[] flames;
	public ParticleSystem[] smokes;
	private float[] intensities = {1,0.7f,0.9f,0.7f,0.5f};
	// Use this for initialization
	void Start () {
		//var x = light.intensity;
		/*  
		for (int i = 0; i < intensities.Length; i++){
			intensities[i] *= x;
		} 
		*/

		if (s == state.Off){
			TurnOff();
		}
		else {
			//StartCoroutine(UpdateCo());
		}
	}
	
	void OnDisable() {
		TurnOff();
	}

	void OnEnable() {
		if (s == state.Off){
			TurnOn();
		}
	}

	void TurnOn(){
		if (s == state.Off){
			//light.DOKill();
			//light.DOIntensity(intensities[0],.6f);
			for(int i = 0; i < smokes.Length; i++){
				smokes[i].Play();
			}
			foreach(var p in flames){
				p.SetTrigger("On");
			}
			//StartCoroutine(UpdateCo());
			s = state.On;
			GetComponent<MeshRenderer>().material = lightMaterial;
		}
	}
	void TurnOff(){
		if (s == state.On){
			StopAllCoroutines();
			//StopCoroutine(UpdateCo());
			//light.DOIntensity(0,.3f);
			for(int i = 0; i < smokes.Length; i++){
				smokes[i].Stop();
			}
			foreach(var p in flames){
				p.SetTrigger("Off");
			}
			s = state.Off;
		}
		GetComponent<MeshRenderer>().material = darkMaterial;
	}

	// Update is called once per frame
	IEnumerator UpdateCo () {
		int x = 0;
		//yield return new WaitWhile(()=>DOTween.IsTweening(light));
		while (true){
			x = (x+1) % intensities.Length;
			//light.DOIntensity(intensities[x], .1f);
			yield return new WaitForSeconds(.2f);
		}
	}
}
