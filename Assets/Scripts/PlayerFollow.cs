using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour {
    public Transform Player;
    public Vector3 CameraDistance = new Vector3(0f, 0f, -1f);

    private void Awake()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update () {
        this.transform.position = Player.position + CameraDistance;
    }
}
