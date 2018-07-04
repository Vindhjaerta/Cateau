using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceProfile : MonoBehaviour
{
    private List<AmbienceContainer> _ambienceContainer = new List<AmbienceContainer>();
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<AmbienceContainer>())
            {
                _ambienceContainer.Add(child.GetComponent<AmbienceContainer>());
            }
        }
        if (_ambienceContainer != null)
        {
            foreach (AmbienceContainer ambienceContainer in _ambienceContainer)
            {
                ambienceContainer.gameObject.SetActive(false);
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Stop()
    {
        if (_ambienceContainer != null)
        {
            foreach (AmbienceContainer ambienceContainer in _ambienceContainer)
            {
                ambienceContainer.gameObject.SetActive(false);
            }
        }
    }

    public void Play()
    {
        if (_ambienceContainer != null)
        {
            foreach (AmbienceContainer ambienceContainer in _ambienceContainer)
            {
                ambienceContainer.gameObject.SetActive(true);
            }
        }
    }

}
