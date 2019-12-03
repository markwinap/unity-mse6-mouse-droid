using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Tutorial
https://www.youtube.com/watch?v=8xdXJtu6nig
*/

public class InputManager : MonoBehaviour
{
    public float acceleration;
    public float steer;
    public bool brake;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Input.GetAxis("Vertical");//Get Float from using keyboard WS or Up and Down
        steer = Input.GetAxis("Horizontal");//Get Float from using keyboard AD or Left and Rigth
        brake = Input.GetKey(KeyCode.Space);
    }
}
