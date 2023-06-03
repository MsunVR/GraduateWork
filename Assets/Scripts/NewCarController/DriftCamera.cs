using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftCamera : MonoBehaviour
{
    [SerializeField]
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float rotationDamping = 3f;
    public float heightDamping = 2f;

    void LateUpdate()
    {
        if (!target)
            return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0f, currentRotationAngle, 0f);

        Vector3 position = target.position - currentRotation * Vector3.forward * distance;
        position.y = currentHeight;

        transform.position = position;
        transform.LookAt(target);
    }
}
