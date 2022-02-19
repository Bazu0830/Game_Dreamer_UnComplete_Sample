using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroCamera : MonoBehaviour
{
    public GameObject targetObject;

    void LateUpdate()
    {
        if (targetObject != null)
        {
            transform.LookAt(targetObject.transform.position + new Vector3(0, 1, 0));
        }
    }
}
