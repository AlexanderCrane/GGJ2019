using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChange : MonoBehaviour {

    private Color myColor;
    private Color localColor;
    public Renderer rend;

    // Use this for initialization
    void Start()
    {
        myColor = this.GetComponentInParent<DoorSwitchColor>().baseColor;
        Material mymat = GetComponent<Renderer>().material;
        mymat.SetColor("_EmissionColor", myColor);
    }

    // Update is called once per frame
    void Update()
    {
        myColor = this.GetComponentInParent<DoorSwitchColor>().baseColor;
        Material mymat = GetComponent<Renderer>().material;
        mymat.SetColor("_EmissionColor", myColor);
    }
}
