using System.Collections.Generic;
using UnityEngine;

public class TrunkGrowth : MonoBehaviour
{
    public GameObject trunkSegment;
    public Transform sword;
    public Camera mainCamera;

    private float nextSpawnY;
    private float segmentHeight = 2f;
    private float spawnSpeed = 1f; 

    private GameManager gameManager;

    private List<GameObject> spawnedSegments = new List<GameObject>();

    void Start()
    {
        nextSpawnY = 7f;
        gameManager = FindObjectOfType<GameManager>(); 
    }

    void Update()
    {
        if (gameManager != null && gameManager.isSwordActive)
        {
            GrowTrunk();
        }
    }

    void GrowTrunk()
    {

        if (sword.position.y+6 > nextSpawnY - segmentHeight)
        {
            while (sword.position.y+6 > nextSpawnY - segmentHeight)
            {
                Vector3 spawnPosition = new Vector3(0, nextSpawnY, -0.5f);
                GameObject newSegment = Instantiate(trunkSegment, spawnPosition, Quaternion.identity);
                spawnedSegments.Add(newSegment);
                nextSpawnY += segmentHeight;
            }
        }

        float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;
        for (int i = spawnedSegments.Count - 1; i >= 0; i--)
        {
            if (spawnedSegments[i].transform.position.y + segmentHeight < cameraBottomY)
            {
                Destroy(spawnedSegments[i]);
                spawnedSegments.RemoveAt(i);
            }
        }
    }
}
