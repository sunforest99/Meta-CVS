using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStand : MonoBehaviour
{
    [SerializeField] List<GameObject> product = new List<GameObject>();
    [SerializeField] Transform spawnPoint = null;
    [SerializeField] GameObject displayStand;

    BoxCollider boxcol;
    Vector3 spawnSizeX = new Vector3(0, 0, 0);
    Vector3 spawnSizeZ = new Vector3(0, 0, 0);

    private void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        displayStand = this.gameObject;
        boxcol = displayStand.gameObject.GetComponent<BoxCollider>();

        // 앞 뒤는 같은 품목 BoxCollider에서 크기 가져오기
        // 옆은 다른 품목
        spawnSizeX.x = (boxcol.bounds.max.x - boxcol.bounds.min.x) / 4;
        spawnSizeZ.z = (boxcol.bounds.max.z - boxcol.bounds.min.z) / 4;
        // 앞쪽, 왼쪽부터 진열되도록 하기.
        spawnPoint.position = new Vector3(boxcol.bounds.center.x - boxcol.bounds.extents.x, boxcol.bounds.center.y, boxcol.bounds.min.z);
        Debug.Log($"X : {spawnSizeX}, Z : {spawnSizeZ}, \nCenter: {boxcol.bounds.center}, Extents: {boxcol.bounds.extents}");

        GameObject cloneProduct = null;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                cloneProduct = Instantiate(product[i].gameObject, spawnPoint.position + (spawnSizeZ*j), Quaternion.identity) as GameObject;
                if (spawnPoint.position.z > boxcol.bounds.max.z)
                {
                    return;
                }
                cloneProduct.transform.parent = this.transform;
            }
            spawnPoint.position += spawnSizeX;
        }
    }
}
