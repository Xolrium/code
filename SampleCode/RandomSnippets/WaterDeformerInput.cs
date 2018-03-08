using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeformerInput : MonoBehaviour
{
    #region Fields
    /// <summary>
    /// Force of the deforming proces.
    /// </summary>
    private float _force = 10f;

    /// <summary>
    /// Force offset for the deforming proces.
    /// </summary>
    private float _forceOffset = 0.1f;
    #endregion

    #region Life Cycle
    private void Update()
    {
        if (Input.GetKey(KeyCode.F7))
        {
            HandleInput();
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Handles the input of the force.
    /// </summary>
    private void HandleInput()
    {
        //Prepare raycast based on mouse
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        //if raycast hits and collides with WaterDeformer add force to that point
        if (Physics.Raycast(inputRay, out _hit))
        {
            WaterDeformer _waterDeformer = _hit.collider.GetComponent<WaterDeformer>();
            if (_waterDeformer)
            {
                Vector3 _point = _hit.point;
                _point += _hit.normal * _forceOffset;
                _waterDeformer.AddDeformForce(_point, _force);
            }
        }
    }
    #endregion
}