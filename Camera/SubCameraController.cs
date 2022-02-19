using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraController : MonoBehaviour
{
    [SerializeField] public GameObject mainchara;
    public GameObject enemy;
    public Vector3 subpos;
    [SerializeField] public LockOnTargetDetector lockon;
    [SerializeField] public SubCameraSwitch menu;

    private void Update()
    {
        this.enemy = lockon.targetc;

        if(enemy!=null)
        {
            subpos = (mainchara.transform.position + enemy.transform.position) / 2;
            this.transform.parent.transform.position = subpos;
            this.transform.LookAt(subpos);
        }
        else
        {
          //  menu.MainCameraON();
        }
    }
}
