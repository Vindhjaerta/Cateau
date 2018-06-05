using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crossroad : SceneTreeObject {

    public enum ECrossroad { SaveableString = 0, CatAffinity}

    public ECrossroad type;

    [System.Serializable]
    public class StringChoice
    {
        public StringReference savedString;
        public SceneTreeObject targetNode;
    }

    [System.Serializable]
    public class CatChoice
    {
        public StringReference catIdentifier;
        public ComparatorLine[] comparators;
        public SceneTreeObject targetNode;
    }

    [System.Serializable]
    public class ComparatorLine
    {
        public int compareValue;
        [SerializeField]
        public Comparator comparator;
    }

    public StringChoice[] stringChoices;
    public CatChoice[] catChoices;

    public override void Continue(int nodeIndex)
    {
        Continue();
    }

    protected override void Initialize()
    {
        switch (type)
        {
            case (ECrossroad.CatAffinity):

                for (int i = 0; i < catChoices.Length; i++)
                {
                    int affinity = 0;
                    if (GameStateContainer.Instance != null)
                    {
                        if (GameStateContainer.Instance.affinity != null)
                        {
                            affinity = GameStateContainer.Instance.affinity[catChoices[i].catIdentifier.value];
                            Debug.Log(affinity + " was found for: " + catChoices[i].catIdentifier);
                        }
                        else
                        {
                            Debug.Log("GameStateContainer.Instance.affinity == null. Says: " + gameObject);
                        }
                    }
                    else
                    {
                        affinity = 30; // GET AFFINITY FROM GAMESTATE
                        Debug.Log("Crossroad: Gamestate connection not yet implemented. Using affinity value of 30.");
                    }
                    bool result = false;
                    for (int j = 0; j < catChoices[i].comparators.Length; j++)
                    {
                        result = catChoices[i].comparators[j].comparator.Check(affinity, catChoices[i].comparators[j].compareValue);
                        if (!result) break;
                    }
                    if (result)
                    {
                        _nextNode = catChoices[i].targetNode;
                        break;
                    }
                }


                break;
            case (ECrossroad.SaveableString):


                foreach (StringChoice choice in stringChoices)
                {
                    if (GameStateContainer.Instance.savableStrings != null)
                    {
                        for (int i = 0; i < GameStateContainer.Instance.savableStrings.Count; i++)
                        {
                            if (GameStateContainer.Instance.savableStrings[i] == choice.savedString)
                            {
                                if (choice.targetNode != null)
                                {
                                    _nextNode = choice.targetNode;
                                }
                                break;
                            }
                        }
                    }
                }
                break;
        }
        Continue();
    }


}
