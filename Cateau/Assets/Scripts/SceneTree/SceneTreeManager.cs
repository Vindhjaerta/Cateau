using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneTreeManager : MonoBehaviour, ISceneTreeData, IButtonData, IDialogue, IInputField {

    private SceneTreeObject _sender;

    private ButtonDataList _buttonDataList;
    private ChoiceController _buttonBox;
    private ConversationController _cC;
    private InputController _inputController;
    private InputField _inputField;

    private string _targetCharacterTagForInput;

    public void ChosenButtonIndex(int index)
    {
        if(_cC != null)
        {
            _cC.Clear();
        }
        if (_buttonDataList.listOfButtonData[index].buttonFunction == EButtonChoice.catButton)
        {
            ExecuteEvents.ExecuteHierarchy<ICatReactionInfoReciever>(gameObject, null, (x, y) => x.RecieveReactionInfo(_buttonDataList.listOfButtonData[index]));
            _sender.Continue(index);
        }
        else
        {
            _sender.Continue(index);
        }
    }

    public void OnDialogueReady()
    {
        _sender.Continue(0);
    }

    public void OnRecieveSceneTreeData(SceneTreeData data)
    {
        switch (data.type)
        {
            case ESceneTreeType.SubScene:
                SubScene[] subSceneList = transform.GetComponentsInChildren<SubScene>();
                foreach (SubScene scene in subSceneList)
                {
                    scene.SetImage(false);
                }
                if (data.sender != null)
                {
                    data.sender.Continue(0);
                }
                break;
            case ESceneTreeType.Conversation:
                ConversationData convertedData = (ConversationData)data;
                string name = "";
                if (GameStateContainer.Instance != null)
                {
                    if (GameStateContainer.Instance.names != null)
                    {
                        if (convertedData.characterNameTag == null) convertedData.characterNameTag = "";
                        if (GameStateContainer.Instance.names.ContainsKey(convertedData.characterNameTag))
                        {
                            name = GameStateContainer.Instance.names[convertedData.characterNameTag];
                        }
                    }
                    else
                    {
                        Debug.Log("Namelist is null");
                    }
                }

                if (data.sender != null)
                {
                    _sender = data.sender;
                }
                if (_cC != null)
                {
                    List<Sentence> tempList = new List<Sentence>();
                    for (int i = 0; i < convertedData.stringList.Length; i++)
                    {
                        tempList.Add(new Sentence(convertedData.stringList[i], null));
                    }
                    _cC.InitiateDialogue(new Sentence(name, null), tempList);
                }


                break;
            case ESceneTreeType.Buttons:
                List<string> buttonNames = new List<string>();

                _sender = data.sender;
                _buttonDataList = (ButtonDataList)data;

                //Choose-text
                if (_cC != null)
                {
                    if(_buttonBox != null)
                    {
                        if(_buttonBox.caption != null)
                        {
                            _cC.InitiateDialogue(_buttonBox.caption, null);
                        }
                    }
                }

                for (int i = 0; i < _buttonDataList.listOfButtonData.Count; i++)
                {
                    buttonNames.Add(_buttonDataList.listOfButtonData[i].buttonText);
                }

                _buttonBox.ReceiveButtonInfo(buttonNames);
                break;
            case ESceneTreeType.InputField:
                _sender = data.sender;
                InputData inputData = (InputData)data;
                _targetCharacterTagForInput = inputData.characterTag;
                if (_inputController != null)
                {
                    _inputController.Activate(inputData.caption);
                    if (_inputController.inputObject != null)
                        _inputController.inputObject.Select();
                    else
                        Debug.Log("No inputField!");
                }
                else Debug.Log("SceneTreeManager: inputcontroller is null");
                break;
            case ESceneTreeType.CatState:
                _sender = data.sender;
                ExecuteEvents.ExecuteHierarchy<ISceneTreeData>(transform.parent.gameObject, data, (handler, dataField) => handler.OnRecieveSceneTreeData((SceneTreeData)dataField));
                _sender.Continue(0);
                break;
            case ESceneTreeType.FadeEffect:
                _sender = data.sender;
                _sender.Continue(0);
                break;
            case ESceneTreeType.ConversationWithEffects:
                _sender = data.sender;
                ConversationWithEffectsData convertedData1 = ((ConversationWithEffectsData)data);

                name = "";
                if (GameStateContainer.Instance != null)
                {
                    if (GameStateContainer.Instance.names != null)
                    {
                        if (convertedData1.characterNameTag == null) convertedData1.characterNameTag = "";
                        if (GameStateContainer.Instance.names.ContainsKey(convertedData1.characterNameTag))
                        {
                            name = GameStateContainer.Instance.names[convertedData1.characterNameTag];
                        }
                    }
                    else
                    {
                        Debug.Log("Namelist is null");
                    }
                }

                if (data.sender != null)
                {
                    _sender = data.sender;
                }
                if (_cC != null)
                {
                    _cC.InitiateDialogue(new Sentence(name, convertedData1.nameEffects), convertedData1.sentences);
                }
                break;
            case ESceneTreeType.Phone:
                ExecuteEvents.ExecuteHierarchy<ISceneTreeData>(transform.parent.gameObject, data, (handler, dataField) => handler.OnRecieveSceneTreeData((SceneTreeData)dataField));
                data.sender.Continue(0);
                break;
            case ESceneTreeType.Emoji:
                ExecuteEvents.ExecuteHierarchy<ISceneTreeData>(transform.parent.gameObject, data, (handler, dataField) => handler.OnRecieveSceneTreeData((SceneTreeData)dataField));
                data.sender.Continue(0);
                break;

        }
    }

    void Start()
    {
        _buttonBox = GetComponentInChildren<ChoiceController>();
        _cC = GetComponentInChildren<ConversationController>();
        _inputController = GetComponentInChildren<InputController>();
        if (_inputController != null) _inputController.DeActivate();

        if (GameStateContainer.Instance != null)
        {
            bool result = false;
            if (GameStateContainer.Instance.useSavepointContinuePath)
            {
                result = LoadSavePoint();
            }
            
            if(!result)
            {
                SceneTreeObject obj = GetComponentInChildren<SceneTreeObject>();
                if (obj != null) obj.ActivateAndWait();
            }
        }
    }

    public void LoadLastSavePoint()
    {
        if (GameController.Instance.buttonsClickable)
        {
            _cC.Clear();
            LoadSavePoint();
        }
    }

    public bool LoadSavePoint()
    {
        if (GameStateContainer.Instance != null)
        {
            if (GameStateContainer.Instance.savepointIndex != 0)
            {
                int index = -1;
                SavePoint[] objectToLoad = gameObject.GetComponentsInChildren<SavePoint>();
                for (int i = 0; i < objectToLoad.Length; i++)
                {
                    if (objectToLoad[i].savepointIndex == GameStateContainer.Instance.savepointIndex)
                    {
                        index = i;
                        break;
                    }
                }
                if (index > -1)
                {
                    ExecuteEvents.ExecuteHierarchy<ISerializeCat>(gameObject, null, (sender, data) => sender.SerializeCat(false));
                    objectToLoad[index].Continue(0);
                    //Debug.Log(GameStateContainer.Instance.affinity["BossCatNameTag"]);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    public void OnInputReturn(string text)
    {
        if (GameStateContainer.Instance != null)
        {
            if (_targetCharacterTagForInput == null) _targetCharacterTagForInput = "";
            if (GameStateContainer.Instance.names.ContainsKey(_targetCharacterTagForInput))
            {
                GameStateContainer.Instance.names[_targetCharacterTagForInput] = text;
            }
            else
            {
                GameStateContainer.Instance.names.Add(_targetCharacterTagForInput, text);
            }
        }
        //_inputField.text = "";
        if (_inputController != null) _inputController.DeActivate();
        else Debug.Log("OnInputReturn: inputcontroller is null");
        _sender.Continue(0);
    }
}
