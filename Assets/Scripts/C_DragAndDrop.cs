using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_DragAndDrop : MonoBehaviour
{
    private GameObject draggedObject = null;
    private Vector2 mousePos;
    private Vector2 originalPos;

    // Update is called once per frame
    void Update()
    {
        mousePos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        mousePos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if (Input.GetMouseButtonDown(0) && draggedObject == null)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll (mousePos, Vector2.zero, 1.0f);

            Debug.Log("2");

            for (int i = 0; i < hits.Length; ++i)
            {
                if (hits[i].collider.tag == "Gem")
                {
                    Debug.Log("2");
                    draggedObject = hits[i].transform.gameObject;
                    C_Box currentBox = draggedObject.GetComponent<C_Box>();
                    if (currentBox != null)
                    {
                        Debug.Log("3");
                        if (currentBox.slotted)
                        {
                            currentBox.slotted = false;
                            draggedObject.transform.localScale = currentBox.originalScale;
                        }


                    }
                }
            }
        }

        if (draggedObject != null)
        {
            draggedObject.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0) && draggedObject != null)
        {

            RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, Vector2.zero, 1.0f);

            C_Box currentBox = draggedObject.GetComponent<C_Box>();

            for (int i = 0; i < hits.Length; ++i)
            {
                if (hits[i].collider.tag == "Carrier")
                {
                    RaycastHit2D hit = hits[i];

                    if(currentBox != null)
                    {
                        if(!currentBox.slotted)
                        {
                            currentBox.slotted = true;

                            draggedObject.transform.position = hit.collider.transform.position;

                            float hitboxX = hit.transform.gameObject.GetComponent<BoxCollider2D>().size.x;
                            float hitboxY = hit.transform.gameObject.GetComponent<BoxCollider2D>().size.y;

                            float sizeY = draggedObject.GetComponent<BoxCollider2D>().size.x;
                            float sizeX = draggedObject.GetComponent<BoxCollider2D>().size.y;

                            Vector3 rescale = draggedObject.transform.localScale;
                            Debug.Log(rescale.x);
                            Debug.Log(rescale.y);

                            rescale.x = hitboxX * rescale.x / sizeX;
                            rescale.y = hitboxY * rescale.y / sizeY;

                            draggedObject.transform.localScale = rescale;
                        }
                    }                                                                                                                     
                }
            }
            
            if(!currentBox.slotted)
            {
                draggedObject.transform.position = currentBox.originalPosition;
            }

            draggedObject = null;
        }
    }
}
