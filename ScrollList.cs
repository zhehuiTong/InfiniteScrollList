using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollList : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {

    [SerializeField]
    private int listCount;
    [SerializeField]
    private int itemSpacing;
    [SerializeField]
    private int loopListCount;
    [SerializeField]
    private RectTransform imageScrolRect;
    [SerializeField]
    private RectTransform imageItemPrefad;
    [SerializeField]
    private GameObject collisionArea;
    private float beginDragPosition;
    private float EndDragPosition;

    // Use this for initialization
    void Start () {
        InitScrollList();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        beginDragPosition = imageScrolRect.anchoredPosition.y;
    }

    public void OnDrag(PointerEventData eventData)
    {
        EndDragPosition = imageScrolRect.anchoredPosition.y;
        JudgeUPAndDown();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDragPosition = imageScrolRect.anchoredPosition.y;
        JudgeUPAndDown();
    }

    private void InitScrollList()
    {
        InitImageScrolRect();
        InitCollisionArea();
        InitCreatItem();
        collisionArea.GetComponent<CollisionDetection>().Init(loopListCount, (int)imageItemPrefad.sizeDelta.y, itemSpacing);
    }

    private void InitImageScrolRect()
    {
        int rectHeight = (int)imageItemPrefad.sizeDelta.y*listCount+itemSpacing*(listCount-1);
        imageScrolRect.sizeDelta = new Vector2(500, rectHeight);
        
    }

    private void InitCollisionArea()
    {
        int collisionHeight = (int)imageItemPrefad.sizeDelta.y * loopListCount + itemSpacing * (loopListCount - 1);
        collisionArea.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(500, collisionHeight);
        BoxCollider2D collider2D = collisionArea.GetComponent<BoxCollider2D>();
        //collider2D.offset = new Vector2(0, -(collisionHeight / 2));
        collider2D.size = new Vector2(500, collisionHeight);
    }

    private void InitCreatItem()
    {
        for(int i = 0; i < loopListCount; i++)
        {
            GameObject itemGame = Instantiate(imageItemPrefad.gameObject);
            itemGame.transform.SetParent(imageScrolRect);
            int itemHeight = (int)imageItemPrefad.sizeDelta.y * i + itemSpacing * i;
            itemGame.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0, -itemHeight);
            itemGame.name = "Item" + i;
        }
    }

    private void JudgeUPAndDown()
    {
        float result = beginDragPosition - EndDragPosition;
        if (result >= 0)
        {
            Debug.Log("下");
            collisionArea.GetComponent<CollisionDetection>().IsUP = false;
        }
        else
        {
            Debug.Log("上");
            collisionArea.GetComponent<CollisionDetection>().IsUP = true;
        }
    }

   
}
