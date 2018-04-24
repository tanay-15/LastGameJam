using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
   [SerializeField] GameObject coinParticlesPrefab;

    float rot = 0;

	void OnTriggerEnter(Collider other)
 {
     CoinCounter.sharedInstance.DeleteCoin();
     Instantiate(coinParticlesPrefab, transform.position, transform.rotation);
     Destroy(gameObject);
 }

    void Update()
    {
        //transform.rotation = Camera.main.gameObject.transform.rotation;
        rot += Time.deltaTime * 90f;
        transform.localRotation = Quaternion.Euler(transform.rotation.x,rot, transform.rotation.z);
    }

}
