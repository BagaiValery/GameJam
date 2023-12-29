using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    public delegate void TapOverFan(GameObject gameObject);
    public static event TapOverFan OnTapOverFan;

    public Vector3 TargetVector { get; set; }
    public Vector3 RightTargetVector { get; set; }
    public Vector3 LeftTargetVector { get; set; }

    Coroutine moveRightCoroutine;
    Coroutine moveLeftCoroutine;

    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnPerformTap += GetTap;

        inputManager.OnStartLeft += StartMovement;
        inputManager.OnStartRight += StartMovement;

        inputManager.OnEndRight += EndRightMovement;
        inputManager.OnEndLeft += EndLeftMovement;
    }

    private void OnDisable()
    {
        inputManager.OnPerformTap -= GetTap;

        inputManager.OnStartLeft -= StartMovement;
        inputManager.OnStartRight -= StartMovement;

        inputManager.OnEndRight -= EndRightMovement;
        inputManager.OnEndLeft -= EndLeftMovement;

        
    }

    private void GetTap(Vector2 v)
    {
        Debug.Log("TapOnFan");

        Ray ray = Camera.main.ScreenPointToRay(v);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if (hit.collider.tag == "Fan" && !IsClickOnUI(v))
            {
                if (OnTapOverFan != null) OnTapOverFan(hit.collider.gameObject);
                
            }


            //Debug.Log(hit.collider.tag + " " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            //testTText.text = hit.collider.tag + " " + LayerMask.LayerToName(hit.collider.gameObject.layer);
        }
    }

    public bool IsClickOnUI(Vector2 vec)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = vec;
        List<RaycastResult> raycastResultsList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);
        bool mouseOverUI = false;
        for (int i = 0; i < raycastResultsList.Count; i++)
        {
            if (raycastResultsList[i].gameObject.layer == LayerMask.NameToLayer("UI"))//GetType() == typeof(GameObject))
            {
                mouseOverUI = true;
                //testText.text = "UI";
                break;
            }
        }
        return mouseOverUI;
    }

    #region Left-Right Movement
    public void StartMovement(Vector3 vector3, bool right)
    {
        switch (right)
        {
            case true: // right
                RightTargetVector = vector3;
                moveRightCoroutine = StartCoroutine(SetMovementVector(vector3));
                break;
            case false: // left
                LeftTargetVector = vector3;
                moveLeftCoroutine = StartCoroutine(SetMovementVector(vector3));
                break;
        }
    }

    public void EndRightMovement()
    {
        StopCoroutine(moveRightCoroutine);
        TargetVector = Vector3.zero;
        RightTargetVector = Vector3.zero;
    }
    public void EndLeftMovement()
    {
        StopCoroutine(moveLeftCoroutine);
        TargetVector = Vector3.zero;
        LeftTargetVector = Vector3.zero;
    }

    IEnumerator SetMovementVector(Vector3 vector3)
    {
        while (true)
        {
            TargetVector = Vector3.zero;
            TargetVector += LeftTargetVector + RightTargetVector;
            yield return null;
        }
    }
    #endregion
}
