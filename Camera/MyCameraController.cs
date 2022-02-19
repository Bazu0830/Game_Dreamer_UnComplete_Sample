using UnityEngine;

public class MyCameraController : MonoBehaviour
{
    [SerializeField]
    TouchController touch;
    [SerializeField]
    public GameObject mainchara = null;
    [SerializeField]
    private LockOnTargetDetector lockOnTargetDetector;
    [SerializeField]
    public GameObject mainCamera;
    public float rotate_speed;
    private GameObject lockOnTarget;
    private const int ROTATE_BUTTON = 1;
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;
    public bool lockon=false;
    private GameObject subplayer = null;
    
    void LateUpdate()
    {
        if (mainchara != null)
        {
            if (subplayer != mainchara)
            {
                subplayer = mainchara;
                touch.player = mainchara;
            }
            transform.position = mainchara.transform.position;
            /*       if (Input.GetKeyDown(KeyCode.R))
                   {

                       if (target != null)
                       {
                           lockOnTarget = target;
                       }
                       else
                       {
                           lockOnTarget = null;
                       }
                   }
           */
            if (lockon == true)
            {
                lockOnTargetObject();
            }
            else
            {
                rotateCmaeraAngle();
            }
            float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
            transform.eulerAngles = new Vector3(
                Mathf.Clamp(angle_x, ANGLE_LIMIT_DOWN, ANGLE_LIMIT_UP),
                transform.eulerAngles.y,
                transform.eulerAngles.z
            );
        }
    }
    public void rotateCmaeraAngle()
    {
        Vector3 angle = new Vector3(
            Input.GetAxis("Mouse X") * rotate_speed,
            Input.GetAxis("Mouse Y") * rotate_speed,
            0
        );

        transform.eulerAngles += new Vector3(angle.y, angle.x);
    }
    private void lockOnTargetObject()
    {
        transform.LookAt(lockOnTargetDetector.targetc.transform, Vector3.up);
    }
    public void LockOnButton()
    {
        if (lockOnTargetDetector.targetc != null
            &&lockon ==false)
        {
            lockon = true;
        }
        else if(lockOnTargetDetector.targetc !=null
              &&lockon==true)
        {
            lockon = false;
            lockOnTargetDetector.targetc = null;
        }
        else
        {
            lockon = false;
        }
    }
}