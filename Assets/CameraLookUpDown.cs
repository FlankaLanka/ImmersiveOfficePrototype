using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookUpDown : MonoBehaviour
{
    private void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.E))
            LookUp();

        if(Input.GetKey(KeyCode.C))
            LookDown();
    }

    private void LookUp()
    {
        if (transform.rotation.x <= -90)
            return;

        Vector3 rotateValue = new Vector3(0.5f, 0, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }

    private void LookDown()
    {
        if (transform.rotation.x >= 90)
            return;

        Vector3 rotateValue = new Vector3(0.5f, 0, 0);
        transform.eulerAngles = transform.eulerAngles + rotateValue;
    }
}
