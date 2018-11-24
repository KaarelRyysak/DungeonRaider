using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {
    public Transform Player;
    //public Vector3 CameraDistance = new Vector3(0f, 0f, -1f);
    [Range(-40, 0)]
    public int minY;
    [Range(0, 40)]
    public int maxY;
    [Range(-40, 0)]
    public int minX;
    [Range(0, 40)]
    public int maxX;

    public bool limitX;
    public bool limitY;



    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update () {
        Vector3 newPos = new Vector3(Player.position.x, Player.position.y, this.transform.position.z);
        if (limitX)
        {
            if (newPos.x > maxX)
            {
                newPos.x = maxX;
            }
            else if (newPos.x < minX)
            {
                newPos.x = minX;
            }
        }
        if (limitY)
        {
            if (newPos.y > maxY)
            {
                newPos.y = maxY;
            }
            else if (newPos.y < minY)
            {
                newPos.y = minY;
            }
        }
        this.transform.position = newPos;
    }
}
