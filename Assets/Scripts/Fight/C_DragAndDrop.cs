using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C_DragAndDrop : MonoBehaviour
{
    private GameObject draggedObject = null;
    private Vector2 mousePos;
    private Vector2 originalPos;
    public C_FightPlayer player;

    public Camera main;
    public C_Box hoveredGem;

    // Update is called once per frame
    void Update()
    {
        mousePos.x = main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePos.y = main.ScreenToWorldPoint(Input.mousePosition).y;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if(Globals.paused)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        MouseOverGem();
    }

    void MouseOverGem()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 1.0f);
        for (int i = 0; i < hits.Length; ++i)
        {
            if (hits[i].collider.tag == "Gem")
            {
                if (hits[i].transform.gameObject.GetComponent<C_Box>() != hoveredGem)
                {
                    if(hoveredGem != null)
                    {
                        hoveredGem.MouseOut();
                        hoveredGem = null;
                    }

                    hoveredGem = hits[i].transform.gameObject.GetComponent<C_Box>();
                    hoveredGem.MouseIn();
                }
                return;
            }
        }

        if (hoveredGem != null)
        {
            hoveredGem.MouseOut();
            hoveredGem = null;
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
                        draggedObject.GetComponent<C_Box>().onStartDrag();
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
                draggedObject.GetComponent<C_Box>().onStartDrag();
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

                        RescaleObjectToObjectByHitbox(currentBox.gameObject, hit.transform.gameObject);
                        currentBox.transform.position = hit.collider.transform.position;

                        currentBox.onStopDrag();
                        hoveredGem = null;
                        draggedObject = null;
                    }
                }
            }
        }

        if(draggedObject != null)
        {
            draggedObject.GetComponent<C_Box>().ResetPosition();
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

