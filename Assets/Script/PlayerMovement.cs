using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour, IDdamageable
{
    [SerializeField] private int hp = 100;
    private int hpMaks;

    [Header("UI")]
    [SerializeField] private Slider barDarah; 

    public float kecepatan = 5f;
    public int skor = 0;

    public GameManager gameManager;
    private Vector2 arahGerak;

    private Animator anim;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        hpMaks = hp; 
        UpdateBarDarah();

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
        Debug.Log("Move : " + arahGerak); // buat ngecek input masuk
    }

    void Update()
    {
        Vector3 gerak = new Vector3(arahGerak.x, arahGerak.y, 0f);
        transform.position += gerak * kecepatan * Time.deltaTime;

        if (arahGerak.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (arahGerak.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        bool isMoving = arahGerak.sqrMagnitude > 0.01f;
        anim.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            anim.SetFloat("MoveX", arahGerak.x);
            anim.SetFloat("MoveY", arahGerak.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            skor++;
            Debug.Log("Player collected Coin! Skor = " + skor);
            Destroy(other.gameObject);

            if (gameManager == null)
            {
                gameManager = FindFirstObjectByType<GameManager>();
            }

            if (gameManager != null)
            {
                gameManager.KoinTerkumpul();
            }
            else
            {
                Debug.LogWarning("GameManager reference missing!");
            }
        }
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"HP Player : {hp}");

        UpdateBarDarah();

        if (hp <= 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }

    void UpdateBarDarah()
    {
        if (barDarah != null)
        {
            barDarah.value = (float)hp / hpMaks;
        }
    }
}