
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour {

	// Variables
    float CurrentTimer;
	bool StartTimer;
	double NextStoreCost;

	//public vars - define gameplay 
	public int StoreCount;
	public double BaseStoreCost;
	public double BaseStoreProfit;
	float BaseStoreTimer;

	public bool StoreUnlocked;
	public bool ManagerUnlocked;
	public float StoreMultiplier = 1.10f;
	public int StoreTimerDivision = 5;

	// Variable initialization
	void Start () {
		StartTimer = false;
		NextStoreCost = BaseStoreCost;
	}

	// Update is called once per frame
	void Update () {
		if (StartTimer) {
			CurrentTimer += Time.deltaTime;
			if (CurrentTimer > BaseStoreTimer) {
				//Debug.Log("Timer has ended, reset!");
				if (!ManagerUnlocked) StartTimer = false;
				CurrentTimer = 0;
				gamemanager.instance.AddToBalance(BaseStoreProfit * StoreCount);
			}
		}
	}

	// BuyStoreButton logic
	public void BuyStore () {
			StoreCount += 1;
			//Debug.Log("Purchased store " + StoreCount);
			double Amt = -NextStoreCost;
			NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier,StoreCount));
			gamemanager.instance.AddToBalance(Amt);

			// every x stores shorten the time of BaseStoreTimer
			if(StoreCount % StoreTimerDivision == 0) {;
			BaseStoreTimer = BaseStoreTimer/2;
			}	
	}

	// StoreClickButton logic
	public void OnStartTimer () {
		//Debug.Log("Clicked the store...");
		if (StartTimer == false && StoreCount > 0) {
			StartTimer = true;
		}
	}

	public float GetCurrentTimer () {
		return CurrentTimer;
	}

	public float GetBaseStoreTimer () {
		return BaseStoreTimer;
	}

	public double GetNextStoreCost () {
		return NextStoreCost;
	}

}
