using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour, ICatReactionInfoReciever,ISwitchScene,ISceneTreeData,ISerializeCat,IShake, IPhone, IEmoji,IArrow {

    public GameStateContainer gameStateContainer;

    private SpriteCatScript _catScript;   

    private static GameController _instance;

    public bool buttonsClickable;

    private Camera camera;
    private Vector3 cameraOrig;
    private Vector3 subOrig;
    private float shakeMagnitude;
    private float shakeCounter;
    private SubScene shakeSubScene;

    private Phone phone;
    private EmojiController emoji;
    private ConversationController _cC;

    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        if(GameStateContainer.Instance != null)
        {
            if (!GameStateContainer.Instance.isInitialized)
            {
                GameStateContainer.Instance.Initialize();
            }
        }

        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        camera = Camera.main;
        cameraOrig = camera.gameObject.transform.position;

        phone = GetComponentInChildren<Phone>();
        if(phone != null)
        {
            phone.gameObject.SetActive(false);
        }

        emoji = GetComponentInChildren<EmojiController>();

        _cC = GetComponentInChildren<ConversationController>();

    }

    // Use this for initialization
    void Start () {
        _catScript = transform.GetComponentInChildren<SpriteCatScript>();
        if(_catScript != null)
        {
            LoadCat();
        }

    }

    public void OnSceneChange()
    {
        SaveCat();
    }

    public void RecieveReactionInfo(ButtonData catReactionData)
    {
        _catScript.ReceiveAffinityFromButton(catReactionData);
    }

    /*
        Saves the values of the current cat in the scene to the gamestate container
         */
    private void SaveCat()
    {
        if (GameStateContainer.Instance != null)
        {
            if (_catScript != null)
            {
                int affinity = _catScript.SendAffinity();
                string tag = _catScript.SendTag();
                string name = _catScript.CatName;
                if (tag == null) tag = "";
                if (GameStateContainer.Instance.affinity.ContainsKey(tag))
                {
                    GameStateContainer.Instance.affinity[tag] = affinity;
                }
                else
                {
                    GameStateContainer.Instance.affinity.Add(tag, affinity);
                }
                //if (GameStateContainer.Instance.names.ContainsKey(tag))
                //{
                //    GameStateContainer.Instance.names.Add(tag, name);
                //}
            }
        }
    }

    public int GetCatAffinity(string catIdentifier)
    {
        int affinity = 0;
        if (_catScript != null)
        {
            if (catIdentifier == _catScript.SendTag())
            {
                affinity = _catScript.SendAffinity();
            }
            else
            {
                Debug.LogWarning("The cat you asked to get affinity for isn't  the active on on GameController");
            }
        }
        else
        {
            Debug.LogWarning("GameController didn't have a _catScript when it was asked for GetCatAffinity, it returned the vaule " + affinity);
        }
        return affinity;
    }

    private void LoadCat()
    {
        if(GameStateContainer.Instance != null)
        {
            if (_catScript != null)
            {
                string tag = _catScript.SendTag();
                if (tag == null) tag = "";
                //if (GameStateContainer.Instance.names.ContainsKey(tag))
                //{
                //    _catScript.name = GameStateContainer.Instance.names[tag];
                //}
                if (GameStateContainer.Instance.affinity.ContainsKey(tag))
                {
                    _catScript.ReceiveAffinity(0, GameStateContainer.Instance.affinity[tag], false);
                }
                    //vad händer om katten får 0 affinity?

            }
        }
    }

    public void SaveGame()
    {
        if(GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.SaveGameState();
        }
    }

    public void LoadGame()
    {
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.LoadGameState();
        }
    }

    public void OnRecieveSceneTreeData(SceneTreeData data)
    {
        switch (data.type)
        {
            case ESceneTreeType.CatState:

                CatStateData catStateData = (CatStateData)data;
                if (_catScript != null)
                {
                    if (catStateData.applyTransform)
                    {
                        _catScript.transform.position = catStateData.newTransform.position;
                    }

                    Image catImg = _catScript.GetComponent<Image>();
                    if(catImg != null)
                    {
                        catImg.enabled = catStateData.catEnabled;
                    }
                    else
                    {
                        Debug.Log("Could not find the cat image");
                    }

                    if(catStateData.newCatPrefab != null)
                    {
                        Transform parent = _catScript.transform.parent;
                        Transform current = _catScript.transform;
                        SaveCat();
                        Destroy(_catScript.gameObject);
                        SpriteCatScript cat = Instantiate(catStateData.newCatPrefab, parent);
                        if (catStateData.applyTransform)
                        {
                            cat.transform.position = catStateData.newTransform.position;
                            cat.transform.localRotation = catStateData.newTransform.localRotation;
                            cat.transform.localScale = catStateData.newTransform.localScale;
                        }
                        else
                        {
                            cat.transform.position = current.position;
                            cat.transform.localRotation = current.localRotation;
                            cat.transform.localScale = current.localScale;
                        }
                        cat.transform.SetAsFirstSibling();
                        _catScript = cat;
                        LoadCat();
                    }
                    else
                    {
                        Debug.Log("no new cat");
                    }

                }
                break;
            case ESceneTreeType.Phone:
                if (phone != null)
                {
                    PhoneData phoneData = (PhoneData)data;

                    if (phoneData.clearHistory)
                    {
                        phone.ClearMessages();
                    }
                    phone.gameObject.SetActive(phoneData.showOnScreen);
                }
                break;
            case ESceneTreeType.Emoji:
                if(emoji != null)
                {
                    EmojiSceneTreeData emojiData = (EmojiSceneTreeData)data;

                    emoji.Spawn(emojiData.emojiType);
                }
                break;
        }
    }

    public void SerializeCat(bool save)
    {
        if (save)
        {
            SaveCat();
        }
        else
        {
            LoadCat();
        }
    }

    public void OnShake(float duration, float magnitude)
    {
        if (shakeSubScene != null)
        {
            shakeSubScene.transform.position = subOrig;
        }
        shakeMagnitude = magnitude;
        shakeCounter += duration;
        shakeSubScene = GetComponentInChildren<SubScene>();
        if(shakeSubScene != null)
        {
            subOrig = shakeSubScene.transform.position;
        }
    }

    public void Update()
    {
        if(shakeCounter != 0 && shakeSubScene != null)
        {
            shakeSubScene.transform.position = new Vector3(subOrig.x + Random.Range(-1.0f, 1.0f) * shakeMagnitude, subOrig.y + Random.Range(-1.0f, 1.0f) * shakeMagnitude, subOrig.z);
            camera.transform.position = new Vector3(cameraOrig.x, cameraOrig.y, cameraOrig.y + 5.0f);
            shakeCounter -= Time.deltaTime;
            if (shakeCounter <= 0)
            {
                shakeCounter = 0;
                shakeSubScene.transform.position = subOrig;
                camera.transform.position = cameraOrig;
            }
        }
    }

    public void OnAddMessage(Sprite sprite)
    {
        if (phone != null)
        {
            phone.AddMessage(sprite);
        }
    }

    public void OnEmoji(EmojiType emojiType)
    {
        if (emoji != null)
        {
            emoji.Spawn(emojiType);
        }
    }

    public void OnAlterArrow(Sprite sprite, bool showWhileTyping, Vector2 offset)
    {
        if(_cC != null)
        {
            _cC.alwaysShowArrow = showWhileTyping;
            if(sprite != null)
            {
                _cC.SetArrowSprite(sprite, offset);
            }
            else
            {
                _cC.ResetArrowSprite();
            }
        }
    }
}
