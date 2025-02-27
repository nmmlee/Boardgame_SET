using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public void MakeCards()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("card");
        for(int i = 0; i < sprites.Length; i++)
        {
            string fileName = sprites[i].name;
            cards.Add(CardFactory.CreateCardFromImageName(fileName, sprites[i]));
        }
    }
}
