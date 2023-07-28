using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotspotController : MonoBehaviour
{
    public GameObject content;
    public GameObject button;
    public AudioSource narrationSource;
    public static List<HotspotController> activeHotspots = new List<HotspotController>();

    private void Awake()
    {
        activeHotspots.Add(this);
    }

    private void OnDestroy()
    {
        activeHotspots.Remove(this);
    }

    public static void CloseAllHotspots()
    {
        foreach (var item in activeHotspots)
        {
            item.Close();
        }
    }

    public static void ToggleAllHotspots()
    {
        foreach (var item in activeHotspots)
        {
            item.gameObject.SetActive(!item.gameObject.activeSelf);
        }
    }

    public void Open()
    {
        button.SetActive(false);
        content.SetActive(true);

        if(narrationSource != null)
        {
            narrationSource.time = 0;
            narrationSource.Play();
        }
    }

    public void Close()
    {
        content.SetActive(false);
        button.SetActive(true);
    }
}
