using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    public Camera cam;
    public float degree;

    public GameObject mainchara;
    public GameObject enemy;
    public Vector3 subpos;
    [SerializeField] public LockOnTargetDetector lockon;
    [SerializeField] public SubCameraSwitch menu;
    private RaycastHit hit;
    [SerializeField] private Transform point;
    [SerializeField] TouchController touchController;

    private void Update()
    {
        this.enemy = lockon.targetc;

        if (enemy != null)
        {
            subpos = (mainchara.transform.position + enemy.transform.position) / 2 +new Vector3(0,1,0);
            this.transform.parent.transform.position = subpos;
            Vector3 dif = mainchara.transform.position - enemy.transform.position;
            float radian = Mathf.Atan2(dif.z, dif.x);
            degree = radian * Mathf.Rad2Deg;
            this.transform.parent.transform.rotation = Quaternion.Euler(0, -degree, 0);
            this.transform.LookAt(subpos);

            if (Physics.Linecast(subpos, point.position, out hit, LayerMask.GetMask("Environment")) == true)
            {
                transform.position = hit.point;
            }
            else
            {
                transform.localPosition = new Vector3(0, 1, -10);
            }
            float height = Mathf.Abs(transform.localPosition.z);

            // 画面：左	screen_LeftBottom.x画面：右 screen_RightTop.x画面：上 screen_RightTop.y画面：下 screen_LeftBottom.y
            Vector3 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);
            Vector3 screen_RightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            float length = Mathf.Abs(Mathf.Sqrt(Mathf.Pow((subpos.x - mainchara.transform.position.x), 2)
                + Mathf.Pow((subpos.z - mainchara.transform.position.z), 2)))+2;

            cam.fieldOfView = Mathf.Atan2(length+(screen_LeftBottom.y/100),height)* Mathf.Rad2Deg*2;

           
        }
        else
        {
            menu.CameraSwitch();
        }
    }
}