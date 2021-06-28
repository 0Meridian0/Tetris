using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{       
    [SerializeField]
    GameObject[] massTetraminoes;
    
    [SerializeField]
    GameObject nextTetramino;
    [SerializeField]
    GameObject curTetramino;
    
    bool firstsTime = true;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewTetramino();
    }

    public void SpawnNewTetramino()
    {
        if(firstsTime)
        {
            curTetramino = Instantiate(massTetraminoes[Random.Range(0, massTetraminoes.Length)], transform.position, Quaternion.identity);
            firstsTime = false;
            nextTetramino = Instantiate(massTetraminoes[Random.Range(0, massTetraminoes.Length)], nextTetramino.transform.position, Quaternion.identity);
            nextTetramino.GetComponent<TetraminoScript>().enabled = false;
        }
        else
        {
            curTetramino = Instantiate(nextTetramino, transform.position, Quaternion.identity);
            curTetramino.GetComponent<TetraminoScript>().enabled = true;
            Destroy(nextTetramino);
            nextTetramino = Instantiate(massTetraminoes[Random.Range(0, massTetraminoes.Length)], nextTetramino.transform.position, Quaternion.identity);
            nextTetramino.GetComponent<TetraminoScript>().enabled = false;
        }        
    }
    public void Reset()
    {
        firstsTime = true;
        Destroy(nextTetramino);
        Destroy(curTetramino);
    }
}
