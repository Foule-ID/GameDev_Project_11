using UnityEngine;
 
// Data stats zombie yang disimpen terpisah dari scene.
// Bisa dipake bareng-bareng oleh banyak Enemy sekaligus.
[CreateAssetMenu(fileName = "ZombieConfig", menuName = "PvZ/Zombie Config")]
public class ZombieConfig : ScriptableObject
{
    public int hp = 100;
    public int damage = 10;
    public float moveSpeed = 2f;
    public float jarakDeteksi = 6f;
    public float jarakSerang = 1.2f;
}
 