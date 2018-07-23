using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public interface IDialogue : IEventSystemHandler
{
    void OnDialogueReady();
}

public class ConversationController : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _dialogueText;
    [SerializeField]
    private GameObject _doneArrow;
    [SerializeField]
    private FloatReference _standardAutoPageTurnDelay;

    public bool clearDialogueWhenFinished;
    public bool wordWrap;
    public bool autoPageTurn;
    public bool autoEndConversation;
    public enum ETextAdaption { ByLines = 0, ByTextboxArea }

    public PrintText namePrint = null;
    public PrintText dialoguePrint = null;

    [SerializeField]
    private int _arrowCycleSpeed = 6;
    private float _arrowCycleCounter;

    private Sprite _origArrowSprite;
    private Vector2 _origArrowSize;
    private Image _arrowImage;
    private Vector2 _currentArrowSize;
    [System.NonSerialized]
    public bool alwaysShowArrow;
    private Vector2 _offset;

    public void InitiateDialogue(Sentence name, List<Sentence> sentences)
    {
        if (_nameText != null && _dialogueText != null && GameStateContainer.Instance.typingSpeeds != null && GameStateContainer.Instance != null)
        {
            List<Sentence> tempSentence = new List<Sentence>();
            if (name != null)
            {
                if(name.text != "")
                {
                    _nameText.transform.parent.gameObject.SetActive(true);
                } else _nameText.transform.parent.gameObject.SetActive(false);
            } else _nameText.transform.parent.gameObject.SetActive(false);

            tempSentence.Add(name);
            namePrint = new PrintText(_nameText, tempSentence, false);
            if (sentences != null)
            {
                
                if (sentences.Count > 0)
                {
                    dialoguePrint = new PrintText(_dialogueText, sentences, wordWrap, GameStateContainer.Instance.typingSpeeds[GameStateContainer.Instance.settings.typingSpeedIndex], autoPageTurn, _standardAutoPageTurnDelay, 0);
                }
                else
                {
                    EndDialogue();
                }
            }
            //else
            //{
            //    EndDialogue();
            //}
        }
        else Debug.LogError("Dialogue not set up properly!");
    }

    private void Start()
    {
        if(GameStateContainer.Instance != null)
        {
            autoPageTurn = GameStateContainer.Instance.autoTurnPage;
        }
    }

    private void Awake()
    {
        if(_doneArrow != null)
        {
            _arrowImage = _doneArrow.GetComponent<Image>();
            _currentArrowSize = _doneArrow.GetComponent<RectTransform>().rect.size;
            _origArrowSprite = _arrowImage.sprite;
            _origArrowSize = _currentArrowSize;
            
        }
    }

    public void SetArrowSprite(Sprite newSprite, Vector2 offset)
    {
        _arrowImage.sprite = newSprite;
        _doneArrow.GetComponent<RectTransform>().sizeDelta = newSprite.rect.size;
        _offset = offset;
    }

    public void ResetArrowSprite()
    {
        _arrowImage.sprite = _origArrowSprite;
        _doneArrow.GetComponent<RectTransform>().sizeDelta = _origArrowSize;
        _offset = Vector2.zero;
    }

    public void TurnPage()
    {
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.turnPage = true;
        }
    }

    public void Clear()
    {
        dialoguePrint = null;
        namePrint = null;
        _nameText.transform.parent.gameObject.SetActive(false);
        _dialogueText.text = "";
        _nameText.text = "";
    }

    private void ActualTurnpage()
    {
        if (dialoguePrint != null)
        {
            if (dialoguePrint.pageDone)
            {
                dialoguePrint.Turnpage();
            }
            else
            {
                dialoguePrint.PrintEntirePage();
            }
            if (dialoguePrint.done)
            {
                EndDialogue();
            }
        }
    }

    public void SwitchAutoPageTurn()
    {
        autoPageTurn = !autoPageTurn;
        if (GameStateContainer.Instance != null)
        {
            GameStateContainer.Instance.autoTurnPage = autoPageTurn;
        }
        if (dialoguePrint != null)
        {
            dialoguePrint.autoPageTurn = autoPageTurn;
            
        }
    }

    public void EndDialogue()
    {

        if (clearDialogueWhenFinished)
        {
            namePrint = null;
            dialoguePrint = null;
            _nameText.text = "";
            _nameText.transform.parent.gameObject.SetActive(false);
            _dialogueText.text = "";
        }

        ExecuteEvents.ExecuteHierarchy<IDialogue>(gameObject, null, (handler, data) => handler.OnDialogueReady());
    }

    void Update()
    {

        if (GameStateContainer.Instance != null)
        {

            if (!GameStateContainer.Instance.inMenu && !GameStateContainer.Instance.imAButton)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {

                    GameStateContainer.Instance.turnPage = true;
                }
            }
        }

        if (namePrint != null)
        {
            namePrint.Tick(Time.deltaTime);

        }

        if (dialoguePrint != null)
        {
            dialoguePrint.Tick(Time.deltaTime);

            if (dialoguePrint.done && autoEndConversation && dialoguePrint.autoPageTurn && dialoguePrint.delayCounter == 0)
            {
                EndDialogue();
            }

            if (!dialoguePrint.pageDone && !alwaysShowArrow)
            {
                _arrowImage.color = new Color(1, 1, 1, 0);
            }
            else if (!dialoguePrint.pageDone && alwaysShowArrow)
            {
                _arrowImage.color = new Color(1, 1, 1, 1);
            }
            else
            {
                _arrowImage.color = new Color(1, 1, 1, Mathf.Sin(_arrowCycleCounter));
            }

            _arrowCycleCounter += (Time.deltaTime * _arrowCycleSpeed);
            if (_arrowCycleCounter > 360) _arrowCycleCounter = 0;

            if (_doneArrow != null)
            {
                Vector3 vec = dialoguePrint.GetEndPosition();
                _doneArrow.transform.localPosition = new Vector3( vec.x + _currentArrowSize.x + _offset.x, vec.y + (_currentArrowSize.y / 2) + _offset.y, _doneArrow.transform.localPosition.z);
            }

        }
        else
        {
            _arrowImage.color = new Color(1, 1, 1, 0);
            _arrowCycleCounter = 0;
        }

    }

    void FixedUpdate()
    {
        if (GameStateContainer.Instance != null)
        {
            if (GameStateContainer.Instance.turnPage)
            {
                ActualTurnpage();
                GameStateContainer.Instance.turnPage = false;
            }

        }
    }

}
