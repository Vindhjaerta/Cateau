using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
    Author: Jesper Kanewoff
    Last updated: 2018-01-30

    Purpose:
    Ment to send information about cat reactions but also provide the user with some choices during a game session. 

    Usage: 
    Used by adding information and purpose for the specific buttons. Can take an infinate number of button choices. 
 
    Dependencies:
    Unity, SceneTreeObjects
    (Dependant on there being a reciever somewhere in a parent structure to show the data)

*/




public enum ECatSoundReaction { NoReaction, RelativeReaction, FullReaction};
public enum EButtonChoice { conversationButton, catButton, stringSaveButton };
public enum ECatReaction { NoReaction, Negative, Neutral, Positive };
[System.Serializable]
public class ConversationButton : System.Object
{
    public string buttonText;
    public SceneTreeObject nextNode;
}

[System.Serializable]
public class CatButton : System.Object
{
    public enum ECatReaction {NoReaction, Negative, Neutral, Positive };
    public string buttonText;
    public ECatReaction catReaction;
    public bool relativeReaction;
    public ECatSoundReaction catSoundReaction;
    public int affinity;
    public SceneTreeObject nextNode;
}

[System.Serializable]
public class StringSaveButton : System.Object
{
    public string buttonText;
    public string stringToSave;
    public SceneTreeObject nextNode;
}

[System.Serializable]
public class ChoiceButton : System.Object
{
    public EmojiType emojiType;
    public string soundContainer;
    public EButtonChoice buttonFunction;
    public ConversationButton conversationButton;
    public CatButton catButton;
    public StringSaveButton stringSaveButton;
}

//Information to send
public class ButtonData : SceneTreeData
{
    public EButtonChoice buttonFunction;
    public string buttonText;

    public ButtonData(EButtonChoice inButtonFunction, string inButtonText)
    {
        this.buttonFunction = inButtonFunction;
        this.buttonText = inButtonText;
    }

    public int reactValue;
    public int affinity;
    public bool react;
    public bool relativeReaction;
    public ECatSoundReaction catSoundReaction;

    public ButtonData(EButtonChoice inButtonFunction, string inButtonText, int inReactValue, int inAffinity, bool inReact, bool relativeReaction, ECatSoundReaction catSoundReaction)
    {
        this.buttonFunction = inButtonFunction;
        this.buttonText = inButtonText;
        this.reactValue = inReactValue;
        this.affinity = inAffinity;
        this.react = inReact;
        this.relativeReaction = relativeReaction;
        this.catSoundReaction = catSoundReaction;
    }
}
//List of the buttons information
public class ButtonDataList : SceneTreeData
{
    public List<ButtonData> listOfButtonData;
    public ButtonDataList() 
    {
        listOfButtonData = new List<ButtonData>();
    }
    public void Add(ButtonData buttonData) 
    {
        this.listOfButtonData.Add(buttonData);
    }
}


public class Choice : SceneTreeObject
{

    [SerializeField]
    public ChoiceButton[] buttons;

    private bool _readyToSend;

