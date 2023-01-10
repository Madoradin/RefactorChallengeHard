using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 7;
    [SerializeField] private float cameraRotateSpeed = 45;
    [SerializeField] private float minCameraXRotation = 10;
    [SerializeField] private float maxCameraXRotation = 35;

    [SerializeField] private Transform cameraTransform;


    public void Move()
    {
        var movementDirection =
            Input.GetAxis("Horizontal") * transform.right +
            Input.GetAxis("Vertical") * transform.forward;

        transform.position += movementDirection.normalized * Time.deltaTime * movementSpeed;
    }

    public void RotateBody()
    {
        var rotationDelta = Input.GetAxis("Mouse X") * cameraRotateSpeed * Time.deltaTime;
        var newYRotation = transform.rotation.eulerAngles.y + rotationDelta;

        transform.localRotation = Quaternion.Euler(0, newYRotation, 0);
    }

    public void RotateCamera()
    {
        var rotationDelta = Input.GetAxis("Mouse Y") * cameraRotateSpeed * Time.deltaTime;
        var newXRotation = cameraTransform.localRotation.eulerAngles.x - rotationDelta;
        newXRotation = Mathf.Clamp(newXRotation, minCameraXRotation, maxCameraXRotation);

        cameraTransform.localRotation = Quaternion.Euler(newXRotation, 0, 0);
    }
}
