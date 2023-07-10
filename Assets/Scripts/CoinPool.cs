using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinPool : MonoBehaviour
{
    [SerializeField] Coin coin = null;

    public IObjectPool<Coin> pool;
    [SerializeField] BoxCollider rangeCollider;

    void Start()
    {
        // Action<T>  bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000);

        pool = new ObjectPool<Coin>(OnCreate, OnObject, OnRelease, OnDestroyCoin, maxSize: 30);

        for (int i = 0; i < 30; i++)
        {
            pool.Get();
        }
    }

    public Coin OnCreate()
    {
        Coin obj = Instantiate(coin);
        obj.SetPool(pool);
        InitPosition(obj);
        return obj;
    }
    void InitPosition(Coin obj)
    {
        obj.transform.position = Return_RandomPosition();
        obj.transform.rotation = Quaternion.identity;
        obj.transform.parent = this.transform;
    }

    public void OnObject(Coin obj)
    {
        InitPosition(obj);
        obj.gameObject.SetActive(true);
    }
    public void OnRelease(Coin obj)
    {
        obj.gameObject.SetActive(false);
    }
    public void OnDestroyCoin(Coin obj)
    {
        Destroy(obj.gameObject);
    }
    /// <summary>
    ///  코인 마을 내에 무작위 소환하기 위해 범위 측정
    /// </summary>
    Vector3 Return_RandomPosition()
    {
        Vector3 originPos = rangeCollider.transform.position;
        float rangeX = rangeCollider.bounds.size.x;
        float rangeZ = rangeCollider.bounds.size.z;

        rangeX = Random.Range((rangeX / 2) * -1, rangeX / 2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);
        Vector3 randomPos = new Vector3(rangeX, 2f, rangeZ);

        Vector3 respawnPos = originPos + randomPos;
        return respawnPos;
    }
    // <summary>
    // 랜덤한 위치로 코인 소환하는 함수. CoinTrigger 함수를 갖게 함.
    // </summary>
    IEnumerator RandomRespawnCoin()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject instantCoin = Instantiate(coin.gameObject, Return_RandomPosition(), Quaternion.identity);
        }
    }
}
