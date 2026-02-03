
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
public class Tile : MonoBehaviour, IPointerClickHandler
{
    private Image image;
    Animator animator;
    bool isplaying;
    public TileData tileData { get; private set; }
    public void UpdateImage(TileData tileData)
    {
        if(image==null)
            image = GetComponent<Image>();
        if (animator == null)
            animator = GetComponent<Animator>();
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
       


        PlayAnimationWithEvent();



    }

    public void PlayAnimationWithEvent()
    {
        
        animator.Rebind();
        animator.Play("TileFlip", 0);
    }
    public void PlayAnimationWithoutEvent()
    {
        animator.Rebind();
        animator.Play("TileFlip 2nd", 0);
    }

  

    private void Effect()
    {
        GameManager.instance.CheckTile(this);
        isplaying = false;
    }



}
[System.Serializable]
public class TileData
{
    public Sprite sprite;
    
}
