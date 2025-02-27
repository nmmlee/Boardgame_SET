using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public string color { get; private set; }
    public string shape { get; private set; }
    public string fillType { get; private set; }
    public int number { get; private set; }
    public Sprite image { get; private set; }

    public Card(string color, string shape, string fillType, int number, Sprite image)
    {
        this.color = color;
        this.shape = shape;
        this.fillType = fillType;
        this.number = number;
        this.image = image;
    }

    public void Initialize(string color, string shape, string fillType, int number, Sprite image)
    {
        this.color = color;
        this.shape = shape;
        this.fillType = fillType;
        this.number = number;
        this.image = image;
    }

}
