using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    IObjectPool<Coin> pool;
    // string coinText;
    
    // void Awake() {
    //     coinText = GameObject.Find("totalCoin").GetComponent<TMPro.TextMeshPro>().text;
    // }

    public void SetPool(IObjectPool<Coin> pool)
    {
        this.pool = pool;
    }
    // 충돌 처리 안됨 -> 고쳐야 함
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("trigger detected...");
            pool.Get();
            pool.Release(this);
            GameMng.I.coinCount += 1;
        }
    }
}
