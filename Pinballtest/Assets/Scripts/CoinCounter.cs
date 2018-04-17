using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour {

    public static CoinCounter sharedInstance;

    int coinCount;
    [SerializeField] TextMesh text;
	void Start () {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        coinCount = FindObjectsOfType<Coin>().Length;
        UpdateText();
	}

    void UpdateText()
    {
        text.text = "x " + coinCount.ToString();
    }
	
	void Update () {
		
	}

    public void DeleteCoin()
    {
        coinCount--;
        UpdateText();
    }
}