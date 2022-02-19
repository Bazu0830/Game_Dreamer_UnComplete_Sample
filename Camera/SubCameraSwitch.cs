using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraSwitch : MonoBehaviour
{
    private int count = 1;
    [SerializeField] TouchController touchController;

    public void CameraSwitch()
    {
        if (count == 0)
        {
            count = 1;
            touchController.Setposition = 0;
            touchController.rayposition.localPosition = new Vector3(0, 0, 0);
        }
        else if (count == 1)
        {
            count = 2;
            touchController.Setposition = -5;
            touchController.rayposition.localPosition = new Vector3(0, 0, -5);
        }
        else if (count == 2)
        {
            count = 3;
            touchController.Setposition = -10;
            touchController.rayposition.localPosition = new Vector3(0, 0, -10);
        }
        else if (count == 3)
        {
            count = 4;
            touchController.Setposition = -20;
            touchController.rayposition.localPosition = new Vector3(0, 0, -20);
        }
        else if (count == 4)
        {
            count = 0;
            touchController.Setposition = 0;
            touchController.rayposition.localPosition = new Vector3(0, 0, 0);
        }

    }
}
