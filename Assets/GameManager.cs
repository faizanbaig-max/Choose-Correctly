using System.Collections;
using System.Collections.Generic;
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
    public GameObject mainmenu, gameplay;


    #region Actions
    
    public delegate void IGameAction();
    public static event IGameAction OnGameStart;
    public static event IGameAction OnGameComplete;
    public static event IGameAction OnGameSelect;

    #endregion


    private void OnEnable()
    {
        OnGameStart += InitializeRound;
    }

    private void OnDisable()
    {
        OnGameStart -= InitializeRound;
    }




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
    }

    private bool isPlaying;
    public void CheckTile(Tile tile)
    {
        if (isPlaying)
            return;

        if (firstTile == null)
        {
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
        yield return new WaitForSeconds(0.25f);
        if (firstTile.tileData.sprite == secondTile.tileData.sprite)
        {
            firstTile.gameObject.SetActive(false);
            secondTile.gameObject.SetActive(false);

        }
        else
        {
            firstTile.Hide();
            secondTile.Hide();
        }
        firstTile = null;
        secondTile = null;
        isPlaying = false;
    }


    public void StartGame()
    {
       

        OnGameStart?.Invoke();
    }



    private void InitializeRound()
    {
        mainmenu.SetActive(false);
        gameplay.SetActive(true);

        gridPlacementController.Initilization();


    }


    public void ReturnToMainMenu()
    {
        mainmenu.SetActive(true);
        gameplay.SetActive(false);
    }   




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
