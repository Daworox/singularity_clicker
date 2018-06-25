using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;
	public Text CurrentBalanceText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable() {
		gamemanager.OnUpdateBalance += UpdateUI;
	}

	void OnDisable() {
		gamemanager.OnUpdateBalance -= UpdateUI;
	}

//	void Awake () {
//		if (instance == null) {
//			instance = this;
//		}
//	}

	public void UpdateUI () {
		CurrentBalanceText.text = "$" + gamemanager.instance.GetCurrentBalance().ToString("n2");
	}

}
