using System.Collections;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public GameObject cloudPrefab; 
    public Sprite[] cloudSprites; 
    public Transform sword; 
    public Camera mainCamera;
    public float spawnDistanceAboveSword = 5f; 
    public float spawnInterval = 3f; 
    public float cloudSpeedMin = 1f; 
    public float cloudSpeedMax = 3f; 

    private float cameraWidth;


    public void StartSpawningClouds()
    {
        cameraWidth = 2f * mainCamera.orthographicSize * mainCamera.aspect;
        StartCoroutine(SpawnClouds());
    }

    IEnumerator SpawnClouds()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnCloudAboveSword();
        }
    }

    void SpawnCloudAboveSword()
    {
        Sprite cloudSprite = cloudSprites[Random.Range(0, cloudSprites.Length)];

        Vector3 spawnPosition = new Vector3(sword.position.x, sword.position.y + spawnDistanceAboveSword, sword.position.z + 1f);

        GameObject newCloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

        SpriteRenderer cloudRenderer = newCloud.GetComponent<SpriteRenderer>();
        cloudRenderer.sprite = cloudSprite;

        float cloudSpeed = Random.Range(cloudSpeedMin, cloudSpeedMax);
        int direction = Random.Range(0, 2) == 0 ? -1 : 1; 

        StartCoroutine(MoveCloud(newCloud, cloudSpeed, direction));
    }

    IEnumerator MoveCloud(GameObject cloud, float speed, int direction)
    {
        while (true)
        {
            cloud.transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

            if (cloud.transform.position.x < -cameraWidth / 2 || cloud.transform.position.x > cameraWidth / 2)
            {
                Destroy(cloud);
                break;
            }

            yield return null;
        }
    }
}
