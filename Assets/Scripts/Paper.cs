using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public bool stapled = false;
    bool selected = false;

    Renderer _renderer;
    Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        defaultColor = _renderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public bool IsSelected()
    {
        return selected;
    }

    public void ToggleSelect(bool val)
    {
        if (val) _renderer.material.color = Color.red;
        else _renderer.material.color = defaultColor;

        selected = val;
    }
}
