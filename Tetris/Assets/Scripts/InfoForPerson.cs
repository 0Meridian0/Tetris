using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InfoForPerson : MonoBehaviour
{
    [SerializeField]
    Text scoreText; 

    [SerializeField]
    Text speedText; 
    
    [SerializeField]
    Text linesText; 

    //Метод обновления игровых очков на UI
    public void UpdateTextScore(int score)
    {
        scoreText.text = "Score: \n" + score.ToString();
    }

    //Метод обновления скорости на UI
    public void UpdateTextSpeed(int speed)
    {
        speedText.text = "Speed: \n" + speed.ToString();
    }

    //Метод обновления разрушенных линий на UI 
    public void UpdateTextLines(int line)
    {
        linesText.text = "Lines: \n" + line.ToString();
    }

}
