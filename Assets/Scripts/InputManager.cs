using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
  //  public static InputManager instance;
    public Action <Vector2Int, Vector2Int> onCancel;

    //For DraggingBuildings
    private float dist;
    private bool dragging = false;
    private Vector3 offset;
    private Transform toDrag;
   
    private void Start()
    {
        

    }
    void Update()
    {
  

       // Debug.Log(t);
        if (!IsPointerOverUIObject() && Input.GetMouseButtonDown(0))   
        {
            CloseAllWindows();
            Interact();
        }




        if (Input.GetMouseButton(1))
        {
            //Cancel();
        }
        //BuildingDrag();
    }
    void CloseAllWindows()
    {
        // turnin button panel off;
        if (GameManager.instance!=null)
        {
        GameManager.instance.ToggleBuildingButtonsPanel(false);
        }

    }
    public void OnConfirmClicked()
    {
        //Debug.Log("currentPos" + BuildingManager.instance.CurrentPos);
        //Debug.Log("currentPosIsValid" + GridSystem.instance.isCellValid(BuildingManager.instance.CurrentPos));
        //if (GridSystem.instance.isCellValid(BuildingManager.instance.CurrentPos) == true)
        //{
        //    BuildingManager.instance.CanBeConstracted = true;
        //    BuildingManager.instance.ConfirmPlaceObject();
        //    BuildingDrag.instance.CloseTickCrossPanel();
        //}
       
    }
    public void Cancel(Vector2Int pos, Vector2Int size)
    {
        onCancel?.Invoke(pos,size);
    }



    void Interact()
    {
        int layer_mask = LayerMask.GetMask("Building");
        var clickPosition = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero, Mathf.Infinity, layer_mask);
        if (hit.collider != null)
        {
       
            print(hit.collider.gameObject.transform.parent.position);
            IIntractable intractableObj = hit.collider.gameObject.GetComponentInParent<IIntractable>();
            if (intractableObj != null)
            {
                intractableObj.Interacted();
            }
        }
    }
    public bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
    public string ObjUnderPointerName()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        if (EventSystem.current.currentSelectedGameObject !=null)
        {
        return results.Count > 0? EventSystem.current.currentSelectedGameObject?.name : "null";
        }
        else { return "No Obj"; }
    }
    void BuildingDragFunction()
    {
        print(Input.touchCount);
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.collider.name);
                if (hit.collider.tag == "Building")
                {
                    toDrag = hit.transform;
                    dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved)
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            toDrag.position = v3 + offset;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            dragging = false;
        }
    }
}
