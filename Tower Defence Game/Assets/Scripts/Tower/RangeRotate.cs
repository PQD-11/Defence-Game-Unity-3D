using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeRotate : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
    }
}
