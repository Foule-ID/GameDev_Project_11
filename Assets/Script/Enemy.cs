using UnityEngine;

public class Enemy : MonoBehaviour, IDdamageable
{
    [SerializeField] private int hp = 100;
    [SerializeField] private int damage = 10;
    
    public float ms = 2f;
    protected Transform player;
    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found in the scene.");
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        Kejar();
    }
    
    public void Kejar ()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(transform.position, player.position, ms * Time.deltaTime);
    }

    public virtual void Serang()
    {
        Debug.Log("monster menyerang");
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} Kena Damage {jumlah}, sisa Hp: {hp}");
        if (hp <= 0)
        {
            Mati();
        }
    }

    private void Mati()
    {
        Debug.Log($"{gameObject.name} Mati");
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
{
    IDdamageable player = other.GetComponent<IDdamageable>();

    if (player != null)
    {
        player.KenaDamage(damage);
    }
}

}
