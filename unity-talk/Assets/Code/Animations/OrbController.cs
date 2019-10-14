using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OrbController : MonoBehaviour {
[Range(0,1)]
	public float stabilization = 0; 
	public MeshRenderer core, effect01, effect02; 
	public float waveHeight, waveFrequency, levitation;
	public Color targetColor1, targetColor2;
	public bool shrink;
	private float lastStabilization;
	private Color originalColor1, originalColor2;
	private float time;
	private Transform t;
	private Vector3 originalPos;
	private Material outlineMaterial, coreMaterial, effect01Material, effect02Material; 
	private float offsetY = 0;
	private ParticleSystem ps;
	private ParticleSystem.EmissionModule em;
	private ParticleSystem.VelocityOverLifetimeModule vol;
	

	void Awake () {
		StartCoroutine(StartCo());
	}
	private IEnumerator StartCo() {
		yield return new WaitForEndOfFrame();

		lastStabilization = -1;
		t = transform;
		originalPos = t.position;
		coreMaterial = core.material;
		effect01Material = effect01.material;
		effect02Material = effect02.material;
		outlineMaterial = GetComponent<MeshRenderer>().material;
		originalColor1 = effect01Material.GetColor("_Color");
		originalColor2 = effect02Material.GetColor("_Color");
		ps = GetComponent<ParticleSystem>();
		em = ps.emission;
		vol = ps.velocityOverLifetime;
		StartCoroutine(UpdateCo());
	}
	private IEnumerator UpdateCo () {

		while (!shrink){	
			offsetY =  (waveHeight * Mathf.Sin(Time.time*(waveFrequency * stabilization)) + levitation * stabilization);	
			
			if (stabilization != lastStabilization){
				
				outlineMaterial.SetColor("_Color", Color.Lerp(originalColor2, targetColor2, stabilization));

				effect01Material.SetFloat("_Transparency", stabilization < .666f ? stabilization*1.5f : 1 );
				effect01Material.SetFloat("_SpeedX", 10*stabilization);
				effect01Material.SetFloat("_SpeedY", 10*stabilization);
				effect01Material.SetColor("_Color", Color.Lerp(originalColor1, targetColor1, stabilization));
			
				effect02Material.SetFloat("_Transparency", stabilization < .666f ? stabilization*3 : 2 );
				effect02Material.SetFloat("_SpeedX", 20*stabilization);
				effect02Material.SetFloat("_SpeedY", 12*stabilization);
				effect02Material.SetColor("_Color", Color.Lerp(originalColor2, targetColor2, stabilization));

				coreMaterial.SetFloat("_Transition", stabilization);
				
				if (stabilization > .333){
					em.rateOverTime = stabilization * 10;
					vol.orbitalZ = stabilization < .02f ? stabilization  + .02f : 1;
					vol.speedModifier = stabilization;
					ps.Play();
				} else{
					ps.Stop();
				}


			}
			lastStabilization = stabilization;
			t.position = originalPos + Vector3.up * offsetY;
			yield return new WaitForEndOfFrame();
		}

		t.DOScale(Vector3.zero,1).SetEase(Ease.InQuad);
		effect01Material.DOFloat(0, "_Scale", 1).SetEase(Ease.InQuad);
		effect02Material.DOFloat(0, "_Scale", 1).SetEase(Ease.InQuad);

		//yield return new WaitForSeconds(1f);

	}
	
}
