
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStore : MonoBehaviour {

	public Text StoreCountText;
	public Text BuyButtonText;
	public Button BuyButton;
	public Slider ProgressSlider;
	public store Store;

	// Use this for initialization
	void OnEnable() {
		gamemanager.OnUpdateBalance += UpdateUI;
	}

	void OnDisable () {
		gamemanager.OnUpdateBalance -= UpdateUI;
	}
	void Awake () {
		Store = transform.GetComponent<store>();
	}
	void Start () {
		StoreCountText.text = Store.StoreCount.ToString();
		UpdateBuyButtonText();
	}
	
	// Update is called once per frame
	void Update () {
		ProgressSlider.value = Store.GetCurrentTimer() / Store.GetBaseStoreTimer();
	}
    
	void UpdateBuyButtonText () {
		BuyButtonText.text = "Buy $" + Store.GetNextStoreCost().ToString("n2");
	}

	public void UpdateUI () {
		CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
		// unlock a new store and enable interaction when having enough money
		if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.GetNextStoreCost())) {
			cg.interactable = false;
			cg.alpha = 0;
		} else {
		if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.GetNextStoreCost())) {
			Store.StoreUnlocked = true;
            cg.interactable = true;
			cg.alpha = 1;
		}

		// disable the Buy button when not having enough money
		if (gamemanager.instance.CanBuy(Store.GetNextStoreCost())) {
				BuyButton.interactable = true;
			} else {
				BuyButton.interactable = false;
		}

	  }

	 UpdateBuyButtonText();
	
    }

	public void BuyStoreOnClick () {
		if (gamemanager.instance.CanBuy(Store.GetNextStoreCost())) {
			Store.BuyStore();
			StoreCountText.text = Store.StoreCount.ToString();
			UpdateUI();
		}	
	}

	public void OnTimerClick () {
		Store.OnStartTimer();
	}

}