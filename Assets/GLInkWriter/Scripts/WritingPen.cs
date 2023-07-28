using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WritingPen : MonoBehaviour {

    [HideInInspector]
    public LineRenderer lineRenderer;
    [HideInInspector]
    public bool active = false;
    public Material penColor;
    public GameObject inkPrefab;
    public Transform point;
    [HideInInspector]
    public float yPos,xPos,zPos,minY,maxY,minX,maxX,minZ,maxZ;
    
	// Update is called once per frame
	void Update () {
		if(active)
        {
            float tempY = yPos;
            float tempX = xPos;
            float tempZ = zPos;
            
            if(tempX == 0)
            {
                tempX = Mathf.Clamp(lineRenderer.gameObject.transform.InverseTransformPoint(point.position).x, minX, maxX);
            }
            if(tempY == 0)
            {
                tempY = Mathf.Clamp(lineRenderer.gameObject.transform.InverseTransformPoint(point.position).y, minY, maxY);
            }
            if (tempZ == 0)
            {
                tempZ = Mathf.Clamp(lineRenderer.gameObject.transform.InverseTransformPoint(point.position).z, minZ, maxZ);
            }
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount-1, new Vector3(tempX, tempY, tempZ));
        }
	}
}
