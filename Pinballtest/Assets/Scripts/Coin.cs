using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    [SerializeField] GameObject coinParticlesPrefab;
	void OnTriggerEnter2D(Collider2D other)
    {
        CoinCounter.sharedInstance.DeleteCoin();
        Instantiate(coinParticlesPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void Update()
    {
        transform.rotation = Camera.main.gameObject.transform.rotation;
    }
}
