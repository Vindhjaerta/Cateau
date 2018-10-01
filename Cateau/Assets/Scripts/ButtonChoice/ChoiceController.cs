using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IButtonData : IEventSystemHandler
{
    void ChosenButtonIndex(int index);
}

public class ChoiceController : MonoBehaviour, IButtonChoiceReciever
{
    [SerializeField]
    private List<GameObject> _buttons;

    [SerializeField]
    private float _paddingY = 150;

    [SerializeField]
    private int _previewButtons;

    private bool _doneEmptying = true;

    private List<Vector3> _buttonsPos = new List<Vector3>();

    void Awake()
    {
        SaveButtonPositions();
    }

    private void SaveButtonPositions()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttonsPos.Add(_buttons[i].transform.position);
        }
    }

    public void ReceiveButtonInfo(List<string> nameList)
    {
        StartCoroutine(enumerator(nameList));
    }

    public Sentence caption = new Sentence("Choose",null);

    IEnumerator enumerator(List<string> nameList)
    {
        yield return new WaitUntil(() =>_doneEmptying);
        if (nameList.Count > _buttons.Count)
        {
            CreateButtons(nameList.Count);
        }
        EnableButtons(nameList.Count);
        NameButtons(nameList);
    }

    private void CreateButtons(int nameListLength)
    {
        RectTransform rt = (RectTransform)_buttons[0].transform;
        float halfTransformSize = rt.rect.height / 2;
        for (int i = _buttons.Count; i < nameListLength; i++)
        {
            GameObject Clone = Instantiate(_buttons[0], new Vector3(_buttons[0].transform.position.x, _buttons[0].transform.position.y - halfTransformSize * (i) - _paddingY * (i), _buttons[0].transform.position.z), Quaternion.identity);
            Clone.transform.SetParent(gameObject.transform);
            _buttons.Add(Clone);
        }
    }

    private void NameButtons(List<string> nameList)
    {
        for (int i = 0; i < nameList.Count; i++)
        {
            _buttons[i].GetComponentInChildren<Text>().text = nameList[i];
            _buttons[i].GetComponentInChildren<ButtonIndex>().Index(i);
        }
    }

    private void EnableButtons(int nameListLength)
    {
        if (nameListLength == 2)
        {
             _buttons[0].transform.position = new Vector3(_buttons[0].transform.position.x, _buttons[0].transform.position.y - 150.0f, _buttons[0].transform.position.z);
             _buttons[1].transform.position = new Vector3(_buttons[1].transform.position.x, _buttons[1].transform.position.y - 150.0f, _buttons[1].transform.position.z);
        }
        else if (nameListLength == 1)
        {
            _buttons[0].transform.position = new Vector3(_buttons[0].transform.position.x, _buttons[0].transform.position.y - 200.0f, _buttons[0].transform.position.z);
        }

       for (int i = 0; i < nameListLength; i++)
       {
            _buttons[i].SetActive(true);
       }
    }

    public void DisableButtons()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            _buttons[i].SetActive(false);
            _buttons[i].transform.position = _buttonsPos[i];
        }
        _doneEmptying = true;
    }

    public void ButtonClicked(int index)
    {
        ExecuteEvents.ExecuteHierarchy<IButtonData>(gameObject, null, (x, y) => x.ChosenButtonIndex(index));
        DisableButtons();
    }

    public void OnDisable()
    {
        for (int i = _buttons.Count; i > 1; i--)
        {
            Destroy(_buttons[i-1].gameObject);
            _buttons.RemoveAt(i-1);
        }
        _buttons[0].gameObject.SetActive(false);
        //gameObject.SetActive(false);
    }


    void OnDrawGizmos()
    {
        RectTransform rt = (RectTransform)_buttons[0].transform;
        if (_previewButtons > 0)
        {
            Gizmos.DrawCube(transform.position, new Vector3(rt.rect.width, rt.rect.height, 1));
            for (int i = 1; i < _previewButtons; i++)
            {
                Gizmos.DrawCube(new Vector3(transform.position.x, transform.position.y + (-rt.rect.height / 2 - _paddingY) * i, transform.position.z), new Vector3(rt.rect.width, rt.rect.height, 1));
            }
        }
    }

}
