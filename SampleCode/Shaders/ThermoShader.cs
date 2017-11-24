using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermoShader : MonoBehaviour {

    public Renderer rend;
    float timer = 3f;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/Thermo");
    }

    void Update()
    {
        float shine = Mathf.PingPong(Time.time - 5, 2.5f);
        rend.material.SetFloat("_Float", shine);
    }
}
