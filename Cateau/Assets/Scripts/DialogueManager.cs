using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class DialogueManager : MonoBehaviour
{

    //Hej GitHUbTestKommenterarer

    public Text nameText;

    public Text dialogueText;

    public GameObject dialogueBox;

    //MAKE PRIVATE AND FETCH FROM GAMESTATECONTAINER
    public TypingSpeed typingSpeed;

    [SerializeField]
    private TypingSpeed fastForward;
    
    public bool autoTyping = false;

	public float delayForNextSentence;

	public float sentenceTimerDuration;

    public bool cutOverflowByWord = true;

    private float _textDelaySpeed;
    private bool _typing = false;
    private int _currentCharIndex = 0;
    private int _stringRowIndex = 0;
    private List<string> _currentDialogue = null;
    private Timer _nextSentenceTimer;
    private int _lastWordIndex = 0;

    public GameObject lastPos;

    public void Awake()
    {
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
    }

    public void StartDialogue(string[] dialogue, string characterName)
    {
        _typing = true;
        _lastWordIndex = 0;
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(true);
        }
        _nextSentenceTimer = new Timer(sentenceTimerDuration);
        _currentCharIndex = 0;
        _stringRowIndex = 0;

        if(typingSpeed != null)
        {
            _textDelaySpeed = typingSpeed.typingSpeed;
        } else
        {
            _textDelaySpeed = 1;
        }
        if (dialogue.Length > 0)
        {
            _currentDialogue = new List<string>(dialogue);
            if (nameText != null)
            {
                nameText.text = characterName;
            }
            StartCoroutine(DisplayTimer());
        }
        else
        {
            EndDialogue();
        }

    }

    IEnumerator DisplayTimer()
    {
        while(1 == 1)
        {
            yield return new WaitForSeconds(_textDelaySpeed);

            if (_currentDialogue != null)
            {
                if (_stringRowIndex >= 0 && _stringRowIndex < _currentDialogue.Count)
                {
                    if (_currentCharIndex > _currentDialogue[_stringRowIndex].Length)
                    {
                        continue;
                    }
                }
                else Debug.LogError("DisplayTimer(): _stringRowIndex [" + _stringRowIndex + "] out of bounds in [continue] block");
            }
            else Debug.LogError("Dialogue not found in DisplayTimer()");

            if (dialogueText != null)
            {
                if (_currentDialogue != null)
                {
                    if (_stringRowIndex >= 0 && _stringRowIndex < _currentDialogue.Count)
                    {
                        if (_currentCharIndex >= 0 && _currentCharIndex < _currentDialogue[_stringRowIndex].Length)
                        {
                            int actualLastIndex = _currentCharIndex;
                            if (cutOverflowByWord) actualLastIndex = _lastWordIndex;
                            string lastString = _currentDialogue[_stringRowIndex].Substring(0, actualLastIndex);
                            string newString = _currentDialogue[_stringRowIndex].Substring(0, _currentCharIndex + 1);
                            if(_currentDialogue[_stringRowIndex][_currentCharIndex] == ' ')
                            {
                                if(_currentCharIndex < _currentDialogue[_stringRowIndex].Length - 1)
                                {
                                    _lastWordIndex = _currentCharIndex + 1;
                                }
                            }
                            dialogueText.text = newString;
                            if (UITextOverflow.IsOverflow(dialogueText))
                            {
                                dialogueText.text = lastString;

                                _currentDialogue.Insert(_stringRowIndex + 1, _currentDialogue[_stringRowIndex].Substring(actualLastIndex, _currentDialogue[_stringRowIndex].Length - actualLastIndex));
                                _currentDialogue[_stringRowIndex] = lastString;

                            }
                        }
                    }
                    else Debug.LogError("DisplayTimer(): _stringRowIndex [" + _stringRowIndex + "] out of bounds in [show text] block");

                    if (_currentCharIndex < _currentDialogue[_stringRowIndex].Length)
                        _currentCharIndex++;
                }
                else Debug.LogError("DisplayTimer(): Dialogue is null");
            }
            else Debug.LogError("DisplayTimer(): Dialogue ref not found");

            //Add continue after each error?

            //check for stop adding to variable?


        }
    }

    public void DisplayNextSentence()
    {
		_typing = true;
        if (_currentDialogue != null)
        {
            if (_stringRowIndex >= 0 && _stringRowIndex < _currentDialogue.Count)
            {
                if (_currentCharIndex < _currentDialogue[_stringRowIndex].Length - 1)
                {
                    //_currentCharIndex = _currentDialogue[_stringRowIndex].Length - 1;
                    if (fastForward != null)
                    {
                        _textDelaySpeed = fastForward.typingSpeed;
                    }
                }
                else
                {
                    if (_stringRowIndex < _currentDialogue.Count - 1)
                    {
                        _textDelaySpeed = typingSpeed.typingSpeed;
                        _stringRowIndex++;
                        _currentCharIndex = 0;
                        _lastWordIndex = 0;
                    }
                    else
                    {
                        EndDialogue();
                    }
                }
            }
            else
            {
                EndDialogue();
            } 
        }
        else Debug.LogError("DisplayNextSentence(): Dialogue not found");
    }

	void Update()
	{
        if (dialogueBox != null)
        {
            if (dialogueBox.activeSelf)
            {
                if (_currentDialogue != null)
                {
                    if (_stringRowIndex >= 0 && _stringRowIndex < _currentDialogue.Count)
                    {
                        //Debug.Log(_currentCharIndex + " " + (_currentDialogue[_stringRowIndex].Length-1));
                        if (_currentCharIndex >= _currentDialogue[_stringRowIndex].Length - 1)
                        {
                            _typing = false;
                        }

                        if (_typing == false && autoTyping == true)
                        {
                            _nextSentenceTimer.Tick(Time.deltaTime);

                            if (_nextSentenceTimer.Done)
                            {
                                DisplayNextSentence();
                                _nextSentenceTimer.Reset(sentenceTimerDuration);
                            }
                        }
                    }
                    else Debug.LogError("Update(): _stringRowIndex [" + _stringRowIndex + "] out of bounds");
                }


                //Press Space or left click to move on in the dialogue
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    DisplayNextSentence();
                }
            }
        }
        

    }

	public void SwitchAutoTyping()
	{
        autoTyping = !autoTyping;
	}

    void EndDialogue()
    {
        _typing = false;
        if (dialogueBox != null)
        {
            dialogueBox.SetActive(false);
        }
        StopCoroutine(DisplayTimer());
        ExecuteEvents.ExecuteHierarchy<IDialogue>(gameObject, null, (handler, data) => handler.OnDialogueReady());
    }


    
}