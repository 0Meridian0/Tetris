using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] massTetraminoes; // Массив всех тетрамино

    [SerializeField]
    GameObject nextTetramino; // Тетрамино, которое будет использоваться следующим

    [SerializeField]
    GameObject curTetramino; // Тетрамино, выходящее на игровое поле
    bool firstsTime = true;

    void Start()
    {
        SpawnNewTetramino();
    }

    // Спавн тетрамино на игровое поле
    public void SpawnNewTetramino()
    {
        if (firstsTime)
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

    //Метод перезапуска спавна
    public void Reset()
    {
        firstsTime = true;
        Destroy(nextTetramino);
        Destroy(curTetramino);
    }
}
