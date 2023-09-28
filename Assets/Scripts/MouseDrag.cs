using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class MouseDrag : MonoBehaviour
{
    Vector3 mousePosition;
    Camera mainCam;
    Vector3 targetPosition;

    bool isDragging = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            targetPosition = mainCam.ScreenToWorldPoint(Input.mousePosition - mousePosition);
            if (Input.GetMouseButtonUp(1))
            {
                isDragging = false;
                rb.useGravity = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            float distance = Vector3.Distance(targetPosition, transform.position);
            if (distance > 0.01)
            {
                Vector3 dir = (targetPosition - transform.position).normalized;
                float spd = UtilityMethods.Map(distance, 1, 0.01f, 1000, 0);
                rb.velocity = dir * spd * Time.fixedDeltaTime;;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            mousePosition = Input.mousePosition - mainCam.WorldToScreenPoint(transform.position);
            isDragging = true;
            rb.useGravity = false;
        }
    }
}
