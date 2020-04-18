using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// This is a super bare bones example of how to play and display a ink story in Unity.

[System.Serializable]
public class TagFoundEvent : UnityEvent<string> { }

[System.Serializable]
public class TextTagFoundEvent : UnityEvent<string> { }

[System.Serializable]
public class WriterOpenEvent : UnityEvent<InkWriter> { };

[System.Serializable]
public class WriterCloseEvent : UnityEvent<InkWriter> { };

public class InkWriter : MonoBehaviour {

	public static InkWriter main;
	public bool mainWriter = true; // only one inkwriter should be main
	[SerializeField]
	private InkStoryObject inkStoryObject;
	public Story story;

	[SerializeField]
	private Canvas writerCanvas;
	[SerializeField]
	private CanvasGroup writerCanvasGroup;

	[SerializeField]
	private GameObject textArea;

	// UI Prefabs
	[SerializeField]
	private TextMeshProUGUI textPrefab;
	[SerializeField]
	private Button buttonPrefab;

	public ScrollRect scrollView;
	private bool autoScroll = false;

	public Image portrait;
	public Image background;
	public Button continueButton;
	public bool clickToContinue = false;
	public bool hideWhenFinished = true;

	public bool clearOnNewStory = true;
	private bool continueStory = true;

	private Coroutine refreshCoroutine;
	public TagFoundEvent tagEvent;

	public TextTagFoundEvent textTagEvent;

	public TMPLinkClickedEvent linkClickedEvent;

	// literally only used once during loading :P
	public bool writerVisible = false;
	public WriterOpenEvent onWriterOpen;
	public WriterCloseEvent onWriterClose;

	private List<Button> cachedButtons = new List<Button> { };
	private Button pickedButton;
	private string lastText;
	private string lastSaveableTags;

	// Make this array larger as more tags are added; currently portrait is saved in 0 and background in 1
	private string[] tagsToSave = new string[2] { "", "" };
	char delimiterLeft = '<';
	char delimiterRight = '>';
	private bool loading;

	void Awake () {

		if (main == null && mainWriter) {
			main = this;
		} else if (main != null && mainWriter) {
			Debug.LogWarning ("Cannot have two main inkWriters!");
		}
	}

