using UnityEngine;
using UnityEngine.Events;
using System;

public class Enemy : MonoBehaviour, IDdamageable
{
    [Header("Config (Opsional)")]
    [Tooltip("Kalau diisi, hp/damage/speed/jarak di bawah bakal ditimpa pakai nilai dari sini saat Start.")]
    [SerializeField] private ZombieConfig config;

    [Header("Pengaturan Enemy")]
    [SerializeField] private int hp = 100;
    [SerializeField] private int damage = 10;

    [Header("State Machine")]
    [SerializeField] private float jarakDeteksi = 6f;
    [SerializeField] private float jarakSerang = 1.2f;
    [SerializeField] private float jedaSerang = 1f;
    [SerializeField] private float moveSpeed = 2f;

    [Header("Idle Settings")]
    [SerializeField] private float waktuTungguIdle = 3f; // durasi diam sebelum mulai patroli

    [Header("Patrol Settings")]
    [SerializeField] private Vector2 batasPatrolMin = new Vector2(-4f, -2f); // pojok kiri-bawah area putih
    [SerializeField] private Vector2 batasPatrolMax = new Vector2(4f, 2f);   // pojok kanan-atas area putih
    [SerializeField] private float jarakSampaiTitik = 0.2f; // dianggap sampai kalau jaraknya segini

    [Header("Visual/Audio saat Mati")]
    [Tooltip("Sambungkan lewat Inspector: klik + lalu pilih GameObject & function-nya (mis. AudioSource.Play).")]
    [SerializeField] private UnityEvent onZombieMatiVisual;

    private Vector2 tujuanPatrol;
    private bool tujuanPatrolAdaIsi = false;
    private bool sedangIdle = false;
    private float waktuMulaiIdle;

    private StateZombie state = StateZombie.IDLE;
    private float waktuSerangTerakhir;

    protected Transform player;

       public static event Action<Enemy> OnZombieMati;

    protected virtual void Start()
    {
        if (config != null)
        {
            hp = config.hp;
            damage = config.damage;
            moveSpeed = config.moveSpeed;
            jarakDeteksi = config.jarakDeteksi;
            jarakSerang = config.jarakSerang;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan!");
        }
    }

    void Update()
    {
        PeriksaTransisi();

        switch (state)
        {
            case StateZombie.IDLE:
                PerilakuIdle();
                break;

            case StateZombie.PATROL:
                PerilakuPatrol();
                break;

            case StateZombie.CHASE:
                PerilakuChase();
                break;

            case StateZombie.ATTACK:
                PerilakuSerang();
                break;
        }
    }

    float JarakKePlayer()
    {
        if (player == null)
            return Mathf.Infinity;

        return Vector2.Distance(transform.position, player.position);
    }

    void PeriksaTransisi()
    {
        float jarak = JarakKePlayer();

        if (jarak <= jarakSerang)
        {
            state = StateZombie.ATTACK;
            sedangIdle = false;
        }
        else if (jarak <= jarakDeteksi)
        {
            state = StateZombie.CHASE;
            sedangIdle = false;
        }
        else if (state == StateZombie.PATROL)
        {
            
        }
        else if (!sedangIdle)
        {
          
            sedangIdle = true;
            waktuMulaiIdle = Time.time;
            state = StateZombie.IDLE;
        }
        else if (Time.time >= waktuMulaiIdle + waktuTungguIdle)
        {
           
            state = StateZombie.PATROL;
            sedangIdle = false;
        }
        else
        {
            state = StateZombie.IDLE;
        }
    }

    void Kejar()
    {
        if (player == null) return;

        transform.position = Vector2.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime
        );
    }

    void PerilakuIdle()
    {
        // Diam
    }

    void PerilakuPatrol()
    {
        if (!tujuanPatrolAdaIsi)
        {
            AmbilTujuanPatrolBaru();
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            tujuanPatrol,
            moveSpeed * Time.deltaTime
        );

        float jarakKeTitik = Vector2.Distance(transform.position, tujuanPatrol);

        if (jarakKeTitik <= jarakSampaiTitik)
        {
            // udah nyampe, cari titik random baru
            AmbilTujuanPatrolBaru();
        }
    }

    void AmbilTujuanPatrolBaru()
    {
        float x = UnityEngine.Random.Range(batasPatrolMin.x, batasPatrolMax.x);
        float y = UnityEngine.Random.Range(batasPatrolMin.y, batasPatrolMax.y);

        tujuanPatrol = new Vector2(x, y);
        tujuanPatrolAdaIsi = true;
    }

    void PerilakuChase()
    {
        Kejar();
    }

    void PerilakuSerang()
    {
        if (Time.time >= waktuSerangTerakhir + jedaSerang)
        {
            Serang();
            waktuSerangTerakhir = Time.time;
        }
    }

    public virtual void Serang()
    {
        Debug.Log("Monster menyerang");
    }

    public void KenaDamage(int jumlah)
    {
        hp -= jumlah;
        Debug.Log($"{gameObject.name} kena damage {jumlah}, sisa HP: {hp}");

        if (hp <= 0)
        {
            Mati();
        }
    }

    protected virtual void Mati()
    {
        Debug.Log($"{gameObject.name} mati");

        OnZombieMati?.Invoke(this);

        onZombieMatiVisual?.Invoke();

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDdamageable target = other.GetComponent<IDdamageable>();

        if (target != null)
        {
            target.KenaDamage(damage);
        }
    }
}