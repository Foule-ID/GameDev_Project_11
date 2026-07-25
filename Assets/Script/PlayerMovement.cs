using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDdamageable
{
    [SerializeField] private int hp = 100;

    public float kecepatan = 5f;
    public int skor = 0;
    
    public GameManager gameManager;
    private Vector2 arahGerak;

    // Dipanggil otomatis oleh Player Input (Behavior = Send Messages)
    void OnMove(InputValue value)
    {
        arahGerak = value.Get<Vector2>();
        Debug.Log("Move : " + arahGerak); // buat ngecek input masuk
    }

    void Update()
    {
        Vector3 gerak = new Vector3(arahGerak.x, arahGerak.y, 0f);
        transform.position += gerak * kecepatan * Time.deltaTime;
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

        if (hp <= 0)
        {
            Debug.Log("Game Over");
            Destroy(gameObject);
        }
    }
}