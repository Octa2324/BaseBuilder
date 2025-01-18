using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LeafsController : MonoBehaviour
{
    public GameObject goodLeafPrefab;
    public GameObject badLeafPrefab;
    public Transform sword; 
    public float spawnInterval = 2f; 

    private float nextSpawnY;
    private float segmentHeight = 2f;
    private GameManager gameManager;

    private int goodLeafCount = 0; 
    private List<GameObject> spawnedLeaves = new List<GameObject>();

    public Camera mainCamera;

    public GameObject badLeafHitEffect;
    public float restartDelay = 2f;

    SoundEffectManager soundEffectManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        nextSpawnY = sword.position.y + 6f;

        goodLeafCount = PlayerPrefs.GetInt("GoodLeafCount", 0);

        soundEffectManager = SoundEffectManager.Instance;
    }

    void Update()
    {
        if (gameManager != null && gameManager.isSwordActive)
        {
            SpawnLeavesContinuously();
            CheckLeafHits();
        }
    }

    void SpawnLeavesContinuously()
    {
        if (sword.position.y + 6f > nextSpawnY - segmentHeight)
        {
            while (sword.position.y + 6f > nextSpawnY - segmentHeight)
            {
                float spawnX = Random.value < 0.5f ? -1.5f : 1.5f;

                Vector3 spawnPosition = new Vector3(spawnX, nextSpawnY, -0.5f);
                GameObject leafPrefab = Random.value < 0.5f ? goodLeafPrefab : badLeafPrefab;
                GameObject newLeaf = Instantiate(leafPrefab, spawnPosition, Quaternion.identity);

                if (spawnX == -1.5f)
                {
                    Vector3 scale = newLeaf.transform.localScale;
                    newLeaf.transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z);
                }

                spawnedLeaves.Add(newLeaf);

                nextSpawnY += segmentHeight;
            }

            float cameraBottomY = mainCamera.transform.position.y - mainCamera.orthographicSize;
            for (int i = spawnedLeaves.Count - 1; i >= 0; i--)
            {
                if (spawnedLeaves[i].transform.position.y + segmentHeight < cameraBottomY)
                {
                    Destroy(spawnedLeaves[i]);
                    spawnedLeaves.RemoveAt(i);
                }
            }
        }
    }

    void CheckLeafHits()
    {
        for (int i = spawnedLeaves.Count - 1; i >= 0; i--)
        {
            if (spawnedLeaves[i] == null) continue;

            Collider2D swordCollider = sword.GetComponent<Collider2D>();
            Collider2D leafCollider = spawnedLeaves[i].GetComponent<Collider2D>();

            if (swordCollider.IsTouching(leafCollider))
            {
                if (spawnedLeaves[i].CompareTag("GoodLeaf"))
                {
                    soundEffectManager.Hit();
                    goodLeafCount++;
                    Debug.Log("Good Leaves Hit: " + goodLeafCount);

                    PlayerPrefs.SetInt("GoodLeafCount", goodLeafCount);
                    PlayerPrefs.Save();

                }
                else if (spawnedLeaves[i].CompareTag("BadLeaf"))
                {

                    Debug.Log("Bad Leaves Hit" );
                    soundEffectManager.Explode();

                    PlayBadLeafHitEffect();

                    sword.gameObject.SetActive(false);

                    Invoke("RestartScene", restartDelay);
                }

                Destroy(spawnedLeaves[i]);
                spawnedLeaves.RemoveAt(i);
            }
        }
    }

    void PlayBadLeafHitEffect()
    {
        Instantiate(badLeafHitEffect, sword.position, Quaternion.identity);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    public int GetGoodLeafCount()
    {
        return goodLeafCount;
    }

}
