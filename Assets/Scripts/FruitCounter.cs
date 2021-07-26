using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitCounter : MonoBehaviour
{
    private int allFruit;
    [SerializeField] private Text fruitText;
    [SerializeField] private Text scoreText;
    public void AddFruit(int value)
    {
        allFruit += value;
        fruitText.text = "Your score: " + allFruit.ToString();
        scoreText.text = "Score: " + allFruit.ToString();
    }
}
