using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour, ICatReactionInfoReciever,ISwitchScene,ISceneTreeData,ISerializeCat {

    public GameStateContainer gameStateContainer;

    private SpriteCatScript _catScript;   

    private static GameController _instance;

    public bool buttonsClickable; 

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
}
