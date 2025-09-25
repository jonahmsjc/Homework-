using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectableManager : MonoBehaviour
{
    [Header("Prefab & Player")]
    public GameObject collectablePrefab;    // assign in Inspector
    public Transform player;                // assign the Player

    [Header("Collectable Settings")]
    public int collectablesOnScreen = 5;    // number visible ahead
    public float minZInterval = 7f;         // random spacing range
    public float maxZInterval = 15f;

    [Header("X Axis Settings")]
    public float minX = -3f;                // minimum X spawn
    public float maxX = 3f;                 // maximum X spawn

    [Header("Y Axis Settings")]
    public bool randomY = false;            // toggle for random heights
    public float fixedY = 1f;               // default Y if not random
    public float minY = 1f;                 // min random Y
    public float maxY = 3f;                 // max random Y

    public int totalCollected = 0;
    public int goal = 10;

    public TMP_Text collectableText;
    public TMP_Text winText;

    private List<GameObject> activeCollectables = new List<GameObject>();
    private float spawnZ = 20f;             // first spawn position
    private int collectableIndex = 0;

    private bool hasWon = false;
    void Start()
    {
        UpdateUI();
        winText.gameObject.SetActive(false);

        for (int i = 0; i < collectablesOnScreen; i++)
        {
            SpawnCollectable();
        }
    }

    void Update()
    {
        // Check if first collectable is behind the player → MISS
        if (activeCollectables.Count > 0)
        {
            GameObject first = activeCollectables[0];
            if (player.position.z > first.transform.position.z + 5f)
            {
                Debug.Log("MISS No. " + collectableIndex);
                RemoveCollectable(first);
            }
        }
    }

    void SpawnCollectable()
    {
        float spacing = Random.Range(minZInterval, maxZInterval);

        // Randomize X instead of using fixed lanes
        float x = Random.Range(minX, maxX);
        float y = randomY ? Random.Range(minY, maxY) : fixedY;

        Vector3 spawnPos = new Vector3(x, y, spawnZ);
        GameObject obj = Instantiate(collectablePrefab, spawnPos, Quaternion.identity);
        activeCollectables.Add(obj);

        spawnZ += spacing;
    }

    public void Collect(GameObject collectable)
    {
        if (hasWon) return;
        Debug.Log("HIT No. " + collectableIndex);
        RemoveCollectable(collectable);
        totalCollected++;
        UpdateUI();
        if (totalCollected >= goal)
        {
            WinGame();
        }
    }

    void RemoveCollectable(GameObject obj)
    {
        activeCollectables.Remove(obj);
        Destroy(obj);
        SpawnCollectable();
        collectableIndex++;
    }
    private void UpdateUI()
    {
        collectableText.text = totalCollected.ToString();
    }
    private void WinGame()
    {
        hasWon = true;

        // Show "You win!"
        winText.gameObject.SetActive(true);
        ClearAllCollectables();
    }
    public void ClearAllCollectables()
    {
        // Loop through all active collectables and destroy them
        foreach (GameObject obj in activeCollectables)
        {
            if (obj != null)
                Destroy(obj);
        }
    }
}
