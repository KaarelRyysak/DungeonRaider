using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {
    public Transform Player;
    //public Vector3 CameraDistance = new Vector3(0f, 0f, -1f);
    [Header("Vertical movement")]

    [SerializeField]
    [Range(-40, 0)]
    private int minY;

    [SerializeField]
    [Range(0, 40)]
    private int maxY;

    [SerializeField]
    private bool limitY;

    [Header("Horizontal movement")]
    [SerializeField]
    [Range(-40, 0)]
    private int minX;

    //See on veidi suurem, kuna me ikka scrollime paremale ;)
    [SerializeField]
    [Range(0, 100)]
    private int maxX;

    [SerializeField]
    private bool limitX;



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
