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

    public void UpdateTextScore(int score)
    {
        scoreText.text = "Score: \n" + score.ToString();
    }

    public void UpdateTextSpeed(int speed)
    {
        speedText.text = "Speed: \n" + speed.ToString();
    }

    public void UpdateTextLines(int line)
    {
        linesText.text = "Lines: \n" + line.ToString();
    }

}
