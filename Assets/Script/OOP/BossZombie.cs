using UnityEngine;

// INHERITANCE: BossZombie mewarisi semua dari Enemy (HP, state machine, patrol, dll)
// POLYMORPHISM: Serang() dan Mati() di-override, jadi beda perilaku dari Enemy biasa
public class BossZombie : Enemy
{
    [Header("Serangan Spesial Boss")]
    [SerializeField] private int damageTambahan = 20;  

    public override void Serang()
    {
        base.Serang(); 

        if (player != null)
        {
            IDdamageable target = player.GetComponent<IDdamageable>();
            if (target != null)
            {
                target.KenaDamage(damageTambahan);
                Debug.Log($"BOSS ZOMBIE serangan spesial! Damage tambahan: {damageTambahan}");
            }
        }
    }

    protected override void Mati()
    {
        Debug.Log("BOSS ZOMBIE tumbang!! Level selesai?");
        base.Mati(); 
    }
}