using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    private Gyroscope gyro;
    private Quaternion _origin = Quaternion.identity;
    private Rigidbody rb;

    float rotationX;
    float rotationY;
    float rotationZ;

    [SerializeField]
    private float sensitivity = 0.5f;
    [SerializeField]
    private float _maxAngularVelocity = 7f;

    private void Awake()
    {
        gyro = Input.gyro;
        if (!gyro.enabled)
        {
            gyro.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        getOrigin();

        rb = gameObject.GetComponent<Rigidbody>();
        rb.maxAngularVelocity = _maxAngularVelocity;
    }

    private void getOrigin()
    {
        _origin = gyro.attitude;
    }

    private void Update()
    {
        if (_origin == Quaternion.identity) {
            
            _origin = gyro.attitude;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Quaternion rotation = ConvertRightHandtoLeftHandQuaternion(Quaternion.Inverse(_origin) * gyro.attitude);
        //Quaternion rotation = ConvertRightHandtoLeftHandQuaternion(gyro.attitude);

        Quaternion rotE = Quaternion.Euler(rotation.eulerAngles.x, 0, rotation.eulerAngles.z);

        Quaternion rotS = new Quaternion(rotE.x * sensitivity, rotE.y * sensitivity, rotE.z * sensitivity, rotE.w);

        Vector3 currentRot = rotS.eulerAngles;
        if (currentRot.x > 20 && currentRot.x < 180)
        {
            currentRot.x = 20f;
        }
        else if (currentRot.x < 340 && currentRot.x > 180)
        {
            currentRot.x = 340f;
        }

        if (currentRot.z > 20 && currentRot.z < 180)
        {
            currentRot.z = 20f;
        }
        else if (currentRot.z < 340 && currentRot.z > 180)
        {
            currentRot.z = 340f;
        }

        rb.MoveRotation(Quaternion.Euler(currentRot));
        
    }
    

    private Quaternion ConvertRightHandtoLeftHandQuaternion(Quaternion rightQuaternion)
    {
        return new Quaternion(-rightQuaternion.x, -rightQuaternion.z, -rightQuaternion.y, rightQuaternion.w);
    }
}
