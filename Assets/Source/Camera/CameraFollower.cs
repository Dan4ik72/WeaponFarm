using UnityEngine;

public enum VectorDirection
{
    Forward,
    Right,
    Up
}

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _playerTansform;
    [SerializeField] private float _height = 9f;
    [SerializeField] private float _angle = -130f;
    [SerializeField] private VectorDirection _vectorAxis;

    private Vector3 _axis;

    private void Start()
    {
        switch (_vectorAxis)
        {
            case VectorDirection.Forward: _axis = Vector3.forward; break;
            case VectorDirection.Right: _axis = Vector3.right; break;
            case VectorDirection.Up: _axis = Vector3.up; break;
        }
    }

    private void Update()
    {
        float radians = _angle * Mathf.Deg2Rad;

        Matrix4x4 rotationMatrix = Matrix4x4.identity;
        rotationMatrix.SetRow(0, new Vector4(1, 0, 0, 0));
        rotationMatrix.SetRow(1, new Vector4(0, Mathf.Cos(radians), -Mathf.Sin(radians), 0));
        rotationMatrix.SetRow(2, new Vector4(0, Mathf.Sin(radians), Mathf.Cos(radians), 0));

        Vector4 rotatedVector = rotationMatrix.MultiplyPoint(new Vector4(_axis.x, _axis.y, _axis.z, 1.0f));
        Vector3 rotatedVector3 = new Vector3(rotatedVector.x, rotatedVector.y, rotatedVector.z) * _height;
        Vector3 newPosition = _playerTansform.position + rotatedVector3;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(-rotatedVector3);
    }
}
