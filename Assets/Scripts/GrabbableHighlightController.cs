using BNG;
using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableHighlightController : MonoBehaviour
{
    Grabbable grabbable;
    HighlightEffect highlight;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<Grabbable>();
        highlight = GetComponent<HighlightEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        highlight.highlighted = grabbable.IsGrabbable() && grabbable.GetClosestGrabber() != null;
    }
}
