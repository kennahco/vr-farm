using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public Camera PortalCamera;
    public GameObject display; 
    public static List<PortalController> activePortals = new List<PortalController>();

    private Camera camera;

    private void Awake()
    {
        activePortals.Add(this);
    }

    private void OnDestroy()
    {
        activePortals.Remove(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        PortalCamera.transform.rotation = this.transform.rotation;
        
        PortalCamera.fieldOfView = 120 - 30 * Mathf.Log10(Vector3.Distance(camera.transform.position, this.transform.position)+1);
        //PortalCamera.transform.localPosition = camera.transform.position - this.transform.position;
    }

    public static void ToggleAllPortals()
    {
        foreach (var item in activePortals)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }
    }
}
