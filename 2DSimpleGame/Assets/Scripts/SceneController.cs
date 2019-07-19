using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private int score;
    public int gridRow = 2;
    public int gridColumn = 4;
    public float offsetX = 2.0f;
    public float offsetY = 2.5f;

    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;



    void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int x = 0; x < gridColumn; x++)
        {
            for (int y = 0; y < gridRow; y++)
            {
                MemoryCard card;
                if (x == 0 && y == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = y * gridColumn + x;
                int id = numbers[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * x) +  startPos.x;
                float posY = -(offsetY * y) + startPos.y;


                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
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

    private MemoryCard _firstRevealad;
    private MemoryCard _secondRevealad;

    public bool canReveal
    {
        get { return _secondRevealad == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (_firstRevealad == null)
        {
            _firstRevealad = card;
        }
        else
        {
            _secondRevealad = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealad.id == _secondRevealad.id)
        {
            score++;
            scoreLabel.text = "Score: " + score;

        }
        else
        {
            yield return new WaitForSeconds(.5f);
            _firstRevealad.Unreveal();
            _secondRevealad.Unreveal();
        }
        _firstRevealad = null;
        _secondRevealad = null;
    }

    public void Restart()
    {
        
        Application.LoadLevel("Scene");
    }
   
}
