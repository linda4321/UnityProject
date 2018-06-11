using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogWindow : MonoBehaviour {

    public Button backgroundButton;
    public Button closeButton;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void OnCloseButton()
    {
        Debug.Log("Hey");
        Destroy(this.gameObject);
        PlayWorld();
    }

    public virtual DialogWindow Show(GameObject parent)
    {
        GameObject dialog = Instantiate(this.gameObject) as GameObject;
        dialog.transform.SetParent(parent.transform, false);
        PauseWorld();
        return dialog.GetComponent<DialogWindow>();
    }

    public void PauseWorld()
    {
        Time.timeScale = 0;
    }

    public void PlayWorld()
    {
        Time.timeScale = 1;
    }
}
