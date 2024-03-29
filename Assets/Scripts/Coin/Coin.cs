using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    IObjectPool<Coin> pool;

    public void SetPool(IObjectPool<Coin> pool)
    {
        this.pool = pool;
    }

    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("trigger detected...");
            pool.Get();
            pool.Release(this);
            GameMng.I.coinCount += 1;
        }
    }
}
