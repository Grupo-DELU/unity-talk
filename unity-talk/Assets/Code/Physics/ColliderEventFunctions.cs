using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEventFunctions : MonoBehaviour {


	private Animator _a;
	private int _allyNumber = 0;
	private int _enemyNumber = 0;

	public GameObject higligh;

	// Use this for initialization
	void Start () {
		_a = GetComponent<Animator>();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Enemy")
		{
			_a.SetTrigger("EnterEnemy");
			_enemyNumber++;
		}
		else if (other.tag == "Ally")
		{
			_a.SetTrigger("EnterAlly");
			_allyNumber++;
		}
		else
		{
			_a.SetTrigger("EnterUnkwon");
		}

	}

	void UpdatePresentAnimation()
	{
		if (_enemyNumber > 0)
		{
			_a.SetBool("EnemyPresent", true);	
		}
		else
		{
			_a.SetBool("EnemyPresent", false);
		}

		if (_allyNumber > 0)
		{
			_a.SetBool("AllyPresent", true);	
		}
		else
		{
			_a.SetBool("AllyPresent", false);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Enemy")
		{
			_a.SetTrigger("ExitEnemy");
			_enemyNumber--;
		}
		else if (other.tag == "Ally")
		{
			_a.SetTrigger("ExitAlly");
			_allyNumber--;
		}
		
		
		if (_enemyNumber == 0 && _allyNumber == 0)
		{
			higligh.SetActive(false);
		}

		UpdatePresentAnimation();
	}


	void OnTriggerStay(Collider other)
	{
		higligh.SetActive(true);
		UpdatePresentAnimation();
	}

}
