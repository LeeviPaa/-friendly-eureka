using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public GameObject launchObject;
    public GameObject cannon;
    
    public Transform spawnTransform;
    public float horizRotate = 0.0f;
    public float sensitivityX = 60;
  
    public float vertRotate = -20.0f;
    public float sensitivityY = 50;
  
    void Update()
    {
        horizRotate += Input.GetAxis("Horizontal") * sensitivityX * Time.deltaTime;
        horizRotate = Mathf.Clamp(horizRotate, -180, 180);
        //transform.rotation = Quaternion.Euler(0, horizRotate, 0);
  
        vertRotate += Input.GetAxis("Vertical") * sensitivityY * Time.deltaTime;
        vertRotate = Mathf.Clamp(vertRotate, -90, -5);
        transform.rotation = Quaternion.Euler(vertRotate, horizRotate, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fire!");
            Instantiate(launchObject, spawnTransform);
        }
    }
}