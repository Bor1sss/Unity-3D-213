using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private InputAction _lookAction;
    
    private Vector3 _c;
    [SerializeField]
    private Transform _fpvTransform;

    private bool _fpv = true;
    private float _mX, _mY;
    private float _sensitivityH = 5, _sensitivityV = 5, _sensitivityW = 0.1f;

    private float _fpvRange = 1.2f;
    private float _maxDistance = 10f;
    private float _minDistance = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lookAction = InputSystem.actions.FindAction("Look");
       _c = this.transform.position - _player.transform.position;
       _mX = this.transform.eulerAngles.y;
       _mY = this.transform.eulerAngles.x;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_fpv)
        {
            Vector2 mouseWheel = Input.mouseScrollDelta;
            if (mouseWheel.y != 0)
            {
                float newMagnitude = _c.magnitude * (1 - mouseWheel.y * _sensitivityW);
                if (newMagnitude > _minDistance && newMagnitude < _maxDistance)
                {
                    _c = _c * (1 - mouseWheel.y * _sensitivityW);
                }
                else if (newMagnitude <= _minDistance)
                {
                    _c = _c.normalized * _minDistance;
                }
                else if (newMagnitude >= _maxDistance)
                {
                    _c = _c.normalized * _maxDistance;
                }
            }

            var lookValue = _lookAction.ReadValue<Vector2>() * Time.deltaTime;
            _mX += lookValue.x * _sensitivityH;
            var my = -lookValue.y * _sensitivityV;

            if (0 <= _mY + my && _mY + my <= 75)
            {
                _mY += my;
            }

            this.transform.eulerAngles = new Vector3(_mY, _mX, 0);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _fpv = !_fpv;
            if (!_fpv)
            {
                this.transform.position = _fpvTransform.position;
                this.transform.rotation = _fpvTransform.rotation;
            }
        }
    }

    void LateUpdate()
    {
        if (_fpv)
        {
            this.transform.position = Quaternion.Euler(0,_mX,0) * _c + _player.transform.position;
            
        }
    }
}
