using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public CardManager cardManager;
    public GameObject[] cardButtons;

    public Text remainedCards;
    public Text timeText;
    public Text hintText;

    private static System.Random rand = new System.Random();

    // ȭ�鿡 ������ 12���� ī�带 �ʱ�ȭ�ϴ� �Լ�. ��Ʈ���� �����
    [SerializeField]
    private List<Card> gameCards = new List<Card>();

    // ��Ʈ�� �� 3���� �̾Ƽ� ����� �� �� ����
    [SerializeField]
    private List<Card> nextCards = new List<Card>();

    // ���õ� ��ư�� ī�� ����Ʈ
    [SerializeField]
    private List<GameObject> selectedButtons = new List<GameObject>();
    [SerializeField]
    private List<Card> selectedCards = new List<Card>();

    private bool isAllColorMatch;
    private bool isAllShapeMatch;
    private bool isAllFillTypeMatch;
    private bool isAllNumberMatch;
    private bool isSet;

    public bool isGameStart;
    public bool isPause;

    private float sec;
    private int min;


    void Awake()
    {
        cardManager.MakeCards();
    }
    void Start()
    {
        // ó�� 12�� ����
        for (int i = 0; i < cardButtons.Length; i++)
        {
            int randIndex = rand.Next(cardManager.cards.Count);

            gameCards.Add(cardManager.cards[randIndex]);
            cardButtons[i].GetComponent<Image>().sprite = gameCards[i].image;
            cardButtons[i].GetComponent<Card>().Initialize(gameCards[i].color, gameCards[i].shape, gameCards[i].fillType, gameCards[i].number, gameCards[i].image);

            cardManager.cards.RemoveAt(randIndex);
        }

        UpdateUI();
    }

    void Update()
    {
        if(isGameStart && !isPause)
        {
            sec += Time.deltaTime;
            if (sec >= 60f)
            {
                min += 1;
                sec = 0;
            }
        }

        timeText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
    }

    // ī�� �������� �� �̺�Ʈ ���
    void OnEnable()
    {
        ButtonClick.onButtonClicked += HandleCardClicked;
    }

    // ī�� ���� �� 3���� �Ǿ��� �� ��Ʈ���� �Ǻ�
    void HandleCardClicked(GameObject clickedButton)
    {
        if (selectedButtons.Contains(clickedButton))
        {
            Debug.Log("�̹� ���õ� ��ư�Դϴ�.");
            return;
        }

        selectedButtons.Add(clickedButton);

        if (selectedButtons.Count == 3)
        {
            CompareSelectedCards();
            ResetBorders();
            selectedButtons.Clear();
            selectedCards.Clear();
        }
    }

    // ��Ʈ �Ǻ� �Լ�
    void CompareSelectedCards()
    {
        selectedCards.Clear();

        foreach (GameObject button in selectedButtons)
        {
            Card card = button.GetComponent<Card>();
            selectedCards.Add(card);
        }

        isAllColorMatch = (selectedCards[0].color == selectedCards[1].color && selectedCards[1].color == selectedCards[2].color && selectedCards[0].color == selectedCards[2].color) ||
                          (selectedCards[0].color != selectedCards[1].color && selectedCards[1].color != selectedCards[2].color && selectedCards[0].color != selectedCards[2].color);

        isAllShapeMatch = (selectedCards[0].shape == selectedCards[1].shape && selectedCards[1].shape == selectedCards[2].shape && selectedCards[0].shape == selectedCards[2].shape) ||
                          (selectedCards[0].shape != selectedCards[1].shape && selectedCards[1].shape != selectedCards[2].shape && selectedCards[0].shape != selectedCards[2].shape);

        isAllFillTypeMatch = (selectedCards[0].fillType == selectedCards[1].fillType && selectedCards[1].fillType == selectedCards[2].fillType && selectedCards[0].fillType == selectedCards[2].fillType) ||
                          (selectedCards[0].fillType != selectedCards[1].fillType && selectedCards[1].fillType != selectedCards[2].fillType && selectedCards[0].fillType != selectedCards[2].fillType);

        isAllNumberMatch = (selectedCards[0].number == selectedCards[1].number && selectedCards[1].number == selectedCards[2].number && selectedCards[0].number == selectedCards[2].number) ||
                          (selectedCards[0].number != selectedCards[1].number && selectedCards[1].number != selectedCards[2].number && selectedCards[0].number != selectedCards[2].number);

        Debug.Log($"Card1: {selectedCards[0].color}, {selectedCards[0].shape}, {selectedCards[0].fillType}, {selectedCards[0].number}");
        Debug.Log($"Card2: {selectedCards[1].color}, {selectedCards[1].shape}, {selectedCards[1].fillType}, {selectedCards[1].number}");
        Debug.Log($"Card3: {selectedCards[2].color}, {selectedCards[2].shape}, {selectedCards[2].fillType}, {selectedCards[2].number}");

        if (isAllColorMatch && isAllShapeMatch && isAllFillTypeMatch && isAllNumberMatch)
        {
            isSet = true;
            Debug.Log("��Ʈ�Դϴ�.");
            NextCards();
        }
        else
        {
            isSet = false;
            Debug.Log("��Ʈ�� �ƴմϴ�.");
        }
    }

    // ��Ʈ�� ī�� 3���� ���ְ�, ������ 3���� �̾ƿ�.
    void NextCards()
    {
        for (int i = 0; i < selectedButtons.Count; i++)
        {
            if(cardManager.cards.Count > 0)
            {
                int randIndex = rand.Next(cardManager.cards.Count);
                nextCards.Add(cardManager.cards[randIndex]);

                selectedButtons[i].GetComponent<Image>().sprite = nextCards[i].image;
                selectedButtons[i].GetComponent<Card>().Initialize(nextCards[i].color, nextCards[i].shape, nextCards[i].fillType, nextCards[i].number, nextCards[i].image);

                cardManager.cards.RemoveAt(randIndex);
            }
            else
            {
                selectedButtons[i].GetComponent<Image>().sprite = null;
                Card cardComponent = selectedButtons[i].GetComponent<Card>();
                Destroy(cardComponent);
                selectedButtons[i].GetComponent<Button>().interactable = false;
            }
            
        }
        UpdateUI();
        nextCards.Clear();

        CheckGameEnd();
    }

    void CheckGameEnd()
    {
        bool hasSet = CheckSet();

        if (gameCards.Count == 0 && cardManager.cards.Count == 0)
        {
            Debug.Log("���� ����! ��� ī�尡 ���Ǿ����ϴ�.");
            GameOver();
        }
        else if (!hasSet && cardManager.cards.Count == 0)
        {
            Debug.Log("���� ����! �� �̻� ��Ʈ�� ���� �� �����ϴ�.");
            GameOver();
        }
    }

    void GameOver()
    {
        isGameStart = false;
        hintText.text = "���� ����!";
    }

    void UpdateUI()
    {
        remainedCards.text = cardManager.cards.Count.ToString();
    }

    void ResetBorders()
    {
        foreach (GameObject button in selectedButtons)
        {
            Transform border = button.transform.Find("border");
            if (border != null)
            {
                border.gameObject.SetActive(false);
            }
        }
    }

    public void Hint()
    {
        if (CheckSet())
        {
            Debug.Log("��Ʈ�� �ֽ��ϴ�.");
        }
        else
        {
            Debug.Log("��Ʈ�� �����ϴ�.");
            hintText.text = "��Ʈ�� �����ϴ�.";
            CheckGameEnd();
        }
    }

    bool CheckSet()
    {
        gameCards.Clear();

        foreach(GameObject button in cardButtons)
        {
            Card card = button.GetComponent<Card>();
            if(card != null)
            {
                gameCards.Add(card);
            }
        }

        for(int i = 0; i < gameCards.Count - 2; i++)
        {
            for(int j = i + 1; j < gameCards.Count - 1; j++)
            {
                for(int k = j + 1; k < gameCards.Count; k++)
                {
                    if(IsSet(gameCards[i], gameCards[j], gameCards[k]))
                    {
                        hintText.text = (i + 1) + ", " + (j + 1) + ", " + (k + 1) + "�� ��Ʈ�Դϴ�.";
                        return true;
                    }
                }
            }
        }
        return false;
    }

    bool IsSet(Card card1, Card card2, Card card3)
    {
        bool isColorMatch = (card1.color == card2.color && card2.color == card3.color && card1.color == card3.color) ||
                    (card1.color != card2.color && card2.color != card3.color && card1.color != card3.color);

        bool isShapeMatch = (card1.shape == card2.shape && card2.shape == card3.shape && card1.shape == card3.shape) ||
                            (card1.shape != card2.shape && card2.shape != card3.shape && card1.shape != card3.shape);

        bool isFillTypeMatch = (card1.fillType == card2.fillType && card2.fillType == card3.fillType && card1.fillType == card3.fillType) ||
                               (card1.fillType != card2.fillType && card2.fillType != card3.fillType && card1.fillType != card3.fillType);

        bool isNumberMatch = (card1.number == card2.number && card2.number == card3.number && card1.number == card3.number) ||
                             (card1.number != card2.number && card2.number != card3.number && card1.number != card3.number);

        return isColorMatch && isShapeMatch && isFillTypeMatch && isNumberMatch;


        Debug.Log($"Card1: {card1.color}, {card1.shape}, {card1.fillType}, {card1.number}");
        Debug.Log($"Card2: {card2.color}, {card2.shape}, {card2.fillType}, {card2.number}");
        Debug.Log($"Card3: {card3.color}, {card3.shape}, {card3.fillType}, {card3.number}");

    }

    public void Shuffle()
    {
        if(cardManager.cards.Count != 0)
        {
            GameObject button = cardButtons[0];
            Card card = button.GetComponent<Card>();

            cardManager.cards.Add(new Card(card.color, card.shape, card.fillType, card.number, card.image));

            int randIndex = rand.Next(cardManager.cards.Count);
            Card newCard = cardManager.cards[randIndex];

            button.GetComponent<Image>().sprite = newCard.image;
            button.GetComponent<Card>().Initialize(newCard.color, newCard.shape, newCard.fillType, newCard.number, newCard.image);

            cardManager.cards.RemoveAt(randIndex);

            UpdateUI();
        }
        else
        {
            Debug.Log("���� ���� ī�尡 �����ϴ�.");
        }
    }

}
