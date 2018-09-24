using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName ="Game State",menuName ="System/Game State")]
public class GameStateContainer : SingletonScriptableObject<GameStateContainer>
{
    private const string FILE_NAME = "save.dat";

    [SerializeField]
    private StringReference _filename;
    public string scene;
    public int savepointIndex;
    [SerializeField]
    public SettingsContainer settings;
    public bool[] activePhotos;
    public List<string> savableStrings;

    public FloatReference[] typingSpeeds;
    public IntVariable[] fontSizes;


    public Dictionary<string, int> affinity = new Dictionary<string, int>();
    public Dictionary<string, string> names = new Dictionary<string, string>();
    public Dictionary<string, bool> isCatHome = new Dictionary<string, bool>();

    [System.NonSerialized]
    private bool _isInitialized = false;
    public bool isInitialized
    {
        get { return _isInitialized; }
    }

    [SerializeField]
    private StringReference[] affinityTags;
    [SerializeField]
    private IntReference[] affinityValues;
    [SerializeField]
    private StringReference[] nameTags;
    [SerializeField]
    private StringReference[] nameValues;

    [HideInInspector]
    public bool turnPage = false;

    [HideInInspector]
    public bool imAButton = false;

    [HideInInspector]
    public bool useSavepointContinuePath = false;

    [HideInInspector]
    public bool inMenu;

    [HideInInspector]
    public bool autoTurnPage = false;

    public void Initialize()
    {
        affinity.Clear();

        for (int i = 0; i < affinityTags.Length; i++)
        {
            if (i < affinityValues.Length)
            {
                if (!affinity.ContainsKey(affinityTags[i]))
                {
                    affinity[affinityTags[i]] = affinityValues[i];
                }
                else
                {
                    affinity.Add(affinityTags[i], affinityValues[i]);
                }
            }
        }

        names.Clear();

        for (int i = 0; i < nameTags.Length; i++)
        {
            if (i < nameValues.Length)
            {
                if (!names.ContainsKey(nameTags[i]))
                {
                    names[nameTags[i]] = nameValues[i];
                }
                else
                {
                    names.Add(nameTags[i], nameValues[i]);
                }
            }
        }

        isCatHome.Clear();

        turnPage = false;
        autoTurnPage = false;
        inMenu = false;
        imAButton = false;
        _isInitialized = true;
        savepointIndex = 0;
        useSavepointContinuePath = false;

    }

    public void ClearGallery()
    {
        for (int i = 0; i < activePhotos.Length; i++)
        {
            activePhotos[i] = false;
        }
    }

    public bool LoadGameState()
    {
        string filename = _filename;
        if (filename == "") filename = FILE_NAME;
        List<object> data = new List<object>();
        data = Serializer.LoadFromDisc(Application.persistentDataPath + filename);
        if (data != null)
        {
            int version = 0;

            if (data[0] is int) version = (int)data[0];

            if (version == 2079)
            {
                settings = (SettingsContainer)data[1];
                scene = (string)data[2];
                activePhotos = (bool[])data[3];
                savepointIndex = (int)data[4];
                savableStrings = (List<string>)data[5];

                List<string> affinityKey = new List<string>();
                List<int> affinityValue = new List<int>();
                affinityKey = (List<string>)data[6];
                affinityValue = (List<int>)data[7];
                affinity.Clear();
                for (int i = 0; i < affinityKey.Count; i++)
                {
                    affinity.Add(affinityKey[i], affinityValue[i]);
                }

                List<string> namesKey = new List<string>();
                List<string> namesValue = new List<string>();
                namesKey = (List<string>)data[8];
                namesValue = (List<string>)data[9];
                names.Clear();
                for (int i = 0; i < namesKey.Count; i++)
                {
                    names.Add(namesKey[i], namesValue[i]);
                }

                List<string> isCatHomeKey = new List<string>();
                List<bool> isCatHomeValue = new List<bool>();
                isCatHomeKey = (List<string>)data[10];
                isCatHomeValue = (List<bool>)data[11];
                isCatHome.Clear();
                for (int i = 0; i < isCatHomeKey.Count; i++)
                {
                    isCatHome.Add(isCatHomeKey[i], isCatHomeValue[i]);
                }
            }
            else
            {

                Debug.Log("wrong version of save data");
                return false;
            }
        }
        else
        {
            Debug.Log("could not load save");
            return false;
        }

        return true;
    }
    public bool SaveGameState()
    {
        int version = 2079;
        string filename = _filename;
        if (filename == "") filename = FILE_NAME;
        List<object> data = new List<object>();
        data.Add(version);
        data.Add(settings);
        data.Add(scene);
        data.Add(activePhotos);
        data.Add(savepointIndex);
        data.Add(savableStrings);
        List<string> affinityKey = new List<string>();
        List<int> affinityValue = new List<int>();
        foreach(KeyValuePair<string,int> pair in affinity)
        {
            affinityKey.Add(pair.Key);
            affinityValue.Add(pair.Value);
        }
        data.Add(affinityKey);
        data.Add(affinityValue);

        List<string> namesKey = new List<string>();
        List<string> namesValue = new List<string>();
        foreach (KeyValuePair<string, string> pair in names)
        {
            namesKey.Add(pair.Key);
            namesValue.Add(pair.Value);
        }
        data.Add(namesKey);
        data.Add(namesValue);

        List<string> isCatHomeKey = new List<string>();
        List<bool> isCatHomeValue = new List<bool>();
        foreach (KeyValuePair<string, bool> pair in isCatHome)
        {
            isCatHomeKey.Add(pair.Key);
            isCatHomeValue.Add(pair.Value);
        }
        data.Add(isCatHomeKey);
        data.Add(isCatHomeValue);

        bool result = Serializer.SaveToDisc(Application.persistentDataPath + filename, data);
        if (!result)
        {
            Debug.LogError("could not save");
            return false;
        }

        return true;
    }


}

