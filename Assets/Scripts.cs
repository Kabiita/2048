using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Scripts : MonoBehaviour
{
    public static Scripts instance;
    public static int ticker; // to check how many cells have received our action
    [SerializeField] GameObject fillPrefab;  //To hold prefab object
    [SerializeField] Cell2048[] everycellsIn2048;

    public static Action<string> slide;
    public int myScore;

    [SerializeField] Text ScoreDisplay;

    int isGameOver;
     [SerializeField] GameObject gameOverPanel;

    public Color[] fillColors;
    [SerializeField] int winningScore;
    [SerializeField] GameObject winningPanel;
    bool hasWon;

    // Touch
    Touch initTouch;
    bool swiped;
    public float speed = 4.0f;
    private Vector2 direction = Vector2.zero;


    //Audio
    public AudioSource effect;

    private void OnEnable()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCellsFill();
        StartCellsFill();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //input check for function
        {
            CellsFill();
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            ticker = 0;
            isGameOver = 0;
            slide("w");
            effect.Play();
        }
         if(Input.GetKeyDown(KeyCode.D))
        { 
            ticker = 0;
            isGameOver = 0;
            slide("d");
            effect.Play();
        }
         if(Input.GetKeyDown(KeyCode.S))
        {
            ticker = 0;
            isGameOver = 0;
            slide("s");
            effect.Play();
        }
         if(Input.GetKeyDown(KeyCode.A))
        {
            ticker = 0;
            isGameOver = 0;
            slide("a");
            effect.Play();
        }
    }

    public void CellsFill()
    {
        bool isFull = true;
        for(int i = 0; i< everycellsIn2048.Length; i++)
        {
            if(everycellsIn2048[i].fill == null)
            {
                    isFull = false;
            }           
        }
        if(isFull == true)
        {
            return;
        }


        int whichCells = UnityEngine.Random.Range(0, everycellsIn2048.Length);
        if(everycellsIn2048[whichCells].transform.childCount != 0)
        {
            Debug.Log(everycellsIn2048[whichCells].name + " is already filled");
            CellsFill();
            return;

        }
        float chance = UnityEngine.Random.Range(0f, 1f);
        
        Debug.Log(chance);
        if(chance < .2f)
        {
            return;
        }  
        else if(chance < .8f) 
        {
            GameObject tempFill = Instantiate(fillPrefab, everycellsIn2048[whichCells].transform);           
            Debug.Log(2);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();  // when fill object is instantiated, pass that value into the FillValueUpdate() ofnewly instantiated  fillprefab
            // To get cell script from current cell we are instantiating
            everycellsIn2048[whichCells].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
        }
         else  
        {
            GameObject tempFill = Instantiate(fillPrefab, everycellsIn2048[whichCells].transform);
            Debug.Log(4);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();
            everycellsIn2048[whichCells].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(4);
        }
    }
    public void StartCellsFill()
    {
        int whichCells = UnityEngine.Random.Range(0, everycellsIn2048.Length);
        if(everycellsIn2048[whichCells].transform.childCount != 0)
        {
            Debug.Log(everycellsIn2048[whichCells].name + " is already filled");
            CellsFill();
            return;

        }
        
            GameObject tempFill = Instantiate(fillPrefab, everycellsIn2048[whichCells].transform);           
            Debug.Log(2);
            Fill2048 tempFillComp = tempFill.GetComponent<Fill2048>();  // when fill object is instantiated, pass that value into the FillValueUpdate() ofnewly instantiated  fillprefab
            // To get cell script from current cell we are instantiating
            everycellsIn2048[whichCells].GetComponent<Cell2048>().fill = tempFillComp;
            tempFillComp.FillValueUpdate(2);
    }
    public void ScoreUpdate(int scoreIn)
    {
        myScore += scoreIn;
        ScoreDisplay.text = myScore.ToString();

    }
   
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void WinningCheck(int highestFill)
    {
        if(hasWon)  
            return;
        if(highestFill == winningScore)
        {
            winningPanel.SetActive(true);  //enable winning panel
            hasWon = true;
        }
    }
    public void KeepPlaying()
    {
        winningPanel.SetActive(false);
    }

    public void MuteSound(){
        effect.mute = true;;
    }

    void FixedUpdate()
    {
        touch();
    }
    void touch()
    {
        foreach (Touch touched in Input.touches)
        {
            if (touched.phase == TouchPhase.Began)
            {
                initTouch = touched;
            }
            else if (touched.phase == TouchPhase.Moved && !swiped)
            {
                float xmoved = initTouch.position.x - touched.position.x;
                float ymoved = initTouch.position.y - touched.position.y;
                float distance = Mathf.Sqrt((xmoved * xmoved) + (ymoved * ymoved));
                bool swipedLeft = Mathf.Abs(xmoved) > Mathf.Abs(ymoved);

                if (distance > 40f)
                {
                    if (swipedLeft==true && xmoved > 0)
                    {
                        direction = Vector2.left;
                        Debug.Log("Swipped Left");
                        ticker = 0;
                        isGameOver = 0;
                        slide("a");  
                        effect.Play();
                    }
                    else if (swipedLeft==true && xmoved < 0)
                    {
                        direction = Vector2.right;
                        Debug.Log("Swipped Right");
                        ticker = 0;
                        isGameOver = 0;
                        slide("d");
                        effect.Play();
                    }
                    else if (swipedLeft == false && ymoved > 0)
                    {
                        direction = Vector2.down;
                        Debug.Log("Swipped Down");
                        ticker = 0;
                        isGameOver = 0;
                        slide("s");
                        effect.Play();
                    }
                    else if (swipedLeft == false && ymoved < 0)
                    {
                        direction = Vector2.up;
                        Debug.Log("Swipped Up");
                        ticker = 0;
                        isGameOver = 0;
                        slide("w");
                        effect.Play();
                    }
                    direction.Normalize();
                    swiped = true;
                }
            }
            else if (touched.phase == TouchPhase.Ended)
            {
                initTouch = new Touch();
                swiped = false;
            }
        }
    }
     public void GameOverCheck()
    {
        isGameOver++;
        if(isGameOver >= 16)
        {
            gameOverPanel.SetActive(true);
        }
    }
}

