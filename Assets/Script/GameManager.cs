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

    void OnGUI()
    {
        GUIStyle gaya = new GUIStyle();
        gaya.fontSize = 24;
        gaya.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 300, 40), $"Koin: {koinTerkumpul}/{totalKoin}", gaya);
    }

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