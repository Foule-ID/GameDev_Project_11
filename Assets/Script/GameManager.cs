using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int totalKoin;
    private int koinTerkumpul = 0;

    void Start()
    {
        totalKoin = GameObject.FindGameObjectsWithTag("coin").Length;
    }
    public void KoinTerkumpul()
    {
        koinTerkumpul++;
        if (koinTerkumpul >= totalKoin)
        {
            Debug.Log("Semua koin telah terkumpul!");
            Menang();
        }
    }

    // Update is called once per frame
    void Menang()
    {
        Debug.Log("WOI! Kamu Menang");

        GameObject[] musuh = GameObject.FindGameObjectsWithTag("Enemy");

    foreach (GameObject enemy in musuh)
    {
        Destroy(enemy);
    }
    }
}
