using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float borderPadding = 100.0f;
    public GameObject arrowGameObject;
    private Camera mainCamera;
    private GameObject warningSign;
    private bool isActive;
    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (warningSign != null)
        {
            Vector3 direction = warningSign.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

            Vector3 warningSignScreenPoint = mainCamera.WorldToScreenPoint(warningSign.transform.position);

            if (CheckIfOffCamera())
            {
                Vector3 cappedTargetPosition = warningSignScreenPoint;
                if (cappedTargetPosition.x <= borderPadding)
                    cappedTargetPosition.x = borderPadding;
                if (cappedTargetPosition.x >= Screen.width - borderPadding)
                    cappedTargetPosition.x = Screen.width - borderPadding;
                if (cappedTargetPosition.y <= borderPadding)
                    cappedTargetPosition.y = borderPadding;
                if (cappedTargetPosition.y >= Screen.height - borderPadding)
                    cappedTargetPosition.y = Screen.height - borderPadding;

                Vector3 pointerWorldPosition = mainCamera.ScreenToWorldPoint(cappedTargetPosition);
                transform.position = pointerWorldPosition;
                transform.localPosition = new Vector3(pointerWorldPosition.x, pointerWorldPosition.y, 0);
            }
        }
    }

    public void SetWarningSign(GameObject warningSignGameObject)
    {
        warningSign = warningSignGameObject;
    }

    public bool CheckIfOffCamera()
    {
        if (mainCamera == null || warningSign == null) return false; 
        Vector3 warningSignScreenPoint = mainCamera.WorldToScreenPoint(warningSign.transform.position);
        return warningSignScreenPoint.x <= borderPadding || warningSignScreenPoint.x >= Screen.width - borderPadding || warningSignScreenPoint.y <= borderPadding || warningSignScreenPoint.y >= Screen.height - borderPadding;
    }

    public void SetActive(bool active)
    {
        arrowGameObject.SetActive(active);
    }

    public bool GetActive()
    {
        return isActive;
    }

    public void SetArrowObject(GameObject arrowObject)
    {
        arrowGameObject = arrowObject;
    }
}
