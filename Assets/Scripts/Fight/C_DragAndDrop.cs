using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DragAndDrop : MonoBehaviour
{
    private GameObject draggedObject = null;
    private Vector2 mousePos;
    private Vector2 originalPos;
    public C_FightPlayer player;

    // Update is called once per frame
    void Update()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (Input.GetMouseButtonDown(0) && draggedObject == null && !Globals.paused)
        {
            PickupGem();
        }

        if (draggedObject != null && !Globals.paused)
        {
            draggedObject.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0) && draggedObject != null || Globals.paused && draggedObject != null)
        {
            DropGem();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Globals.Pause();
        }
    }

    void PickupGem()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 1.0f);
        
        //if pickup from carrier
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider.tag == "Carrier")
            {
                C_FillSlot slot = hits[i].transform.gameObject.GetComponent<C_FillSlot>();
                if (slot != null)
                {
                    C_Box currentBox = slot.getBoxFromSlot();
                    slot.fillSlot(null);

                    if (currentBox != null)
                    {
                        currentBox.ResetScale();
                        draggedObject = currentBox.gameObject;
                    }
                    return;
                }
            }
        }

        //if pickup gem directly
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider.tag == "Gem")
            {
                draggedObject = hits[i].transform.gameObject;
                return;
            }
        }
    }

    void DropGem()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 1.0f);

        C_Box currentBox = draggedObject.GetComponent<C_Box>();

        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider.tag == "Carrier")
            {
                RaycastHit2D hit = hits[i];
                GameObject hitSlotObject = hit.transform.gameObject;

                if (currentBox != null) //Box is dragged
                {
                    C_FillSlot slot = hitSlotObject.GetComponent<C_FillSlot>();
                    if (slot != null)
                    {
                        C_Box swappedBox = slot.fillSlot(currentBox);

                        if(currentBox != null)
                        currentBox.spell.OnSwitch(swappedBox);

                        if(swappedBox != null)
                        swappedBox.spell.OnInsert(currentBox);

                        if (swappedBox != null)
                        {
                            swappedBox.ResetScale();
                            swappedBox.ResetPosition();
                        }

                        currentBox.transform.position = hit.collider.transform.position;
                        RescaleObjectToObjectByHitbox(currentBox.gameObject, hit.transform.gameObject);
                        draggedObject = null;
                    }
                }
            }
        }

        if(draggedObject != null)
        {
            draggedObject.transform.position = currentBox.originalPosition;
            draggedObject = null;
        }
    }


    void RescaleObjectToObjectByHitbox(GameObject originalObject, GameObject fitObject)
    {
        float fitX = fitObject.GetComponent<BoxCollider2D>().size.x;
        float fitY = fitObject.GetComponent<BoxCollider2D>().size.y;

        float originalX = originalObject.GetComponent<SpriteRenderer>().size.x;
        float originalY = originalObject.GetComponent<SpriteRenderer>().size.y;

        Vector3 rescale = originalObject.transform.localScale;

        rescale.x *= fitX / originalX;
        rescale.y *= fitY / originalY;

        originalObject.transform.localScale = rescale;
    }
}

