using UnityEngine;
using UnityEngine.UI;

public class AISwitchBtn : MonoBehaviour {
	
	bool isAiMode = true;
	private Text text;
	private string playerModeText = "P";
	private string aiModeText = "AI";

	void Start () {
		Group.aimode = isAiMode;	
		
		text = transform.GetChild(0).GetComponent<Text>();
	}
	
	public void Switch(){
		isAiMode = !isAiMode;
		Group.aimode = isAiMode;	
		
		if(Group.aimode == true)
			text.text = aiModeText;
		else
			text.text = playerModeText;
	}
}
