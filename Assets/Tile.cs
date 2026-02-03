
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Tile : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    public TileData tileData { get; private set; }
    public void UpdateImage(TileData tileData)
    {
        if(image==null)
            image = GetComponent<Image>();
        this.tileData = tileData;
        Show();
    }
    public void Show()
    {
        image.sprite = tileData.sprite;
    }
    public void Hide()
    {
        image.sprite = GameManager.instance.emptySprite;
    }
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameManager.instance.CheckTile(this);

        ////Use this to tell when the user right-clicks on the Button
        //if (pointerEventData.button == PointerEventData.InputButton.Right)
        //{
        //    //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        //    Debug.Log(name + " Game Object Right Clicked!");
        //}

        //Use this to tell when the user left-clicks on the Button
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            
        }
    }

}
[System.Serializable]
public class TileData
{
    public Sprite sprite;
    
}
