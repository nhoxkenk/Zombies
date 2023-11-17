using Cinemachine;
using System.Collections;
using UnityEngine;

public class CamRotationCinemachineExtension : CinemachineExtension
{
    private CinemachineFreeLook _vCam;
    private float _targetRotation;
    private bool _isRotating = false;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        _vCam = (CinemachineFreeLook)VirtualCamera;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !_isRotating)
        {
            StartRotation();
        }
    }

    private void StartRotation()
    {
        //_vCam.m_XAxis.Value += 180f;
        _targetRotation = _vCam.m_XAxis.Value + 180f; // Thiết lập giá trị quay mới
        StartCoroutine(RotateCamera());
    }

    private IEnumerator RotateCamera()
    {
        _isRotating = true;

        float initialRotation = _vCam.m_XAxis.Value;
        float elapsedTime = 0f;

        while (elapsedTime < 0.1f) // Thời gian để hoàn thành quay
        {
            elapsedTime += Time.deltaTime;
            float currentRotation = Mathf.Lerp(initialRotation, _targetRotation, elapsedTime / 0.1f); // Tính toán giá trị quay hiện tại

            _vCam.m_XAxis.Value = currentRotation; // Đặt giá trị quay cho trục X

            yield return null;
        }

        _vCam.m_XAxis.Value = _targetRotation; // Đảm bảo giá trị cuối cùng là giá trị đích
        _isRotating = false;
    }
}
