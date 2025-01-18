using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public SpriteRenderer sword;
    public List<Sprite> swordSprites;

    public float jumpForce = 10f; 
    public float rotationSpeed = 360f;
    public float moveSpeed = 5f; 

    private Rigidbody2D rb;
    private bool isFlipped = false;

    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();

        
    }

    private void SetSword(int index)
    {
        if(index >=0 && index < swordSprites.Count)
        {
            sword.sprite = swordSprites[index];
        }
    }

    void Update()
    {
        if (gameManager != null && gameManager.isSwordActive)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (rb.velocity.y <= 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            RotateSword();
            HandleMovement();
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }


        int selectedSwordIndex = PlayerPrefs.GetInt("SelectedSword", 0);
        SetSword(selectedSwordIndex);
    }

    void RotateSword()
    {
        float direction = isFlipped ? -1f : 1f;
        transform.Rotate(0, 0, rotationSpeed * direction * Time.deltaTime);
    }

    void HandleMovement()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector2(2.5f, transform.position.y); 
            if (isFlipped) FlipSword(); 
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector2(-2.5f, transform.position.y); 
            if (!isFlipped) FlipSword(); 
        }
    }

    void FlipSword()
    {
        isFlipped = !isFlipped; 
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z); 
    }
}
