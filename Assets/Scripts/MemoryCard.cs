using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] public GameObject cardBack;

    // give access to the SceneController script (class)
    [SerializeField] private SceneController controller = null;

    // get a read-only of the id
    private int _id;
    public int id
    {
        get { return _id; }
    }

    // Give the card id and image of the memory card
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    // if the card back is active and the second card didnt reveal it self yet.
    void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal)
        {
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
}
