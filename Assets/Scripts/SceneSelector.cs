using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public List<Scene> scenes = new List<Scene>();
    public TextMeshProUGUI name;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        SelectByIndex(0);
    }

    void SelectByIndex(int index)
    {
        if(name != null)
        name.text = scenes[index].name;
    }

    public void LoadSelected()
    {
        SceneManager.LoadScene(scenes[index].index);
    }

    public void SelectLeft()
    {
        index--;
        if (index < 0)
        {
            index = scenes.Count - 1;
        }
        SelectByIndex(index);
    }

    public void SelectRight()
    {
        index++;
        if(index >= scenes.Count)
        {
            index = 0;
        }
        SelectByIndex(index);
    }

    [System.Serializable]
    public class Scene 
    {
        public string name;
        public int index;
    }
}
