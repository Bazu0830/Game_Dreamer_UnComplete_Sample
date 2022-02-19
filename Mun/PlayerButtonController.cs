using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonController : MonoBehaviour
{

    public GameObject player;

    public void ChargeAttack()
    {
        player.GetComponent<UnityChanScript>().ChargeAttack();
    }

    public void NAttack()
    {
        player.GetComponent<UnityChanScript>().SetNAttack();
    }
    public void Tackle()
    {
        player.GetComponent<UnityChanScript>().Tackle();
    }
    public void LowAttack()
    {
        player.GetComponent<UnityChanScript>().LowAttack();
    }
    public void Roling()
    {
        player.GetComponent<UnityChanScript>().Roling();
    }
    public void Jump()
    {
        player.GetComponent<UnityChanScript>().Jump();
    }
    public void Counter()
    {
        player.GetComponent<UnityChanScript>().Counter();
    }
    public void Guard()
    {
        player.GetComponent<UnityChanScript>().SetGuard();
    }
    public void NotGuard()
    {
        player.GetComponent<UnityChanScript>().SetNotGuard();
    }
    public void Wait()
    {
        player.GetComponent<UnityChanScript>().SetWait();
    }
    public void Fish()
    {
        player.GetComponent<UnityChanScript>().FishingStart();
    }
    public void FishEnd()
    {
        player.GetComponent<UnityChanScript>().FishingEnd();
    }

    public void AutoDirection()
    {
        if (player.GetComponent<UnityChanScript>().autodirection == 0)
        {
            player.GetComponent<UnityChanScript>().autodirection = 1;//近接型
        }
        else if (player.GetComponent<UnityChanScript>().autodirection == 1)
        {
            player.GetComponent<UnityChanScript>().autodirection = 2;//遠距離型
        }
        else
        {
            player.GetComponent<UnityChanScript>().autodirection = 0;//手動
        }
    }

    private Vector2 attackstart;
    private Vector2 attackend;
    public int attackfinger;
    private bool isAttack=false;
    private float attacktime;


    public void OnAttack()
    {
        if (isAttack == false)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android
                attackfinger = Input.touchCount - 1;
                Touch touch = Input.touches[attackfinger];
                attackfinger = touch.fingerId;
                attackstart = touch.position;
                attacktime = 0;
                isAttack = true;
                ChargeAttack();
            }
            else
            {
                attackstart = Input.mousePosition;
                attacktime = 0;
                isAttack = true;
                ChargeAttack();
            }
        }
    }
    public void OffAttack()
    {
        if (isAttack == true)
        {
            isAttack = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android

                for (int i = 0; i < Input.touchCount; i++)
                {
                    int a = Input.touches[i].fingerId;
                    if (a == attackfinger)
                    {
                        Touch touch = Input.touches[i];
                        if (touch.phase == TouchPhase.Ended)
                        {
                            attackend = touch.position;
                            Vector2 SwipeRange = new Vector2((new Vector3(attackend.x, 0, 0) - new Vector3(attackstart.x, 0, 0)).magnitude,
                                (new Vector3(0, attackend.y, 0) - new Vector3(0, attackstart.y, 0)).magnitude);

                            if (SwipeRange.x <= 50f && SwipeRange.y <= 50f)
                            {
                                Debug.Log("Tap");
                                NAttack();
                            }
                            /*      else if (SwipeRange.x > SwipeRange.y)
                                  {
                                      float _x = Mathf.Sign(attackend.x - attackstart.x);
                                      if (_x > 0) Debug.Log("Right");
                                      else if (_x < 0) Debug.Log("Left");
                                  }
                            */
                            else
                            {
                                float _y = Mathf.Sign(attackend.y - attackstart.y);
                                if (_y > 0)
                                {
                                    Debug.Log("Up");
                                    Tackle();
                                }
                                else if (_y < 0)
                                {
                                    Debug.Log("Down");
                                    LowAttack();
                                }
                            }

                        }
                    }
                }
            }
            else
            {

                attackend = Input.mousePosition;
                Vector2 SwipeRange = new Vector2((new Vector3(attackend.x, 0, 0) - new Vector3(attackstart.x, 0, 0)).magnitude,
                    (new Vector3(0, attackend.y, 0) - new Vector3(0, attackstart.y, 0)).magnitude);

                if (SwipeRange.x <= 50f && SwipeRange.y <= 50f)
                {
                    Debug.Log("Tap");
                    NAttack();
                }
                /*      else if (SwipeRange.x > SwipeRange.y)
                      {
                          float _x = Mathf.Sign(attackend.x - attackstart.x);
                          if (_x > 0) Debug.Log("Right");
                          else if (_x < 0) Debug.Log("Left");
                      }
                */
                else
                {
                    float _y = Mathf.Sign(attackend.y - attackstart.y);
                    if (_y > 0)
                    {
                        Debug.Log("Up");
                        Tackle();
                    }
                    else if (_y < 0)
                    {
                        Debug.Log("Down");
                        LowAttack();
                    }
                }
            }
        }
    }
    private Vector2 blockstart;
    private Vector2 blockend;
    public int blockfinger;
    private bool isBlock = false;
    private float blocktime = 0;

    public void OnBlock()
    {
        if (isBlock == false)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android

                blockfinger = Input.touchCount - 1;
                Touch touch = Input.touches[blockfinger];
                blockfinger = touch.fingerId;
                blockstart = touch.position;
                blocktime = 0;
                isBlock = true;
                Guard();
            }
            else
            {
                blockstart = Input.mousePosition;
                blocktime = 0;
                isBlock = true;
                Guard();
            }
        }
    }
    public void OffBlock()
    {
        if (isBlock == true)
        {
            isBlock = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                // Android

                for (int i = 0; i < Input.touchCount; i++)
                {
                    int a = Input.touches[i].fingerId;
                    if (a == blockfinger)
                    {
                        Touch touch = Input.touches[i];
                        if (touch.phase == TouchPhase.Ended)
                        {
                            blockend = touch.position;
                            Vector2 SwipeRange = new Vector2((new Vector3(blockend.x, 0, 0) - new Vector3(blockstart.x, 0, 0)).magnitude,
                                (new Vector3(0, blockend.y, 0) - new Vector3(0, blockstart.y, 0)).magnitude);

                            if (SwipeRange.x <= 30f && SwipeRange.y <= 30f)
                            {
                                Debug.Log("Tap");
                                NotGuard();
                            }
                            /*     else if (SwipeRange.x > SwipeRange.y)
                                 {
                                     float _x = Mathf.Sign(blockend.x - blockstart.x);
                                     if (_x > 0) Debug.Log("Right");
                                     else if (_x < 0) Debug.Log("Left");
                                 }
                            */
                            else
                            {
                                float _y = Mathf.Sign(blockend.y - blockstart.y);
                                if (_y > 0)
                                {
                                    Debug.Log("Up");
                                    Roling();
                                }
                                else if (_y < 0)
                                {
                                    Debug.Log("Down");
                                    Jump();
                                }

                            }
                        }
                    }
                }
            }
            else
            {
                blockend = Input.mousePosition;
                Vector2 SwipeRange = new Vector2((new Vector3(blockend.x, 0, 0) - new Vector3(blockstart.x, 0, 0)).magnitude,
                    (new Vector3(0, blockend.y, 0) - new Vector3(0, blockstart.y, 0)).magnitude);

                if (SwipeRange.x <= 30f && SwipeRange.y <= 30f)
                {
                    Debug.Log("Tap");
                    NotGuard();
                }
                /*     else if (SwipeRange.x > SwipeRange.y)
                     {
                         float _x = Mathf.Sign(blockend.x - blockstart.x);
                         if (_x > 0) Debug.Log("Right");
                         else if (_x < 0) Debug.Log("Left");
                     }
                */
                else
                {
                    float _y = Mathf.Sign(blockend.y - blockstart.y);
                    if (_y > 0)
                    {
                        Debug.Log("Up");
                        Roling();
                    }
                    else if (_y < 0)
                    {
                        Debug.Log("Down");
                        Jump();
                    }

                }
            }
        }
    }

    private void Update()
    {
        if (isAttack == true)
        {
            attacktime += Time.deltaTime;
        }
        if (isBlock == true)
        {
            blocktime += Time.deltaTime;
        }
    }
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}


