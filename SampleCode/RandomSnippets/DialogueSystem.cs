/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
   CMDG LISA ENGELEN
	PORTFOLIO: https://lisaengelen-portfolio.tumblr.com/
	*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum DialogueType
{
	Monologue,
	Dialogue,
	Quest
}

public class DialogueSystem : MonoBehaviour {
	public static DialogueSystem Instance { get; set; }

	#region Variables
	[HideInInspector]
	public string npcName;
	public List<string> dialogueLines = new List<string>();

	public GameObject dialoguePanel;
	public int dialogueType;
	private Button continueButton;
	private Text dialogueText, nameText;
	private int dialogueIndex;
	private DialogueType type;
	#endregion

	#region Methods
	void Start () 
	{
		continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
		dialogueText = dialoguePanel.transform.Find("Text").GetComponent<Text>();
		nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<Text>();
		continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
		dialoguePanel.SetActive(false);

		if(Instance != null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	public void AddNewDialogue(string[] lines, string _npcName, int _dialogueType)
	{
		npcName = _npcName;
		dialogueType = _dialogueType;
	
		switch (dialogueType)
		{
			case 1:
				type = DialogueType.Monologue;
				break;
			case 2:
				type = DialogueType.Dialogue;
				break;
			case 4:
				type = DialogueType.Quest;
				break;
		}

		dialogueIndex = 0;
		dialogueLines = new List<string>(lines.Length);
		dialogueLines.AddRange(lines);

		switch (type)
		{
			case DialogueType.Monologue:
				CreateMonologue();
				break;
			case DialogueType.Dialogue:
				CreateDialogue();
				break;
			case DialogueType.Quest:
				CreateQuest();
				break;
		}
	}

	public void CreateMonologue()
	{
		dialogueText.text = dialogueLines[dialogueIndex];
		nameText.text = npcName;
		dialoguePanel.SetActive(true);
	}

	public void CreateDialogue()
	{

	}

	public void CreateQuest()
	{

	}

	public void ContinueDialogue()
	{
		if (dialogueIndex < dialogueLines.Count - 1)
		{
			dialogueIndex++;
			dialogueText.text = dialogueLines[dialogueIndex];
		}
		else
			dialoguePanel.SetActive(false);
	}
	#endregion
}
