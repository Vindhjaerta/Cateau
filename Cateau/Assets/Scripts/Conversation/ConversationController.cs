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
    private FloatReference _standardAutoPageTurnDelay;

    public bool clearDialogueWhenFinished;
    public bool wordWrap;
    public bool autoPageTurn;
    public bool autoEndConversation;
    public enum ETextAdaption { ByLines = 0, ByTextboxArea }

    public PrintText namePrint = null;
    public PrintText dialoguePrint = null;

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
            else
            {
                EndDialogue();
            }
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
