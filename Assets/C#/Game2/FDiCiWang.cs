using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDiCiWang : MonoBehaviour
{
    public DiCiWang DiCiWang ;
    public NumberDisplayGame1 NumberDisplayGame1;
    // Start is called before the first frame update
    void Start()
    {
        DiCiWang = FindObjectOfType<DiCiWang>();
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
            Whole.Game2 += 1;
            DiCiWang.SpawnPrefab();
            Destroy(gameObject);
        }
    }
}
