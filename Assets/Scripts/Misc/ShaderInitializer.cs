using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderInitializer : MonoBehaviour
{
    public Material _mat;
    public Texture2D _mainTexture;
    public float _vibrationIntensity = 0.85f;
    public Color _borderColor = Color.black;
    public float _borderThickness = 0.0025f;

    void Start()
    {
        _mat.SetTexture("Texture2D_3c01c3ce242341a781b39a6abb8e7cab", _mainTexture);
        _mat.SetFloat("Vector1_61d4bf80d40f40efb005708b991807fa", _vibrationIntensity);
        _mat.SetColor("Color_4a10e2379d244746a98ee648a335658d", _borderColor);
        _mat.SetFloat("Vector1_aad1dfb20c0d4c9f811619471f7664b4", _borderThickness);
    }
}
