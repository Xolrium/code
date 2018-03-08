using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaterDeformer : MonoBehaviour
{
    #region Inspector Fields
    public float springForce = 20f;

    public float damping = 5f;
    #endregion

    #region Fields
    /// <summary>
    /// Mesh that will be deformed.
    /// </summary>
    private Mesh _waterMesh;

    /// <summary>
    /// Original vertices of the mesh.
    /// </summary>
    private Vector3[] _originalVertices;

    /// <summary>
    /// Displaced vertices of the mesh.
    /// </summary>
    private Vector3[] _displacedVertices;

    /// <summary>
    /// Velocity the vertices will be moved upon.
    /// </summary>
    private Vector3[] _vertexVelocities;

    /// <summary>
    /// Basic scaling number.
    /// </summary>
    private float _uniformScale = 1f;
    #endregion

    #region Life Cycle
    private void Start()
    {
        _waterMesh = GetComponent<MeshFilter>().mesh;
        _originalVertices = _waterMesh.vertices;
        _displacedVertices = new Vector3[_originalVertices.Length];
        for (int i = 0; i < _originalVertices.Length; i++)
        {
            _displacedVertices[i] = _originalVertices[i];
        }
        _vertexVelocities = new Vector3[_originalVertices.Length];
    }

    private void Update()
    {
        _uniformScale = transform.localScale.x;
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            UpdateVertex(i);
        }
        _waterMesh.vertices = _displacedVertices;
        _waterMesh.RecalculateNormals();
    }
    #endregion

    #region Methods
    /// <summary>
    /// Add the deformation force to the mesh.
    /// </summary>
    /// <param name="_point">Get the Vector3 from the input.</param>
    /// <param name="_force">Get the force from input.</param>
    public void AddDeformForce(Vector3 _point, float _force)
    {
        //Debug.DrawLine(Camera.main.transform.position, _point, Color.red);
        _point = transform.InverseTransformPoint(_point);
        for (int i = 0; i < _displacedVertices.Length; i++)
        {
            AddForceToVertex(i, _point, _force);
        }
    }

    private void AddForceToVertex(int _i, Vector3 _point, float _force)
    {
        Vector3 _pointToVertex = _displacedVertices[_i] - _point;
        _pointToVertex *= _uniformScale;
        float attenuatedForce = _force / (1f + _pointToVertex.sqrMagnitude);
        float _velocity = attenuatedForce * Time.deltaTime;
        _vertexVelocities[_i] += _pointToVertex.normalized * _velocity;
    }

    private void UpdateVertex(int _i)
    {
        Vector3 _velocity = _vertexVelocities[_i];
        Vector3 _displacement = _displacedVertices[_i] - _originalVertices[_i];
        _displacement *= _uniformScale;
        _velocity -= _displacement * springForce * Time.deltaTime;
        _velocity *= 1f - damping * Time.deltaTime;
        _vertexVelocities[_i] = _velocity;
        _displacedVertices[_i] += _velocity * (Time.deltaTime / _uniformScale);
    }
    #endregion
}