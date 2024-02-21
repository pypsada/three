using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDao : MonoBehaviour
{
    public Dao Dao;
    public NumberDisplayGame1 NumberDisplayGame1;
    // Start is called before the first frame update
    void Start()
    {
        Dao = FindObjectOfType<Dao>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            NumberDisplayGame1.Number += 1;
            Whole.Game1 += 1;
            Dao.SpawnPrefab();
            Destroy(gameObject);
        }
    }
}
