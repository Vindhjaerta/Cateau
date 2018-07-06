using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReactionPackage : System.Object
{
    public StringVariable characterIdentifier;
    public string characterReaction;
    public BuppData buppData;
    public bool reactionDone;
}

[System.Serializable]
public class ChangeAnimationLayerData : System.Object
{
    public StringVariable characterIdentifier;
    public int animationIndex;
}

public class CharacterController : MonoBehaviour
{
    [HideInInspector]
    public List<AnimatedCharacter> animatedCharacters;

    private static CharacterController _instance;

    public static CharacterController Instance
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
		
	}

    public void DelegateReaction(ReactionPackage reactionPackage)
    {
        if (animatedCharacters != null && reactionPackage != null)
        {
            foreach (AnimatedCharacter animatedCharacter in animatedCharacters)
            {
                if (animatedCharacter.characterIdentifier.value == reactionPackage.characterIdentifier.value)
                {
                    if (animatedCharacter.gameObject.activeSelf == true)
                    {
                        animatedCharacter.ReceiveReaction(reactionPackage);
                    }
                }
            }
        }
    }

    public void DelegateAnimationLayerChange(ChangeAnimationLayerData changeAnimationLayerData)
    {
        if (animatedCharacters != null)
        {
            foreach (AnimatedCharacter animatedCharacter in animatedCharacters)
            {
                if (animatedCharacter.characterIdentifier.value == changeAnimationLayerData.characterIdentifier.value)
                {
                    if (animatedCharacter.gameObject.activeSelf == true)
                    {
                        animatedCharacter.ChangeAnimationLayer(changeAnimationLayerData.animationIndex);
                    }
                }
            }
        }
    }
}
