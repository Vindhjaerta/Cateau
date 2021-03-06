﻿using System.Collections;
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

    //private Sprite _origArrowSprite;
    private Image _arrowImage;
    private RectTransform _doneArrowRect;
    [System.NonSerialized]
    public bool alwaysShowArrow;
    private Vector2 _offset;
    [SerializeField]
    private CE_AlterTypingArrow _standardArrow;
    private Canvas _canvas;

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
            //ResetArrowSprite();
        }
        else Debug.LogError("Dialogue not set up properly!");
    }

    private void Start()
    {
        if(GameStateContainer.Instance != null)
        {
            autoPageTurn = GameStateContainer.Instance.autoTurnPage;

            _canvas = GameController.Instance.gameObject.GetComponentInChildren<Canvas>();
            
        }
        if(_standardArrow != null)
            SetArrowSprite(_standardArrow.sprite, _standardArrow.offset);
    }

    private void Awake()
    {
        if(_doneArrow != null)
        {
            _arrowImage = _doneArrow.GetComponent<Image>();
            _doneArrowRect = _doneArrow.GetComponent<RectTransform>();
            //_origArrowSprite = _arrowImage.sprite;
        }

    }

    public void SetArrowSprite(Sprite newSprite, Vector2 offset)
    {
        if (newSprite != null)
        {
            _arrowImage.sprite = newSprite;
            _doneArrowRect.sizeDelta = newSprite.rect.size;
            _offset = offset;
        }
        else
        {
            ResetArrowSprite();
        }
        
    }

    public void ResetArrowSprite()
    {
        //_arrowImage.sprite = _origArrowSprite;
        //_doneArrowRect.sizeDelta = _origArrowSprite.rect.size;
        if (_standardArrow.sprite != null)
        {
            _arrowImage.sprite = _standardArrow.sprite;
            _doneArrowRect.sizeDelta = _standardArrow.sprite.rect.size;
            _offset = _standardArrow.offset;
        }
        else
        {
            _arrowImage.sprite = null;
            _doneArrowRect.sizeDelta = Vector2.zero;
            _offset = Vector2.zero;
        }
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
        ResetArrowSprite();
        alwaysShowArrow = false;
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
                _doneArrow.transform.localPosition = Vector3.zero;
                if (_dialogueText != null && _canvas != null)
                {
                    Vector3 vec = UITextOverflow.GetLastPosition(_dialogueText);
                    _doneArrow.transform.localPosition = new Vector3((vec.x / _canvas.scaleFactor) + (_doneArrowRect.rect.size.x * 0.5f) + (_offset.x ), (vec.y / _canvas.scaleFactor) + (_doneArrowRect.rect.size.x * 0.5f) + (_offset.y ), 0);
                    //_doneArrow.transform.localPosition = new Vector3((vec.x / _canvas.scaleFactor) + (_offset.x / _canvas.scaleFactor), (vec.y / _canvas.scaleFactor)  + (_offset.y / _canvas.scaleFactor), 0);
                }
            }

            if (dialoguePrint.done && autoEndConversation && dialoguePrint.autoPageTurn && dialoguePrint.delayCounter == 0)
            {
                EndDialogue();
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
