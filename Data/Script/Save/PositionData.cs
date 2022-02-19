using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionData : MonoBehaviour
{
    
    public GameObject player;
    [SerializeField]
    private Vector3 position;
    private void Update()
    {
        if (player != null)
        {
            position = player.transform.position;
        }
    }
    public void SetPosition(Vector3 position)
    {
        player.transform.position = position;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public void ToOne()
    {
        SceneManager.LoadScene("One");
    }
    public void ToZero()
    {
        SceneManager.LoadScene("Zero");
    }
}
