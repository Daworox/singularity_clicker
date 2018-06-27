
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour {

	public delegate void UpdateBalance();
	public static event UpdateBalance OnUpdateBalance;
	public static gamemanager instance;
	double CurrentBalance;

    void Awake () {
		if (instance == null) {
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		CurrentBalance = 1.00;
		//UIManager.instance.UpdateUI();
		if(OnUpdateBalance != null) {
			OnUpdateBalance();
		}	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToBalance (double amt) {
		CurrentBalance += amt;
		if(OnUpdateBalance != null) {
			OnUpdateBalance ();
		}	
		//UIManager.instance.UpdateUI();
	}

	public bool CanBuy (double AmtToSpend) {
		if (CurrentBalance >= AmtToSpend) return true;
		else return false;
	}

	public double GetCurrentBalance () {
		return CurrentBalance;
	}

}
