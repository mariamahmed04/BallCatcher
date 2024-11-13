using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private CollectibleSpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<CollectibleSpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pass the full collectible name to the spawner
            spawner.RegisterFirstCollectible(gameObject.name);

            spawner.DecrementCollectibleCount();
            Destroy(gameObject);
        }
    }
}

