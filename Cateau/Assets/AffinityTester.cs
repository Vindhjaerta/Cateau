using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AffinityTester : MonoBehaviour {

    public GameStateContainer container;
    public StringReference fatCatTag;
    public StringReference bossCatTag;
    public StringReference kittenCatTag;
    private Text text;
    private SpriteCatScript cat;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        cat = FindObjectOfType<SpriteCatScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(container != null)
        {
            text.text = "FatCat: " + container.affinity[fatCatTag.value].ToString() + "\n";
            text.text += "BossCat: " + container.affinity[bossCatTag.value].ToString() + "\n"; 
            text.text += "Kitten: " + container.affinity[kittenCatTag.value].ToString() + "\n";
            if(cat != null)
            {
                text.text += "current: " + cat.SendAffinity().ToString();
            }
        }
	}
}
