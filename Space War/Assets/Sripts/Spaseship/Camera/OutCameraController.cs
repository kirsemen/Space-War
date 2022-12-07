using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutCameraController : MonoBehaviour
{

    public GameObject Mesh;
    public Parametrs parametrs;

    public float CameraSoftRotation = 0.03f;

    public KeyCode keyFreeRotation = KeyCode.LeftAlt;

    private float size = 1;
    private float CuretSize = 1;

    public float SpeedSizing = 0.1f;
    public float MinSize = 0.5f;
    public float MaxSize = 100;
    public float SizeSoftSizing = 0.01f;

    public LayerMask EnemyMasks;

    private Vector2 _mousePosition = new Vector2(0, 0);
    private bool _mouseIsDown = false;

    private Vector3 CameraAngle = new Vector3(0, 0, 0);
    private Vector3 CameraOldAngle = new Vector3(0, 0, 0);
    private Vector3 _CameraCurentAngle = new Vector3(0, 0, 0);
    private Vector3 _MeshCurentAngle = new Vector3(0, 0, 0);

    private void Start()
    {
        CameraAngle = transform.parent.parent.rotation.eulerAngles;
        _CameraCurentAngle = transform.parent.parent.rotation.eulerAngles;
        _MeshCurentAngle = transform.parent.parent.rotation.eulerAngles;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.GetChild(0).position, transform.GetChild(0).forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, float.MaxValue, EnemyMasks))
            {
                parametrs.target = hit.collider.transform.parent.GetComponent<Parametrs>();
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            _mouseIsDown = true;
            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
            _mouseIsDown = false;

        if (_mouseIsDown)
        {
            Vector2 mouseDelta = _mousePosition - new Vector2(Input.mousePosition.x, Input.mousePosition.y);


            CameraAngle = new Vector3(mouseDelta.y / 10, -mouseDelta.x / 10, 0) + CameraAngle;

            _mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        var deltaCamAngle = (CameraAngle - _CameraCurentAngle) * CameraSoftRotation;
        _CameraCurentAngle += deltaCamAngle;
        transform.rotation = Quaternion.Euler(deltaCamAngle + _CameraCurentAngle);
        if (Input.GetKeyDown(keyFreeRotation))
            CameraOldAngle = CameraAngle;
        if (Input.GetKeyUp(keyFreeRotation))
            CameraAngle = CameraOldAngle;

        if (parametrs.rotationSpeed != 0)
        {
            Vector3 deltaMeshAngle;
            if (!Input.GetKey(keyFreeRotation))
                deltaMeshAngle = (CameraAngle - _MeshCurentAngle);
            else
                deltaMeshAngle = (CameraOldAngle - _MeshCurentAngle);


            float x = Mathf.Abs(deltaMeshAngle.x) < parametrs.rotationSpeed ? deltaMeshAngle.x : parametrs.rotationSpeed * (Mathf.Abs(deltaMeshAngle.x) / deltaMeshAngle.x);
            float y = Mathf.Abs(deltaMeshAngle.y) < parametrs.rotationSpeed ? deltaMeshAngle.y : parametrs.rotationSpeed * (Mathf.Abs(deltaMeshAngle.y) / deltaMeshAngle.y);

            _MeshCurentAngle = Vector3.Lerp(_MeshCurentAngle, _MeshCurentAngle + new Vector3(x, y, 0), 0.1f);
            Mesh.transform.rotation = Quaternion.Euler(_MeshCurentAngle);
        }




        float mw = Input.GetAxis("Mouse ScrollWheel");

        size += mw * -10 * SpeedSizing * size;


        if (size < MinSize)
            size = MinSize;
        else if (size > MaxSize)
            size = MaxSize;
        CuretSize += (size - CuretSize) * SizeSoftSizing;
        transform.localScale = new Vector3(1, 1, CuretSize);

    }
}
