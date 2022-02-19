using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    [SerializeField] public float duration = 1.0f;
    [SerializeField] public float _distance;

    public float distance
    {
        set
        {
            _distance = value;
            targetPosition = GetTargetPosition(value);
        }
        get
        {
            return _distance;
        }

    }
    private Vector3 GetTargetPosition(float distance)
    {
        if (targetPosition == Vector3.zero)
        {
            return transform.position + transform.forward * distance;
        }
        else
        {
            return (transform.position + targetPosition).normalized * distance;

        }
    }


    [SerializeField]public float pastTime = 0;
    [SerializeField]public Vector3 homePosition;
    [SerializeField]public Vector3 targetPosition;
    [SerializeField]public Rigidbody rigidBody;

    private void FixedUpdate()
    {
        pastTime += Time.fixedDeltaTime;
        rigidBody.MovePosition(
            Vector3.Lerp(homePosition, targetPosition, Mathf.PingPong(pastTime, duration) / duration));
    }
}