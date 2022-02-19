using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Linq;

public class TouchController : MonoBehaviour
{
    public GameObject player;
    // 回転速度
    public float rotateSpeed = 5.0f;
    // 移動速度
    public float moveSpeed = 0.5f;
    bool min=false;
    bool max=false;
    private RaycastHit hit;
    private RaycastHit hit2;
    private int count = 1;
    public float Setposition=-5;
    [SerializeField]
    public Transform rayposition;
    private bool iscamera;
    private int finger;
    public GameObject targetc = null;
    private bool lockon;
    private const int ROTATE_BUTTON = 1;
    private const float ANGLE_LIMIT_UP = 60f;
    private const float ANGLE_LIMIT_DOWN = -60f;
    private Vector3 eulerAngles;

    void LateUpdate()
    {
        if (player != null)
        {
            transform.parent.transform.position = player.transform.position;
            if (transform.localPosition.z <= 0)
            {
                if (Physics.Linecast(player.transform.position, rayposition.position, out hit, LayerMask.GetMask("Environment")) == true)
                {
                    transform.position = hit.point;
                }
                else
                {
                    transform.localPosition = new Vector3(0, 0, Setposition);
                }
            }
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[Input.touchCount - 1];
                if (touch.phase == TouchPhase.Began)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        if (Physics.Raycast(ray, out hit2, 100, LayerMask.GetMask("Enemy")) == true)
                        {
                            if (targetc != hit2.collider.gameObject)
                            {
                                targetc = hit2.collider.gameObject;
                                lockon = true;
                            }
                            else
                            {
                                lockon = false;
                                targetc = null;
                            }
                        }
                        else if (Physics.Raycast(ray, out hit2, 100, LayerMask.GetMask("Player")) == true)
                        {
                            if (hit2.collider.gameObject.tag != "Mine")
                            {
                                if (targetc != hit2.collider.gameObject)
                                {
                                    targetc = hit2.collider.gameObject;
                                    lockon = true;
                                }
                                else
                                {
                                    lockon = false;
                                    targetc = null;
                                }
                            }
                        }
                        else
                        {
                            lockon = false;
                            targetc = null;
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit2, 100, LayerMask.GetMask("Enemy")) == true)
                {
                    if (targetc != hit2.collider.gameObject)
                    {
                        targetc = hit2.collider.gameObject;
                        lockon = true;
                    }
                    else
                    {
                        lockon = false;
                        targetc = null;
                    }
                }
                else if (Physics.Raycast(ray, out hit2, 100, LayerMask.GetMask("Player")) == true)
                {
                    if (hit2.collider.gameObject.tag != "Mine")
                    {
                        if (targetc != hit2.collider.gameObject)
                        {
                            targetc = hit2.collider.gameObject;
                            lockon = true;
                        }
                        else
                        {
                            lockon = false;
                            targetc = null;
                        }
                    }
                }
                else
                {
                    lockon = false;
                    targetc = null;
                }

            }
        }
        if (lockon == true)
        {
            if (targetc != null)
            {
                transform.parent.transform.LookAt(targetc.transform.position + new Vector3(0, 1, 0));
            }
        }
        else
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0)
                {
                    if (iscamera == false)
                    {
                        Touch touch = Input.touches[Input.touchCount - 1];
                        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Began)
                        {
                            iscamera = true;
                            finger = touch.fingerId;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Input.touchCount; i++)
                        {
                            int a = Input.touches[i].fingerId;
                            if (a == finger)
                            {
                                Touch touch = Input.touches[i];
                                if (touch.phase == TouchPhase.Moved)
                                {
                                    // UIを最初に触った場合はタッチでの操作をさせない
                                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                                    {
                                        this.transform.parent.gameObject.transform.eulerAngles += new Vector3(-touch.deltaPosition.y * moveSpeed, touch.deltaPosition.x * moveSpeed, 0);
                                    }
                                }
                                else if (touch.phase == TouchPhase.Ended)
                                {
                                    iscamera = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {

                Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * moveSpeed * 30, Input.GetAxis("Mouse Y") * moveSpeed * 30, 0);
                eulerAngles += new Vector3(angle.y, angle.x);

                float angle_x = 180f <= eulerAngles.x ? eulerAngles.x - 360 : eulerAngles.x;
                eulerAngles = new Vector3(Mathf.Clamp(angle_x, -60f, 60f),eulerAngles.y,eulerAngles.z);

                this.transform.parent.gameObject.transform.eulerAngles = eulerAngles;

            }
        }
    }
}