using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;//Car Object
    public float distance = 5f;
    public float height = 2f;
    public float dampening = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //public static Vector3 Lerp(Vector3 a, Vector3 b, float t); 
        //https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        //Linearly interpolates between two points.
        //Move Camera from starting position (a) to focus position (b)  + distance offset, time to reach position b
        transform.position = Vector3.Lerp(
            transform.position,//Initial Position
            target.transform.position + target.transform.TransformDirection(new Vector3(0f, height, -distance)),//Target Position + Offset
            dampening * Time.deltaTime//Between 0-1
        );
        //public void LookAt(Transform target);
        //public void LookAt(Transform target, Vector3 worldUp = Vector3.up);
        transform.LookAt(target.transform);//Force Camera to point to target
    }
}
