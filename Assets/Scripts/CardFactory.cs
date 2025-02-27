using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : MonoBehaviour
{
    public static Card CreateCardFromImageName(string fileName, Sprite image)
    {
        string nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
        string[] properties = nameWithoutExtension.Split('_');

        string color = properties[0];
        string shape = properties[1];
        string fillType = properties[2];
        int number = int.Parse(properties[3]);

        return new Card(color, shape, fillType, number, image);
    }
}
