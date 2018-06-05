using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintText
{

    private float _printCounter;
    private float _delayCounter;
    private float _currentPrintSpeed;
    private Text _targetText;
    private float _deltaAcc;
    private float _standardPrintSpeed;
    private float _standardAutoPageTurnDelay;
    private bool _done = false;
    private bool _wrapByWord = false;
    private int _maxLines = 0;
    private bool _turnpage = false;
    private bool _printEntirePage = false;
    private bool _pageDone = false;
    private bool _autoPageTurn = false;
    private string _wrapTestBuffer;
    private List<Sentence> _pageFrontBuffer;
    private List<Sentence> _originalSentences;
    private string _wordBuffer;
    private bool _wrapCheck;
    private bool _newSentence;


    public PrintText(Text targetText, List<Sentence> sentences, bool wrapByWord, float standardPrintSpeed, bool autoPageTurn, float standardAutoPageTurnDelay, int maxLines)
    {
        _maxLines = maxLines;
        Initialize(targetText, sentences, wrapByWord, standardPrintSpeed, autoPageTurn, standardAutoPageTurnDelay);
    }
    public PrintText(Text targetText, List<Sentence> sentences, bool wrapByWord)
    {
        Initialize(targetText, sentences, wrapByWord, 0.0f, false, 0.0f);
    }

    private void Initialize(Text targetText, List<Sentence> sentences, bool wrapByWord, float standardPrintSpeed, bool autoPageTurn, float standardAutoPageTurnDelay)
    {
        #region Initialize
        if (targetText != null)
        {
            this.standardPrintSpeed = standardPrintSpeed;
            if (standardAutoPageTurnDelay > 0) _standardAutoPageTurnDelay = standardAutoPageTurnDelay;
            else _standardAutoPageTurnDelay = 0;
            _autoPageTurn = autoPageTurn;
            _printEntirePage = false;
            _targetText = targetText;
            _targetText.text = "";
            _newSentence = true;
            _wrapCheck = true;
            _pageDone = false;
            _delayCounter = 0;

            _deltaAcc = 0;

            _printCounter = 0;
            _wrapByWord = wrapByWord;
            _pageFrontBuffer = new List<Sentence>();
            _originalSentences = new List<Sentence>();
            if (sentences != null)
            {
                foreach (Sentence sentence in sentences)
                {
                    _originalSentences.Add(new Sentence(sentence.text, sentence.effects));
                }
            }
        }
        else
        {
            Debug.LogError("Text is null");
            _done = true;
        }
        #endregion
    }

    public void Tick(float deltatime)
    {

        #region Nulltest targetText
        if (_targetText != null)
        #endregion
        {
            if ((!_pageDone && (_delayCounter == 0 || _printEntirePage)) && !_done)
            {
                #region Nulltest originalSentences
                if (_originalSentences != null)
                #endregion
                {
                    if (!_printEntirePage && !_pageDone && _delayCounter == 0)
                        _deltaAcc += deltatime;
                    else
                        _deltaAcc = 0;

                    while (!_pageDone)
                    {
                        #region Length test originalSentences
                        if (_originalSentences.Count > 0)
                        {
                            if (_originalSentences[0].text.Length > 0)
                            #endregion
                            {
                                //There are letters to print

                                #region Initialize if new sentence
                                //Initialize if new sentence
                                if (_newSentence)
                                {
                                    //float prevPrintSpeed = currentPrintSpeed;
                                    _currentPrintSpeed = _standardPrintSpeed;
                                    if (_originalSentences[0].effects != null)
                                    {
                                        for (int i = 0; i < _originalSentences[0].effects.Count; i++)
                                        {
                                            _originalSentences[0].effects[i].Initialize(this, _originalSentences[0], _targetText);
                                        }
                                    }
                                }
                                #endregion

                                if (_deltaAcc >= _currentPrintSpeed || _printEntirePage)
                                {
                                    //print letter to buffer, then check if overflow

                                    //check if we are allowed to print, i.e no overflow
                                    #region Wraptest

                                    bool newLineResult = false;
                                    int newLineBufferCheckCounter = 0;

                                    if (_wrapByWord)
                                    {
                                        //Only do a wrap check at the beginning of a new word
                                        if (_wrapCheck)
                                        {
                                            // fill _wrapTestBuffer and test overflow for each letter, until a word break occurs

                                            string wrapTestBufferBack = "";
                                            string wrapTestBufferFront = "";
                                            bool wrapCheckResult = true;

                                            //Add tags and such so the wrap test can take font size into account
                                            for (int i = 0; i < _pageFrontBuffer.Count; i++)
                                            {
                                                wrapTestBufferFront += _pageFrontBuffer[i].ApplyEffects(_pageFrontBuffer[i].text);
                                            }

                                            //Put the next word into a buffer and check for overflow
                                            for (int i = 0; i < _originalSentences.Count; i++)
                                            {
                                                _wrapTestBuffer = "";
                                                for (int j = 0; j < _originalSentences[i].text.Length; j++)
                                                {
                                                    //Check if the word has ended
                                                    if (CheckWordBreak(_originalSentences[i].text[j], false))
                                                    {
                                                        _wrapCheck = false;
                                                        break;
                                                    }

                                                    _wrapTestBuffer = _originalSentences[i].ApplyEffects(_originalSentences[i].text.Substring(0, j + 1));

                                                    //check overflow
                                                    if (UITextOverflowCheck.IsOverflow(_targetText))
                                                    {
                                                        _wrapCheck = false;
                                                        wrapCheckResult = false;
                                                        break;
                                                    }

                                                    //the text fits, now test for newline
                                                    _targetText.text = wrapTestBufferFront + wrapTestBufferBack;
                                                    float testheight = UITextOverflowCheck.GetHeight(_targetText);

                                                    _targetText.text += _wrapTestBuffer;
                                                    newLineBufferCheckCounter++;

                                                    //_targetText is filled with all buffers, take the opportunity to check max lines
                                                    if (_maxLines > 0)
                                                    {
                                                        Canvas.ForceUpdateCanvases(); //lineCount below isn't updated until the next frame without this line
                                                        if (_targetText.cachedTextGenerator.lineCount > _maxLines)
                                                        {
                                                            _wrapCheck = false;
                                                            wrapCheckResult = false;
                                                            break;
                                                        }
                                                    }

                                                    if (UITextOverflowCheck.GetHeight(_targetText) > testheight)
                                                    {
                                                        if (newLineBufferCheckCounter > 1)
                                                        {
                                                            newLineResult = true;
                                                        }
                                                        break;
                                                    }

                                                }
                                                if (!_wrapCheck) break;
                                                wrapTestBufferBack += _wrapTestBuffer;
                                            }

                                            if (!wrapCheckResult)
                                            {
                                                if (_autoPageTurn) _delayCounter = _standardAutoPageTurnDelay;
                                                _delayCounter = _standardAutoPageTurnDelay;
                                                _pageDone = true;
                                                _wrapCheck = false;
                                                break;
                                            }
                                            _wrapCheck = false;
                                        }
                                    }
                                    else
                                    {
                                        // fill _wrapTestBuffer
                                        _wrapTestBuffer = _originalSentences[0].text.Substring(0, 1);
                                        _originalSentences[0].ApplyEffects(_wrapTestBuffer);

                                        _targetText.text = "";
                                        for (int i = 0; i < _pageFrontBuffer.Count; i++)
                                        {
                                            _targetText.text += _pageFrontBuffer[i].ApplyEffects(_pageFrontBuffer[i].text);
                                        }

                                        //Save height for future newLine check
                                        float testheight = UITextOverflowCheck.GetHeight(_targetText);

                                        _targetText.text += _wrapTestBuffer;

                                        //check overflow
                                        if (UITextOverflowCheck.IsOverflow(_targetText))
                                        {
                                            if (_autoPageTurn) _delayCounter = _standardAutoPageTurnDelay;
                                            _pageDone = true;
                                            _delayCounter = _standardAutoPageTurnDelay;
                                            break;
                                        }

                                        //the text fits, now test for newline

                                        //_targetText is filled with all buffers, take the opportunity to check max lines
                                        if (_maxLines > 0)
                                        {
                                            Canvas.ForceUpdateCanvases(); //lineCount below isn't updated until the next frame without this line
                                            if (_targetText.cachedTextGenerator.lineCount > _maxLines)
                                            {
                                                if (_autoPageTurn) _delayCounter = _standardAutoPageTurnDelay;
                                                _pageDone = true;
                                                _delayCounter = _standardAutoPageTurnDelay;
                                                break;
                                            }
                                        }

                                        if (UITextOverflowCheck.GetHeight(_targetText) > testheight)
                                        {
                                            newLineResult = true;
                                        }

                                    }
                                    #endregion

                                    //We passed the wrap test, now actually print the letter

                                    if (!_printEntirePage) _deltaAcc -= _currentPrintSpeed;

                                    #region Check word break
                                    if (_wrapByWord)
                                    {
                                        if (CheckWordBreak(_originalSentences[0].text[0], false))
                                        {
                                            //This indicates the end of a word, flag for a wrap test next frame
                                            _wrapCheck = true;
                                        }
                                    }
                                    #endregion

                                    //If the wrap test decided a newLine was needed, now is the time to insert it
                                    #region NewLine
                                    if (newLineResult)
                                    {
                                        _pageFrontBuffer[_pageFrontBuffer.Count - 1].text += System.Environment.NewLine;
                                        //_originalSentences[0].text = System.Environment.NewLine + _originalSentences[0].text;
                                        newLineResult = false;
                                    }
                                    #endregion

                                    MoveFirstLetter(_originalSentences, _pageFrontBuffer, _newSentence);

                                    #region Finalize
                                    if (_originalSentences[0].text.Length == 0)
                                    {
                                        if (_originalSentences[0].effects != null)
                                        {
                                            for (int i = 0; i < _originalSentences[0].effects.Count; i++)
                                            {
                                                _originalSentences[0].effects[i].Finalize(this, _originalSentences[0], _targetText);
                                            }
                                        }
                                        _originalSentences.RemoveAt(0);
                                        _newSentence = true;
                                    }
                                    else
                                    {
                                        _newSentence = false;
                                    }
                                    #endregion
                                }
                                else
                                {
                                    // Delta accumulator was not filled enough, break
                                    break;
                                }
                            }
                            else
                            #region TrimSentence
                            {
                                // No letters in the last sentence
                                _originalSentences.RemoveAt(0);
                            }
                            #endregion
                        }
                        else
                        #region Break
                        {
                            if (_autoPageTurn) _delayCounter = _standardAutoPageTurnDelay;
                            // No sentences, break
                            _pageDone = true;
                            _done = true;
                            break;
                        }
                        #endregion
                    }
                }
            }
            else
            #region WaitForPageTurn
            {

                //Page is done, wait for page swap
                if ((_turnpage || (_autoPageTurn && _delayCounter == 0)) && !_done)
                {
                    /*put front buffer in a page buffer for future rollback?*/
                    _turnpage = false;
                    _pageDone = false;
                    _wrapCheck = true;
                    _deltaAcc = 0;
                    _printEntirePage = false;
                    _pageFrontBuffer = new List<Sentence>();

                }
            }
            #endregion

            #region delayCounter
            if (_delayCounter > 0)
            {
                _delayCounter -= deltatime;
                if (_delayCounter < 0) _delayCounter = 0;
            }
            #endregion

            _targetText.text = "";

            //Tick front buffer
            #region FrontBufferTick
            for (int i = 0; i < _pageFrontBuffer.Count; i++)
            {
                if (_pageFrontBuffer[i].effects != null)
                {
                    for (int j = 0; j < _pageFrontBuffer[i].effects.Count; j++)
                    {
                        _pageFrontBuffer[i].effects[j].Tick(this, _pageFrontBuffer[i], _targetText, deltatime);
                    }
                }

                _targetText.text += _pageFrontBuffer[i].ApplyEffects(_pageFrontBuffer[i].text);
            }
            #endregion
        }
    }

    public bool autoPageTurn
    {
        get
        {
            return _autoPageTurn;
        }
        set
        {
            _autoPageTurn = value;
        }
    }
    public bool done
    {
        get
        {
            return _done;
        }
    }

    public float standardAutoPageTurnDelay
    {
        get
        {
            return _standardAutoPageTurnDelay;
        }
        set
        {
            if (value > 0)
            {
                _standardAutoPageTurnDelay = value;
            }
        }
    }

    public bool pageDone
    {
        get
        {
            return _pageDone;
        }
    }

    public void PageBreak()
    {
        if (_autoPageTurn) _delayCounter = _standardAutoPageTurnDelay;
        _pageDone = true;
    }

    public void Turnpage()
    {
        if (_pageDone)
        {
            _delayCounter = 0;
            _turnpage = true;
        }
    }

    public float currentSentencePrintSpeed
    {
        get
        {
            return 1 / _currentPrintSpeed;
        }
        set
        {
            if (value <= 0) _currentPrintSpeed = 0;
            else _currentPrintSpeed = 1 / value;
        }
    }

    public float delayCounter
    {
        get
        {
            return _delayCounter;
        }
        set
        {
            if (value < 0) _delayCounter = 0;
            else _delayCounter = value;
        }
    }

    public float standardPrintSpeed
    {
        get
        {
            return _standardPrintSpeed;
        }
        set
        {
            if (value <= 0) _standardPrintSpeed = 0;
            else _standardPrintSpeed = 1 / value;
        }
    }

    public void PrintEntirePage()
    {
        _deltaAcc = 0;
        _printEntirePage = true;
    }

    private bool MoveFirstLetter(List<Sentence> source, List<Sentence> target, bool newSentence)
    {
        if (source != null && target != null)
        {
            if (source.Count > 0)
            {
                while (source[0].text.Length == 0)
                {
                    source.RemoveAt(0);
                    if (source.Count == 0)
                    {
                        return false;
                    }
                }
                if (target.Count == 0 || newSentence)
                {
                    target.Add(new Sentence("", source[0].effects));
                }
                target[target.Count - 1].text += source[0].text.Substring(0, 1);
                if (source[0].text.Length == 1)
                {
                    //source.RemoveAt(0);
                    source[0].text = "";
                }
                else
                {
                    source[0].text = source[0].text.Substring(1, source[0].text.Length - 1);
                }
                return true;
            }
        }
        return false;
    }

    private bool CheckWordBreak(char letter, bool checkForPageBreak)
    {
        // Add functionality for sentence breaks, i.e "!?.:" and so on.
        if (letter == ' ') return true;
        return false;
    }

}

