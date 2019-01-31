using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDetection : MonoBehaviour {

    private bool isUP = false;
    private int loopListCount;
    private int itemPrefadHeigh;
    private int itemSpacing;

    public bool IsUP
    {
        get
        {
            return isUP;
        }

        set
        {
            isUP = value;
        }
    }

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ScrollItem")
        {
            Image currentItem = collision.collider.GetComponent<Image>();
            Vector2 currentAnchoredPosition= currentItem.rectTransform.anchoredPosition;
            int itemHeigh = -(itemPrefadHeigh * loopListCount + itemSpacing * loopListCount);
            if (isUP)
            {
                currentItem.rectTransform.anchoredPosition = new Vector2(0, currentAnchoredPosition.y + itemHeigh);
                int index = int.Parse(currentItem.gameObject.name.Remove(0,4));
                currentItem.name = "Item" + (index +loopListCount);

            }
            else
            {
                int index = int.Parse(currentItem.gameObject.name.Remove(0, 4));
                currentItem.rectTransform.anchoredPosition = new Vector2(0, currentAnchoredPosition.y - itemHeigh);
                currentItem.name = "Item" + (index - loopListCount);
            }
        }
    }

    public void Init(int listCount,int itemHeight,int spacing)
    {
        this.loopListCount = listCount;
        this.itemPrefadHeigh = itemHeight;
        this.itemSpacing = spacing;
    }

}
