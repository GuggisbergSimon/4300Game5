using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPath : MonoBehaviour
{
    [SerializeField] private GameObject pathPrefab;

    private Transform playerPosition;
    private float spawnZ = 0.0f;
    private float pathLength = 13.0f;
    private int pathOnScreen = 6;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < pathOnScreen; i++)
        {
            SpawnPath();
        }
    }
    void Update()
    {
        if (playerPosition.position.z>(spawnZ-pathOnScreen*pathLength))
        {
            SpawnPath();
        }
    }

    private void SpawnPath()
    {
        GameObject path;
        path=Instantiate(pathPrefab)as GameObject;
        path.transform.SetParent(transform);
        path.transform.position = Vector3.forward * spawnZ;
        spawnZ += pathLength;
    }
}
