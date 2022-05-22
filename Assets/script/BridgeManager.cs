using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeManager : MonoBehaviour
{
    public GameObject BridgePrefab;
    private Transform playerTransform;
    private float spawnz = 0.0f;
    private float bridgeLenght = 60;
    private int NoOfBridges = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < NoOfBridges; i++)
        {
            spawnBridge();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position.z > (spawnz - NoOfBridges * bridgeLenght))
        {
            spawnBridge();
        }
    }

    void spawnBridge(int prefabIndex = -1)
    {
        GameObject bridge;
        bridge = Instantiate(BridgePrefab) as GameObject;
        bridge.transform.SetParent(transform);
        bridge.transform.position = Vector3.forward * spawnz;
        spawnz += bridgeLenght;
    }
}
