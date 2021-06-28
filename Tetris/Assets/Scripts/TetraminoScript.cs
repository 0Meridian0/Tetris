using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetraminoScript : MonoBehaviour
{
    [SerializeField]
    Vector3 tetraCenterRotation;

    [SerializeField]
    float fallTime = 0.8f;
    private float prevTime;
    private static int height = 20;
    private static int width = 10;
    private static Transform[,] table = new Transform[width, height];
    private static bool stopGame = false;
    private static int speed = 1;
    private static int score = 0;

    // Метод вывода информации для пользователя
    void Start()
    {
        FindObjectOfType<InfoForPerson>().UpdateTextScore(score);
        FindObjectOfType<InfoForPerson>().UpdateTextSpeed(speed);
    }

    // Основная логика игры(движение фигур, остановка при достижении "земли",
    // разрушение линии, сдвиг вышестоящих фигур вниз, спавн новой фигуры)
    void Update()
    {
        PersonControl();
        float fallenTetra;
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            fallenTetra = fallTime / (10 * speed);
        else
            fallenTetra = fallTime / speed;

        if (Time.time - prevTime > fallenTetra)
        {
            transform.position += new Vector3(0, -1, 0);

            if (!CanTetraMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToTable();
                if (!stopGame)
                {
                    CheckingFilledLine();
                    this.enabled = false;
                    FindObjectOfType<SpawnerScript>().SpawnNewTetramino();
                }
                else
                {
                    this.enabled = false;
                    RestartGame();
                }
            }
            prevTime = Time.time;
        }
    }

    // Метод спуска вышестоящих фигур после разрушения линии
    void PullDownLine(int i)
    {
        for (int h = i; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                if (table[w, h] != null)
                {
                    table[w, h - 1] = table[w, h];
                    table[w, h] = null;
                    table[w, h - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    // Метод разрушения заполненной линии
    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (table[j, i] != null)
                Destroy(table[j, i].gameObject);
            table[j, i] = null;
        }
    }

    // Метод поиска заполненых линий на игровом поле
    bool LineFilled(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (table[j, i] == null)
                return false;
        }
        return true;
    }

    // Метод разрушения заполненной линии
    void CheckingFilledLine()
    {
        int line = 0;
        for (int i = height - 1; i >= 0; i--)
        {
            if (LineFilled(i))
            {
                DeleteLine(i);
                PullDownLine(i);
                line++;
            }
        }
        if (line != 0)
        {
            FindObjectOfType<InfoForPerson>().UpdateTextLines(line);
            UpdateScore(line);
            CheckScoreSpeed();
        }

    }

    // Метод повышения скорости игры каждые 1000 очков
    void CheckScoreSpeed()
    {
        if (score % 1000 == 0 && speed < 10)
            speed++;
    }

    // Метод пересчета игровых очков в зависимости от разрушенных линий
    void UpdateScore(int line)
    {
        switch (line)
        {
            case 1:
                score += 100;
                break;
            case 2:
                score += 300;
                break;
            case 3:
                score += 700;
                break;
            case 4:
                score += 1500;
                break;
        }
        FindObjectOfType<InfoForPerson>().UpdateTextScore(score);
    }

    // Метод добавления тетрамино в общую таблицу
    void AddToTable()
    {
        foreach (Transform child in transform)
        {
            int X = Mathf.RoundToInt(child.transform.position.x);
            int Y = Mathf.RoundToInt(child.transform.position.y);

            if (table[X, Y] != null)
            {
                stopGame = true;
            }
            else
            {
                table[X, Y] = child;
            }
        }
    }

    // Метод управления тетрамино пользователем
    void PersonControl()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!CanTetraMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!CanTetraMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            transform.RotateAround(transform.TransformPoint(tetraCenterRotation), new Vector3(0, 0, 1), 90);
            if (!CanTetraMove())
                transform.RotateAround(transform.TransformPoint(tetraCenterRotation), new Vector3(0, 0, 1), -90);
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && speed < 10)
        {
            speed++;
            FindObjectOfType<InfoForPerson>().UpdateTextSpeed(speed);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus) && speed > 1)
        {
            speed--;
            FindObjectOfType<InfoForPerson>().UpdateTextSpeed(speed);
        }
    }

    // Метод проверки возможности движения тетрамино
    bool CanTetraMove()
    {
        foreach (Transform child in transform)
        {
            int X = Mathf.RoundToInt(child.transform.position.x);
            int Y = Mathf.RoundToInt(child.transform.position.y);

            if (X < 0 || X >= width || Y < 0 || Y >= height)
                return false;

            if (table[X, Y] != null)
                return false;
        }
        return true;
    }

    // Метод перезапуска игры
    public void RestartGame()
    {
        System.Threading.Thread.Sleep(1000);
        for (int i = 0; i < height; i++)
        {
            DeleteLine(i);
        }
        speed = 1;
        score = 0;
        stopGame = false;
        FindObjectOfType<InfoForPerson>().UpdateTextScore(score);
        FindObjectOfType<InfoForPerson>().UpdateTextSpeed(speed);
        FindObjectOfType<InfoForPerson>().UpdateTextLines(1);

        FindObjectOfType<SpawnerScript>().Reset();
        FindObjectOfType<SpawnerScript>().SpawnNewTetramino();
    }
}
