using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;

    // refernce to check the card condition
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;

    public TextMesh score = null;
    private int _score = 0;

    [SerializeField] private MemoryCard originalCard = null;
    [SerializeField] private Sprite[] images = null;

    // getter with condtion for the memory card script
    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    // take the object form the MemoryCard script into the refernce objects
    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }

    // suffle the array for more randomnes
    private int[] suffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;

        }
        return newArray;
    }

    // wait for message from the UIButton script that activite the restart.
    public void Restart(){
        SceneManager.LoadScene("Scene");
    }


    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = suffleArray(numbers);

        // 2x for loops to achive the grid of cards
        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {   
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = j * gridCols + i; // another small suffel based on the loop
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j) + startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }


    }

    // corutine that responseble on the score, unreveal cards that dont mach and making the object refrence null again for another use.
    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            score.text = "Score:" + _score;
        }
        else
        {
            yield return new WaitForSeconds(.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

}
