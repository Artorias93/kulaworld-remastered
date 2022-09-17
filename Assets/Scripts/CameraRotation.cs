using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public GameObject targetObject;
    public float xOffset, yOffset, zOffset;
    private float targetAngle = 0;
    const float rotationAmount = 1f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;
    bool go;

    private void Start()
    {
        go = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetObject.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(targetObject.transform.position);

        if (!go)
        {
            return;
        }

        // Trigger functions if Rotate is requested
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetAngle += 90.0f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetAngle -= 90.0f;
        }

        if (targetAngle != 0)
        {
            Rotate();
        }
    }

    protected void Rotate()
    {

        float step = rSpeed * Time.deltaTime;
        float orbitCircumfrance = 2F * rDistance * Mathf.PI;
        float distanceDegrees = (rSpeed / orbitCircumfrance) * 360;
        float distanceRadians = (rSpeed / orbitCircumfrance) * 2 * Mathf.PI;

        if (targetAngle > 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, -rotationAmount);
            targetAngle -= rotationAmount;
        }
        else if (targetAngle < 0)
        {
            transform.RotateAround(targetObject.transform.position, Vector3.up, rotationAmount);
            targetAngle += rotationAmount;
        }

    }
    
    public void stopRot()
    {
        go = false;
    }
}
