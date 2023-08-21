using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStand : MonoBehaviour
{
    [SerializeField] List<GameObject> product = new List<GameObject>();
    [SerializeField] Transform spawnPoint = null;
    [SerializeField] GameObject displayStand;

    BoxCollider boxCol;
    Vector3 spawnSizeX = Vector3.zero;
    Vector3 spawnSizeZ = Vector3.zero;

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        displayStand = this.gameObject;
        boxCol = displayStand.gameObject.GetComponent<BoxCollider>();

        // 앞 뒤는 같은 품목 BoxCollider에서 크기 가져오기
        // 옆은 다른 품목
        spawnSizeX.x = (boxCol.bounds.max.x - boxCol.bounds.min.x) / 4;
        spawnSizeZ.z = (boxCol.bounds.max.z - boxCol.bounds.min.z) / 4;
        
        // 앞쪽, 왼쪽부터 진열되도록 하기.
        spawnPoint.position = new Vector3(boxCol.bounds.center.x - boxCol.bounds.extents.x, boxCol.bounds.center.y + 1, boxCol.bounds.min.z);
        Debug.Log($"X : {spawnSizeX}, Z : {spawnSizeZ}, \nCenter: {boxCol.bounds.center}, Extents: {boxCol.bounds.extents}");

        GameObject cloneProduct = null;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                cloneProduct = Instantiate(product[i].gameObject, spawnPoint.position + (spawnSizeZ * j), Quaternion.identity) as GameObject;
                cloneProduct.name = product[i].name;
                if (spawnPoint.position.z > boxCol.bounds.max.z)
                {
                    return;
                }
                cloneProduct.transform.parent = this.transform;
            }
            spawnPoint.position += spawnSizeX;
        }
    }
}
