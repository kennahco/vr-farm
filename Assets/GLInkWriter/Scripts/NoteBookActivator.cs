using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBookActivator : MonoBehaviour {
    
    [Tooltip("The axis of the normal to the plane. Blue is Z, Red is X and Green is Y. The normal is the axis that creates a perpendicular with the plane")]
    public NormalDirection normalDirection = NormalDirection.Z;
    float yPos = 0, xPos = 0, zPos = 0, minY = 0, maxY = 0, minX = 0, maxX = 0, minZ = 0, maxZ = 0;

    public GameObject surface;
    
    // Use this for initialization
    void Start () {
        SetBounds();
    }

    void SetBounds()
    {
        Vector3 boxCollider = GetComponent<BoxCollider>().size;

        maxY = boxCollider.y / 2;
        minY = -maxY;
        maxX = boxCollider.x / 2;
        minX = -maxX;
        maxZ = boxCollider.z / 2;
        minZ = -maxZ;

        switch (normalDirection)
        {
            case NormalDirection.X:
                xPos = surface.transform.localPosition.x;
                break;
            case NormalDirection.Y:
                yPos = surface.transform.localPosition.y;
                break;
            case NormalDirection.Z:
                zPos = surface.transform.localPosition.z;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PenPoint")
        {
            WritingPen pen = other.gameObject.GetComponentInParent<WritingPen>();
            if(pen != null)
            {
                CreateNewLineRenderer(pen);
                SetValues(pen);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PenPoint")
        {
            WritingPen pen = other.gameObject.GetComponentInParent<WritingPen>();
            if (pen != null)
            {
                pen.active = false;
            }
        }
    }

    public void SetValues(WritingPen pen)
    {
        pen.xPos = xPos;
        pen.yPos = yPos;
        pen.zPos = zPos;

        pen.minX = minX;
        pen.maxX = maxX;
        pen.minY = minY;
        pen.maxY = maxY;
        pen.minZ = minZ;
        pen.maxZ = maxZ;
        pen.active = true;
    }
    public void CreateNewLineRenderer(WritingPen writingPen)
    {
        GameObject inkInstance = Instantiate(writingPen.inkPrefab, transform);
        LineRenderer lineRendererInk = inkInstance.GetComponent<LineRenderer>();
        lineRendererInk.material = writingPen.penColor;
        writingPen.lineRenderer = lineRendererInk;
    }
}

public enum NormalDirection
{
    X,
    Y,
    Z
}
