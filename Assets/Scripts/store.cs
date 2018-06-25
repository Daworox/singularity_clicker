using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class store : MonoBehaviour {

	// Variables
    float CurrentTimer;
	bool StartTimer;
	private double NextStoreCost;

	//public vars - define gameplay 
	public int StoreCount;
	public double BaseStoreCost;
	public double BaseStoreProfit;
	public float BaseStoreTimer;
	public Text StoreCountText;
	public Text BuyButtonText;
	public Button BuyButton;
	public Slider ProgressSlider;
	public bool StoreUnlocked;
	public bool ManagerUnlocked;
	public float StoreMultiplier = 1.10f;
	public int StoreTimerDivision = 5;

	// Variable initialization
	void Start () {
		StartTimer = false;
		StoreCountText.text = StoreCount.ToString();
		NextStoreCost = BaseStoreCost;
		BuyButtonText.text = "Buy $" + NextStoreCost.ToString("n2");
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
		ProgressSlider.value = CurrentTimer / BaseStoreTimer;
		CheckStoreBuy ();
	}

	public void CheckStoreBuy () {
		CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
		// unlock a new store and enable interaction when having enough money
		if (!StoreUnlocked && !gamemanager.instance.CanBuy(NextStoreCost)) {
			cg.interactable = false;
			cg.alpha = 0;
		} else {
			StoreUnlocked = true;
            cg.interactable = true;
			cg.alpha = 1;
		}

		// disable the Buy button when not having enough money
		if (gamemanager.instance.CanBuy(NextStoreCost)) {
				BuyButton.interactable = true;
			} else {
				BuyButton.interactable = false;
		}
	}

	// BuyStoreButton logic
	public void BuyStoreOnClick () {
		if (gamemanager.instance.CanBuy(NextStoreCost)) {
			StoreCount += 1;
			//Debug.Log("Purchased store " + StoreCount);
			StoreCountText.text = StoreCount.ToString();
			gamemanager.instance.AddToBalance(-NextStoreCost);
			NextStoreCost = (BaseStoreCost * Mathf.Pow(StoreMultiplier,StoreCount));
			BuyButtonText.text = "Buy $" + NextStoreCost.ToString("n2");

			// every x stores shorten the time of BaseStoreTimer
			if(StoreCount % StoreTimerDivision == 0) {;
			BaseStoreTimer = BaseStoreTimer/2;
			}
		}	
	}

	// StoreClickButton logic
	public void StoreOnClick () {
		//Debug.Log("Clicked the store...");
		if (StartTimer == false && StoreCount > 0) {
			StartTimer = true;
		}
	}

}
