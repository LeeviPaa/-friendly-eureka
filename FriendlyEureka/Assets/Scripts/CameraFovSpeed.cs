using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraFovSpeed : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _targetRigid;
    [SerializeField]
    private float _fovLerpSpeed;
    [SerializeField]
    private float _fovVelocityValue;
    [SerializeField]
    private float _fovMax = 120;
    private float _fovStart = 80;
    private CinemachineVirtualCamera _camera;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _fovStart = _camera.m_Lens.FieldOfView;
    }

    private void Update()
    {
        float targetFov = Mathf.Clamp(_targetRigid.velocity.magnitude * _fovVelocityValue, _fovStart, _fovMax);
        var lens = _camera.m_Lens;
        lens.FieldOfView = Mathf.Lerp(_camera.m_Lens.FieldOfView, targetFov, _fovLerpSpeed * Time.deltaTime);
        _camera.m_Lens = lens;
    }
}
