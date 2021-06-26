using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] MassTetraminoes;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewTetramino();
    }

    public void SpawnNewTetramino()
    {
        Instantiate(MassTetraminoes[Random.Range(0, MassTetraminoes.Length)], transform.position, Quaternion.identity);
    }
}
