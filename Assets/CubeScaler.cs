using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeScaler : MonoBehaviour
{
    [SerializeField] Transform cubeTransform;
    [SerializeField] TMP_InputField lengthInput;
    [SerializeField] TMP_InputField widthInput;
    [SerializeField] TMP_InputField heightInput;


    private void OnEnable()
    {
        lengthInput.onValueChanged.AddListener(ResizeCube);
        widthInput.onValueChanged.AddListener(ResizeCube);
        heightInput.onValueChanged.AddListener(ResizeCube);
    }

    private void OnDisable()
    {
        lengthInput.onValueChanged.RemoveListener(ResizeCube);
        widthInput.onValueChanged.RemoveListener(ResizeCube);
        heightInput.onValueChanged.RemoveListener(ResizeCube);
    }


    void ResizeCube(string data)
    {
        if (!float.TryParse(lengthInput.text, out float length))
            return;

        if (!float.TryParse(widthInput.text, out float width))
            return;

        if (!float.TryParse(heightInput.text, out float height))
            return;

        cubeTransform.localScale = new Vector3(length, height, width);

        cubeTransform.position = new Vector3(length/2, height/2, width/2);

    }

}
