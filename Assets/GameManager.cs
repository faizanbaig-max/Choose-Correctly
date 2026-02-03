using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<GameObject> GridSizeSelection;
    public Sprite emptySprite;
    public GridPlacementController gridPlacementController;
    public int gridSize;
    private Tile firstTile,secondTile;
    Coroutine ShowEffectCoroutine;
    public GameObject mainmenu, gameplay, RoundCompleteScreen, RoundFailedScreen;
    public TextMeshProUGUI TotalTakesView;
    public TextMeshProUGUI TotalPointsView;
    private int totalRemainingTakes=0;
    private int totalPoints=0;
    private const string totalPointsStr="TotalPoints";


    #region Actions
    
    public delegate void IGameAction();
    public static event IGameAction OnGameStart;
    public static event IGameAction OnGameComplete;
    public static event IGameAction OnGameFailed;


    #endregion








    public void SelectGridOption(int index)
    {
        for (int i = 0; i < GridSizeSelection.Count; i++)
        {
            
            GridSizeSelection[i].SetActive(false);
        }
        GridSizeSelection[index].SetActive(true);

        switch (index)
        {
            case 0:
                gridSize = 2;
                
                break;
            case 1:
                gridSize = 4;
                
                break;
            case 2:
                gridSize = 6;
                
                break;

            default:
                break;
        }
    }





    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);

        }
        UpdateTotalPoints();
    }

    private void OnEnable()
    {
        OnGameStart += InitializeRound;
        OnGameComplete += RoundCompleteFunctionality;
        OnGameFailed += RoundFailedFunctionality;
    }

    private void OnDisable()
    {
        OnGameStart -= InitializeRound;
        OnGameComplete -= RoundCompleteFunctionality;
        OnGameFailed -= RoundFailedFunctionality;

    }

    private bool isPlaying;
    public void CheckTile(Tile tile)
    {
        if (isPlaying)
            return;

        if (firstTile == null)
        {
            totalRemainingTakes--;
            UpdateTotalMoves();
            firstTile = tile;
            firstTile.Show();
            return;    
        }
        else if (secondTile == null)
        {
            if(tile== firstTile)
                return;
            secondTile = tile;
            secondTile.Show();
            
        }
        if(ShowEffectCoroutine!=null)
        {
            StopCoroutine(ShowEffectCoroutine);
            
        }
        ShowEffectCoroutine = StartCoroutine(ShowEffect());
      



    }
    private IEnumerator ShowEffect()
    {
        isPlaying = true;
        yield return new WaitForSeconds(0.5f);
        if (firstTile.tileData.sprite == secondTile.tileData.sprite)
        {
            //firstTile.gameObject.SetActive(false);
            //secondTile.gameObject.SetActive(false);
            Destroy(firstTile.gameObject);
            Destroy(secondTile.gameObject);
            yield return new WaitForSeconds(0.25f);
            if (CheckRoundComplete())
            {
                OnGameComplete?.Invoke();
                Debug.Log("RoundComplete");
            }
        }
        else
        {

            firstTile.PlayAnimationWithoutEvent();
            secondTile.PlayAnimationWithoutEvent();
            yield return new WaitForSeconds(0.25f);
            firstTile.Hide();
            secondTile.Hide();

            if(CheckRoundFailed())
            {
                OnGameFailed?.Invoke();
                Debug.Log("RoundFailed");
            }


        }
        firstTile = null;
        secondTile = null;
       

       
        isPlaying = false;

    }


    public void StartGame()
    {
        //totalRemainingTakes = 0;
        UpdateTotalMoves();
        UpdateTotalPoints();
        OnGameStart?.Invoke();
    }

    private bool CheckRoundComplete()
    {
        Debug.Log("gridPlacementController.transform.childCount" + gridPlacementController.transform.childCount);
        return gridPlacementController.transform.childCount == 0;
    }

    private bool CheckRoundFailed()
    {
        return totalRemainingTakes <= 0;
    }

    public void RoundComplete()
    {
        
        StartGame();
    }

    public void RoundCompleteFunctionality()
    {
        RoundCompleteScreen.gameObject.SetActive(true);
    }
    public void RoundFailedFunctionality()
    {
        RoundFailedScreen.gameObject.SetActive(true);
    }






    private void UpdateTotalMoves()
    {
        TotalTakesView.text ="Takes: "+ totalRemainingTakes.ToString();
    }

    private void UpdateTotalPoints()
    {
        TotalPointsView.text = "Total Points: "  + PlayerPrefs.GetInt(totalPointsStr).ToString()  ;
    }




    private void InitializeRound()
    {
        TotalScore(totalRemainingTakes * 2);
        mainmenu.SetActive(false);
        gameplay.SetActive(true);
        totalRemainingTakes = (int)Mathf.Pow(gridSize,2) + gridSize*2;
        UpdateTotalMoves();
        gridPlacementController.Initilization();


    }


    public void ReturnToMainMenu()
    {
        mainmenu.SetActive(true);
        gameplay.SetActive(false);
    }   


    private void TotalScore(int score)
    {
        if (score < 0)
            return;

        totalPoints = PlayerPrefs.GetInt(totalPointsStr) + score;
        PlayerPrefs.SetInt(totalPointsStr, totalPoints);
        UpdateTotalPoints();


    }    


}