	void Start () {
		RemoveChildren ();
		//Invoke ("StartStory", 0.1f);
		if (!clickToContinue) {
			ActivateContinueButton (false);
		}
		continueButton.onClick.AddListener (OnClickContinueButton);
		// Find canvas group of writer canvas
		if (writerCanvas != null) {
			writerCanvasGroup = writerCanvas.GetComponent<CanvasGroup> ();
			writerCanvas.gameObject.SetActive (true);
		}
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory () {
		HideCanvas (false);

		if (clearOnNewStory) {
			ClearChildren ();
		}

		lastText = "";
		lastSaveableTags = "";
		story = new Story (inkStoryObject.inkJsonAsset.text);
		inkStoryObject.Init ();
		string savedJson = PlayerPrefs.GetString (inkStoryObject.storyName + "savedInkStory");
		if (savedJson != "") {
			story.state.LoadJson (savedJson);
			Debug.Log ("Loading story");
			lastText = (string) InkWriter.main.story.variablesState["lastSavedString"];
			lastSaveableTags = (string) InkWriter.main.story.variablesState["lastSavedTags"];
			Debug.Log ("Tags at load point: " + lastSaveableTags);
			loading = true;
		} else { // no saved json -> go to "start" knot
			if (mainWriter) { GoToKnot ("start"); };
		}
		story.variablesState["debug"] = false;
		RefreshView ();
	}

	[EasyButtons.Button]
	public void ResetStory () {
		lastText = "";
		for (int i = 0; i < tagsToSave.Length; i++) {
			tagsToSave[i] = "";
		}
		if (!Application.isPlaying) {
			PlayerPrefs.SetString (inkStoryObject.storyName + "savedInkStory", "");
		} else {
			int childCount = textArea.transform.childCount;
			for (int i = childCount - 1; i >= 0; --i) {
				GameObject.Destroy (textArea.transform.GetChild (i).gameObject);
			}
			PlayerPrefs.SetString (inkStoryObject.storyName + "savedInkStory", "");
			DisablePortraits ();
			DisableBackgrounds ();
			StartStory ();
		}
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
		continueStory = true;
		if (clickToContinue) { ActivateContinueButton (true); };
		if (refreshCoroutine == null) {
			refreshCoroutine = StartCoroutine (RefreshCoroutine ());
		} else {
			StopCoroutine (refreshCoroutine);
			refreshCoroutine = StartCoroutine (RefreshCoroutine ());
		}
	}

	IEnumerator RefreshCoroutine () {

		// if we loaded, display the last text
		if (loading) {
			LoadTags ();
			CreateContentView (lastText, textArea.transform);
			loading = false;
			lastText = "";
			lastSaveableTags = "";
		}

		// Read all the content until we can't continue any more
		while (story.canContinue) {
			// Continue gets the next line of the story
			yield return new WaitUntil (() => continueStory);
			CreateText ();
		}

		// continue click after last text
		yield return new WaitUntil (() => continueStory);
		// Disable continue button
		if (clickToContinue) { ActivateContinueButton (false); };
		// Display all the choices, if there are any!
		if (story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices[i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice, button);
				});
				// add button to cached button dictionary
				cachedButtons.Add (button);
			}

		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
			if (mainWriter) {
				//SaveStory ();
				if (hideWhenFinished) {
					HideCanvas (true);
				} else { // PROBABLY CHANGE THIS LOL.
					/*Button choice = CreateChoiceView ("This is a terrible bug. Everything is broken. Click here to restart.");
					cachedButtons.Add (choice);
					choice.onClick.AddListener (delegate {
						ResetStory ();
					});*/
					ClearChildren ();
				}
			} else {
				if (hideWhenFinished) {
					HideCanvas (true);
				}
			}
		}
		/*Button choice2 = CreateChoiceView ("Reset story");
		choice2.onClick.AddListener (delegate {
			ResetStory ();
		});
		cachedButtons.Add (choice2);*/
		// Move scrollbar
		MoveScrollBar (true);
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice, Button button) {
		lastText = "";
		story.ChooseChoiceIndex (choice.index);
		pickedButton = button;
		RefreshView ();
	}
	void OnClickContinueButton () {
		continueStory = true;
	}

	void CreateText () {
		string text = story.Continue ();
		// This removes any white space from the text.
		text = text.Trim ();
		if (text != "") {
			text += "\n\n";
			// Display the text on screen!
			if (story.currentTags.Count > 0) {
				SaveTags (story.currentTags);
				if (TagParser (story.currentTags)) {
					CreateContentView (text, textArea.transform);
				}
			} else {
				CreateContentView (text, textArea.transform);
			};
			lastText += text;
			if (clickToContinue) {
				continueStory = false;
				ActivateContinueButton (true);
			}
		}
	}
	void SaveTags (List<string> tags) {
		foreach (string tag in tags) {
			if (tag.Contains ("portrait")) {
				tagsToSave[0] = tag;
				//Debug.Log ("Saving tag: " + tag);
			}
			if (tag.Contains ("background")) {
				tagsToSave[1] = tag;
				//Debug.Log ("Saving tag: " + tag);
			}
		}
	}
	void SerializeSavedTags () { // save the tags that need to be saved/loaded, e.g. character portrait showing, background.
		// clear tags, we only want the latest

		lastSaveableTags = "";
		if (tagsToSave.Length > 0) {
			foreach (string tag in tagsToSave) {
				if (tag.Contains ("portrait")) {
					lastSaveableTags += delimiterLeft;
					lastSaveableTags += tag;
					lastSaveableTags += delimiterRight;
				}
				if (tag.Contains ("background")) {
					lastSaveableTags += delimiterLeft;
					lastSaveableTags += tag;
					lastSaveableTags += delimiterRight;
				}
			}
		};
		//Debug.Log ("Saving tags: " + lastSaveableTags);
	}
	void LoadTags () {
		// These are the symbols that delimit each individual string, by default <> - these can't be used in the body of the string itself! (you can't use [] btw, unless you do some clever regex-canceling magic)
		//Debug.Log ("Loading tags: " + lastSaveableTags);
		List<string> checkString = new List<string> { };

		// Checks for anything between delimiter brackets and then sends the first match onward.
		Regex brackets = new Regex (delimiterLeft + ".*?" + delimiterRight);
		MatchCollection matches = brackets.Matches (lastSaveableTags);
		if (matches.Count > 0) {
			for (int i = 0; i < matches.Count; i++) {
				// Trim out the actual string
				string ReqText = matches[i].Value.Trim (new Char[] { delimiterLeft, delimiterRight, ' ' });
				checkString.Add (ReqText);
				//Debug.Log ("Adding to parser list: " + ReqText);
				// Also re-add to saved tags!
			}
		};
		//Debug.Log ("Final parser list: " + checkString[0] + checkString[1]);
		SaveTags (checkString);
		TagParser (checkString);
	}

	bool TagParser (List<string> tags) {
		/*foreach (string tag in tags) {
			Debug.Log ("Tagparser content " + tag);
		}*/
		//Debug.Log (tags[0]);
		// parse tags, and return true or false if the story should continue

		if (tags.Contains ("endStory")) {
			PauseStory ();
			return false;
		}
		if (tags.Contains ("saveStory")) {
			SaveStory ();
		}
		if (tags.Contains ("changeportrait")) {
			DisablePortraits ();
		}
		if (tags.Contains ("changebackground")) {
			DisableBackgrounds ();
		}
		// invoke all the tags!
		foreach (string tag in tags) {
			tagEvent.Invoke (tag);
		}

		return true;
	}

	void DisablePortraits () {
		int childCount = portrait.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			portrait.transform.GetChild (i).gameObject.SetActive (false);
		}
		//Debug.Log ("Disabled portraits");
	}
	void DisableBackgrounds () {
		int childCount = background.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			background.transform.GetChild (i).gameObject.SetActive (false);
		}
		//Debug.Log ("Disabled backgrounds");
	}

	void ActivateContinueButton (bool activate) {
		continueButton.gameObject.SetActive (activate);
	}

	[EasyButtons.Button]
	public void SaveStory () {

		InkWriter.main.story.variablesState["lastSavedString"] = lastText;
		SerializeSavedTags ();
		InkWriter.main.story.variablesState["lastSavedTags"] = lastSaveableTags;
		PlayerPrefs.SetString (inkStoryObject.storyName + "savedInkStory", story.state.ToJson ());
		Debug.Log ("Tags at save point: " + lastSaveableTags);
	}

	[EasyButtons.Button]
	public void LoadStory () {
		StartStory ();
	}
	void PauseStory () {
		SaveStory ();
		//RemoveChildren ();
		Button choice = CreateChoiceView ("Continue Story");
		choice.onClick.AddListener (delegate {
			StartStory ();
		});
	}

	public void GoToKnot (string knot) {
		if (clearOnNewStory) {
			ClearChildren ();
		}
		lastText = "";
		story.ChoosePathString (knot);
		HideCanvas (false);
		RefreshView ();
	}

	void ClearChildren () {
		int childCount = textArea.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (textArea.transform.GetChild (i).gameObject);
		}
	}

	// Creates a piece of text
	void CreateContentView (string text, Transform parent) {
		TextMeshProUGUI storyText = Instantiate (textPrefab) as TextMeshProUGUI;
		storyText.text = text;
		storyText.transform.SetParent (parent, false);
		// Find the text link listener and listen to it
		TMP_LinkWatcher lnkWtcher = storyText.gameObject.GetComponent<TMP_LinkWatcher> ();
		lnkWtcher.linkClickedEvent.AddListener ((arg) => linkClickedEvent.Invoke (arg));
	}
	public void DebugAThing (string debugtext) {
		Debug.LogWarning (debugtext);
	}
	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (textArea.transform, false);
		// Gets the text from the button prefab
		TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		//HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup> ();
		//layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		/*int childCount = textArea.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (textArea.transform.GetChild (i).gameObject);
		}*/
		foreach (Button btn in cachedButtons) {
			if (btn != pickedButton) {
				GameObject.Destroy (btn.gameObject);
			};
		}
		if (pickedButton != null) {
			pickedButton.targetGraphic.color = pickedButton.colors.highlightedColor;
			pickedButton.interactable = false;
		};
		cachedButtons.Clear ();
		pickedButton = null;
	}

	void MoveScrollBar (bool move) {
		autoScroll = move;
	}

	public void HideCanvas (bool hide) {
		writerVisible = !hide;
		//GameManager.instance.PauseGame (!hide);
		writerCanvasGroup.alpha = hide ? 0f : 1f;
		writerCanvasGroup.interactable = !hide;
		writerCanvasGroup.blocksRaycasts = !hide;
		if (hide) {
			onWriterClose.Invoke (this);
		} else {
			onWriterOpen.Invoke (this);
		}
	}

	void Update () {
		if (scrollView != null) {
			if (scrollView.verticalNormalizedPosition > 0f && autoScroll) {
				scrollView.verticalNormalizedPosition = -0.01f;
			} else if (scrollView.verticalNormalizedPosition <= 0f && autoScroll) {
				autoScroll = false;
			}
		}
	}
}