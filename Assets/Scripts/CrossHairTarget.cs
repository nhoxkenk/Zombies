using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    Camera m_Camera;
    Ray ray;
    RaycastHit m_RaycastHit;

    private void Start()
    {
        m_Camera = Camera.main;
    }

    private void Update()
    {
        ray.origin = m_Camera.transform.position;
        ray.direction = m_Camera.transform.forward;
        Physics.Raycast(ray, out m_RaycastHit);
        transform.position = m_RaycastHit.point;
    }
}
