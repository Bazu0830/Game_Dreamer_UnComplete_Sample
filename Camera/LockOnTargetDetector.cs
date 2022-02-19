using UnityEngine;

public class LockOnTargetDetector : MonoBehaviour
{
    public GameObject targetc = null;
    public Transform point;
    [SerializeField] MyCameraController controller;
    private float rimit;
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[Input.touchCount - 1];
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit = new RaycastHit();
                if (Physics.Linecast(point.transform.position, transform.position, out hit, LayerMask.GetMask("Enemy")) == true)
                {
                    if (targetc != hit.collider.gameObject)
                    {
                        targetc = hit.collider.gameObject;
                        controller.lockon = true;
                    }
                    else
                    {
                        controller.lockon = false;
                        targetc = null;
                    }
                }
                else if (Physics.Linecast(point.transform.position, transform.position, out hit, LayerMask.GetMask("Player")) == true)
                {
                    if (hit.collider.gameObject.tag == "Mine")
                    {
                        return;
                    }
                    if (targetc != hit.collider.gameObject)
                    {
                        targetc = hit.collider.gameObject;
                        controller.lockon = true;
                    }
                    else
                    {
                        controller.lockon = false;
                        targetc = null;
                    }
                }
            }
        }
    }
}