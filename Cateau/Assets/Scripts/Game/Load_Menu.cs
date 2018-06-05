using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Menu : MonoBehaviour
{
    [SerializeField]
    private string nameOfNextScene = "Menu";


    private static Load_Menu _instance;
    public static Load_Menu Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(nameOfNextScene);
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(nameOfNextScene);
    }
}
