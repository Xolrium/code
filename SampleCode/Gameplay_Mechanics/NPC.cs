/* CMDG ERNESTO HERNANDEZ
	PORTFOLIO: http://ernestohh.weebly.com/
   CMDG LISA ENGELEN
	PORTFOLIO: https://lisaengelen-portfolio.tumblr.com/

	*/
using UnityEngine;

public class NPC : MonoBehaviour {

	#region Variables
	public string[] dialogue;
	public string npcName;
	public int dialogueType;
	#endregion

	#region Unity Methods
	public void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
			DialogueSystem.Instance.AddNewDialogue(dialogue, npcName, dialogueType);

	}
	#endregion
}
