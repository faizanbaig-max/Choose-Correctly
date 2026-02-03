using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GridPlacementController : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    [SerializeField] Tile tilePrefab;
    public List<GridSize> gridSizes;
    public List<TileData> tileData;
    private int totalTiles;

    private Dictionary<int, int> tileValuePair = new Dictionary<int, int>();
    private List<TileData> selectedIndex;


    public void Initilization()
    {

        if (gridLayoutGroup == null)
            gridLayoutGroup = GetComponent<GridLayoutGroup>();
        totalTiles = (int)Mathf.Pow(GameManager.instance.gridSize, 2);
        tileValuePair = new Dictionary<int, int>();
        for (int i = 0; i < totalTiles / 2; i++)
        {
            tileValuePair.Add(i, 0);
        }
        
        UpdateSettings();
        DestroyAvailableTiles();
        CreateNewTiles();
        StartCoroutine(ShowAndHide());
        Debug.Log("Grid Created");
        Invoke("DisableGridLayout", 3f);
    }

  





    private void DisableGridLayout()
    {
        gridLayoutGroup.enabled = false;
    }
    private TileData GetRandomTileData()
    {
        List<int> keys = new List<int>(tileValuePair.Keys);
        int randomIndex=0 ;
        bool check = false; ;
        while(!check)
        {
            randomIndex = Random.Range(0, keys.Count);
            check=true;
            if (tileValuePair[randomIndex] == 2)
            {
                check = false;
                
            }


        }
        tileValuePair[randomIndex] = tileValuePair[randomIndex] + 1;
        

        //int selectedKey = keys[randomIndex];
        //tileValuePair[selectedKey]++;

        //if (tileValuePair[selectedKey] >= 2)
        //{
        //    tileValuePair.Remove(selectedKey);
        //}
        return tileData[randomIndex];
    }


    private void UpdateSettings()
    {
        for (int i = 0; i < gridSizes.Count; i++)
        {
            if (gridSizes[i].size==GameManager.instance.gridSize)
            {
                gridLayoutGroup.cellSize = new Vector2(gridSizes[i].cellSize, gridSizes[i].cellSize);
                gridLayoutGroup.constraintCount = gridSizes[i].ConstrantCounts;
                gridLayoutGroup.enabled = true;
                break;
            }
        }
    }



    private void DestroyAvailableTiles()
    {
        for (int i = transform.childCount-1; i >= 0 ; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    private void CreateNewTiles()
    {
        for (int i = 0; i <  totalTiles; i++)
        {
            Tile tile = Instantiate(tilePrefab,transform);
            Debug.Log("Tile Created");
            tile.UpdateImage(GetRandomTileData());
            tile.GetComponent<Image>().raycastTarget = false;
            tile.Hide();
        }
        
    }
    private IEnumerator ShowAndHide()
    {
        for(int i = 0;i < transform.childCount;i++)
        {
            Tile tile = transform.GetChild(i).GetComponent<Tile>();
            tile.Show();
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < transform.childCount; i++)
        {
            Tile tile = transform.GetChild(i).GetComponent<Tile>();
            tile.PlayAnimationWithoutEvent();
           
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < transform.childCount; i++)
        {
            Tile tile = transform.GetChild(i).GetComponent<Tile>();
            tile.Hide();
            tile.GetComponent<Image>().raycastTarget = true;
        }
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
[System.Serializable]
public class GridSize
{
    public int size;
    public int cellSize;
    public int ConstrantCounts;
}



