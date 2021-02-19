using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField] float _sensitivityX = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += mouseX * _sensitivityX;

        transform.localEulerAngles = newRotation;
    }
}
