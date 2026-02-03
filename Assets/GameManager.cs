using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Sprite emptySprite;
    public GridPlacementController gridPlacementController;
    public int gridSize;
    private Tile firstTile,secondTile;
    
    #region Actions
    
    public delegate void IGameAction();
    public static event IGameAction OnGameStart;
    public static event IGameAction OnGameComplete;
    public static event IGameAction OnGameSelect;

    #endregion






    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);

        }
    }
      

    public void CheckTile(Tile tile)
    {
        if (firstTile == null)
        {
            firstTile = tile;
            firstTile.Show();
            return;    
        }
        else if (secondTile == null)
        {
            secondTile = tile;
            secondTile.Show();
            
        }

        if(firstTile.tileData.sprite == secondTile.tileData.sprite)
        {
            firstTile.gameObject.SetActive(false);
            secondTile.gameObject.SetActive(false);
        }
        else
        {
            firstTile.Hide();
            secondTile.Hide();
        }




    }

    public void StartGame()
    {
        OnGameStart?.Invoke();
        InitializeRound();
    }



    private void InitializeRound()
    {

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
