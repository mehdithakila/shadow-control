using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigerDoorController : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    private bool isRotating = true;
    [Header("Rotation Configs")]
    [SerializeField]
    private float rotAmount = 100f;
    [SerializeField]
    private float fowardDir = 0f;

    private Vector3 StartRot;
    private Vector3 forward;

    private Coroutine animationCoroutine;

    private void Awake()
    {
        StartRot = transform.rotation.eulerAngles;
        forward = transform.right;
    }

    public void Open(Vector3 userPos)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            if (isRotating)
            {
                float dot = Vector3.Dot(forward, (userPos - transform.position).normalized);
                Debug.Log($"Dot: {dot.ToString("N3")}");
                animationCoroutine = StartCoroutine(DoRotOpen(dot));
            }

        }
    }

    public IEnumerator DoRotOpen(float dot)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot;

        if (dot >= fowardDir)
        {
            endRot = Quaternion.Euler(new Vector3(0, startRot.y - rotAmount, 0));
        }
        else
        {
            endRot = Quaternion.Euler(new Vector3(0, startRot.y + rotAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot,time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }


    public void Close()
    {
        if (isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            if (isRotating)
            {
                animationCoroutine = StartCoroutine(DoRotClose());
            }

        }
    }

    public IEnumerator DoRotClose()
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(StartRot);

        isOpen = false;

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }
    }


}

