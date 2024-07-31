using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float upwardForce = 5f;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float rateTime = 0.2f;
    private float shootTime;
    private void OnEnable()
    {
        GlobalEventManager.OnSendRightClickMouse += OnGetMouseClick;
    }

    private void OnGetMouseClick(Vector2 vector)
    {
        if (Time.time > shootTime + rateTime)
        {
            Throw(vector);
            shootTime = Time.time;
        }
        
    }

    private void Throw(Vector2 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 throwDirection = (hit.point - transform.position).normalized;
            throwDirection.y += upwardForce;

            GameObject bomb = Instantiate(bombPrefab, startPosition.position, Quaternion.identity);
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
    }

    private void OnDisable()
    {
        GlobalEventManager.OnSendRightClickMouse -= OnGetMouseClick;
    }

}