[System.Serializable]
public class Sentence
{
    public string text;

    public List<ConversationEffect> effects;

    public Timer timer = null;
    [System.NonSerialized]
    public float counter;

    public Sentence(string text, List<ConversationEffect> effects)
    {
        this.text = text;
        if (effects != null)
        {
            if (effects.Count > 0)
            {
                this.effects = effects;
            }
        }
        else
        {
            this.effects = null;
        }
    }
    public Sentence()
    {
        text = "";
        effects = null;
    }

    public string ApplyEffects(string target)
    {
        string preBuffer = "";
        string midBuffer = target;
        string postBuffer = "";

        if (effects != null)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                preBuffer += effects[i].GetPreTag();
            }
            for (int i = 0; i < effects.Count; i++)
            {
                midBuffer = effects[i].AlterText(midBuffer);
            }
            for (int i = effects.Count - 1; i >= 0; i--)
            {
                postBuffer += effects[i].GetPostTag();
            }
            return preBuffer + midBuffer + postBuffer;
        }
        else
        {
            return target;
        }
    }

}

public abstract class ConversationEffect : ScriptableObject
{
    public abstract string GetPreTag();
    public abstract string AlterText(string source);
    public abstract void Initialize(PrintText controller, Sentence source, Text textRef);
    public abstract void Tick(PrintText controller, Sentence source, Text textRef, float deltatime);
    public abstract void Finalize(PrintText controller, Sentence source, Text textRef);
    public abstract string GetPostTag();
}