    public override void Continue(int nodeIndex)
    {
        if (buttons[nodeIndex].emojiType != null)
        {
            ExecuteEvents.Execute<IEmoji>(GameController.Instance.gameObject, null, (handler, data) => handler.OnEmoji(buttons[nodeIndex].emojiType));
        }
        if (buttons[nodeIndex].soundContainer != null)
        {
            if (SoundEffectsManager.Instance != null)
            {
                SoundEffectsManager.Instance.PlaySoundFromContainer(buttons[nodeIndex].soundContainer);
            }
            else
            {
                Debug.Log("SoundeffectsManager wasn't found");
            }
        }


        if (buttons[nodeIndex].buttonFunction == EButtonChoice.conversationButton)
        {
            if (buttons[nodeIndex].conversationButton.nextNode != null)
            {
                targetNode = buttons[nodeIndex].conversationButton.nextNode;
            }
            else
            {
                Debug.LogWarning(gameObject + " " + buttons[nodeIndex].buttonFunction + " number " + nodeIndex + " didn't have an assigned NextNode, the default at the top of the script was used");
                if (targetNode == null)
                {
                    Debug.LogWarning(gameObject + "doesn't even have a default nextNode :(");
                }
            }
        }
        else if (buttons[nodeIndex].buttonFunction == EButtonChoice.catButton)
        {
            if (buttons[nodeIndex].catButton.nextNode != null)
            {
                targetNode = buttons[nodeIndex].catButton.nextNode;
            }
            else
            {
                Debug.LogWarning(gameObject + " " + buttons[nodeIndex].buttonFunction + " number " + nodeIndex + " didn't have an assigned NextNode, the default at the top of the script was used");
                if (targetNode == null)
                {
                    Debug.LogWarning(gameObject + " doesn't even have a default nextNode :(");
                }
            }
        }
        else if (buttons[nodeIndex].buttonFunction == EButtonChoice.stringSaveButton)
        {
            //Save to GameStateContainer
            GameStateContainer.Instance.savableStrings.Add(buttons[nodeIndex].stringSaveButton.stringToSave);
            if (buttons[nodeIndex].stringSaveButton.nextNode != null)
            {
                targetNode = buttons[nodeIndex].stringSaveButton.nextNode;
            }
            else
            {
                Debug.LogWarning(gameObject + " " + buttons[nodeIndex].buttonFunction + " number " + nodeIndex + " didn't have an assigned NextNode, the default at the top of the script was used");
                if (targetNode == null)
                {
                    Debug.LogWarning(gameObject + " doesn't even have a default nextNode :(");
                }
            }
        }
        Continue();
    }

    private void InternalContinue()
    {
        Continue();
    }


    protected override void Initialize()
    {
        ButtonDataList _buttonDataList = new ButtonDataList();
        if (buttons.Length == 0)
        {
            Debug.LogWarning(gameObject + " doesn't contain any choices! Script used the default NextNode on the top of the choiceScript");
            if (targetNode == null)
            {
                Debug.LogWarning(gameObject + "doesn't even have a default nextNode :(");
            }
                InternalContinue();
        }
        else
        { 
            for (int i = 0; i < buttons.Length; i++)
            {
    
                if (buttons[i].buttonFunction == EButtonChoice.conversationButton)
                {
                    EButtonChoice buttonFunctionToSend = buttons[i].buttonFunction;
                    string buttonTextToSend = buttons[i].conversationButton.buttonText;
                    _buttonDataList.Add(new ButtonData(buttonFunctionToSend, buttonTextToSend));
                }
                else if (buttons[i].buttonFunction == EButtonChoice.catButton)
                {
                    int reactValue = 0;
                    bool reactToSend = false;
                    bool relativeReactionToSend = buttons[i].catButton.relativeReaction;
                    EButtonChoice buttonFunctionToSend = buttons[i].buttonFunction;
                    string buttonTextToSend = buttons[i].catButton.buttonText;

                    if (buttons[i].catButton.catReaction == CatButton.ECatReaction.Negative)
                    {
                        reactValue = -2;
                        reactToSend = true;
                    }
                    else if (buttons[i].catButton.catReaction == CatButton.ECatReaction.Neutral)
                    {
                        reactValue = 0;
                        reactToSend = true;
                    }
                    else if (buttons[i].catButton.catReaction == CatButton.ECatReaction.Positive)
                    {
                        reactValue = +2;
                        reactToSend = true;
                    }
                    else if (buttons[i].catButton.catReaction == CatButton.ECatReaction.NoReaction)
                    {
                        reactToSend = false;
                    }
                    int affintyToSend = buttons[i].catButton.affinity;
                    int reactValueToSend = reactValue;
                    ECatSoundReaction catSoundReactionToSend = buttons[i].catButton.catSoundReaction;
                    _buttonDataList.Add(new ButtonData(buttonFunctionToSend, buttonTextToSend, reactValueToSend, affintyToSend, reactToSend, relativeReactionToSend, catSoundReactionToSend));
                }
                if (buttons[i].buttonFunction == EButtonChoice.stringSaveButton)
                {
                    EButtonChoice buttonFunctionToSend = buttons[i].buttonFunction;
                    string buttonTextToSend = buttons[i].stringSaveButton.buttonText;
                    _buttonDataList.Add(new ButtonData(buttonFunctionToSend, buttonTextToSend));
                }

                
            }
            _data = _buttonDataList;
            _data.sender = this;
            _data.type = ESceneTreeType.Buttons;
            //Check om inga val

        }
    }
}

