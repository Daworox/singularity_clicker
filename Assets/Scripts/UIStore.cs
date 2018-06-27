
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

	void Awake () {
		Store = transform.GetComponent<store>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ProgressSlider.value = Store.CurrentTimer / Store.BaseStoreTimer;
		UpdateUI ();
	}
    
	void UpdateBuyButtonText () {
		BuyButtonText.text = "Buy $" + Store.NextStoreCost.ToString("n2");
	}

	public void UpdateUI () {
		CanvasGroup cg = this.transform.GetComponent<CanvasGroup>();
		// unlock a new store and enable interaction when having enough money
		if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.NextStoreCost)) {
			cg.interactable = false;
			cg.alpha = 0;
		} else {
		if (!Store.StoreUnlocked && !gamemanager.instance.CanBuy(Store.NextStoreCost)) {
			Store.StoreUnlocked = true;
            cg.interactable = true;
			cg.alpha = 1;
		}

		// disable the Buy button when not having enough money
		if (gamemanager.instance.CanBuy(Store.NextStoreCost)) {
				BuyButton.interactable = true;
			} else {
				BuyButton.interactable = false;
		}

	  }

	 UpdateBuyButtonText();
	 StoreCountText.text = Store.StoreCount.ToString();

    }

	public void BuyStoreOnClick () {
		if (gamemanager.instance.CanBuy(Store.NextStoreCost)) {
			Store.BuyStore();
		}	
	}

	public void OnTimerClick () {
		Store.OnStartTimer();
	}

}