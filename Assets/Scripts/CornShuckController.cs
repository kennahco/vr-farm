using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornShuckController : MonoBehaviour
{
    public Grabbable baseCob;
    public List<Grabbable> piecesInOrder = new List<Grabbable>();

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < piecesInOrder.Count; i++)
        {
            piecesInOrder[i].enabled = i == 0 && baseCob.GetPrimaryGrabber() != null;
        }
        piecesInOrder.RemoveAll(piece => piece.GetPrimaryGrabber() != null);
    }
}
