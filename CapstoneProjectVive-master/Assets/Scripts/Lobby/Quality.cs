// Controls and sets the video quality settings within the lobby

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Quality : MonoBehaviour {

	public Button Default;
	public Button Lowest;
	public Button Low;
	public Button Medium;
	public Button High;
	public Button Ultra;

	// This will be our blue colour
	private Color c = new Color (0.26f, 0.44f, 1f, 1f);

	void Start () {
		// Get and Set the Quality Level
		int qualityLevel = QualitySettings.GetQualityLevel ();
		SetQualityLevelColor (qualityLevel);
	}

	public void QualityFastest () {
		// "Lowest" Settings
		int level;
		QualitySettings.SetQualityLevel (0);
		ResetButton ();
		level = QualitySettings.GetQualityLevel ();
		if (level == 0) {
			ColorBlock cb = Lowest.colors;
			cb.normalColor = c;
			Lowest.colors = cb;
		}
	}

	public void QualityFast () {
		// "Low" Settings
		int level;
		QualitySettings.SetQualityLevel (1);
		ResetButton ();
		level = QualitySettings.GetQualityLevel ();
		if (level == 1) {
			ColorBlock cb = Low.colors;
			cb.normalColor = c;
			Low.colors = cb;
		}
	}

	public void QualitySimple () {
		// "Med" Settings
		int level;
		QualitySettings.SetQualityLevel (2);
		ResetButton ();
		level = QualitySettings.GetQualityLevel ();
		if (level == 2) {
			ColorBlock cb = Medium.colors;
			cb.normalColor = c;
			Medium.colors = cb;
		}
	}

	public void QualityBeautiful () {
		// "High" Settings
		int level;
		QualitySettings.SetQualityLevel (4);
		ResetButton ();
		level = QualitySettings.GetQualityLevel ();
		if (level == 4) {
			ColorBlock cb = Medium.colors;
			cb.normalColor = c;
			Medium.colors = cb;
		}
	}

	public void QualityFantastic () {
		// "Ultra" Settings
		int level;
		QualitySettings.SetQualityLevel (5);
		ResetButton();
		level = QualitySettings.GetQualityLevel ();
		if (level == 5) {
			ColorBlock cb = Low.colors;
			cb.normalColor = c;
			Ultra.colors = cb;
			Debug.Log ("Ultra");
		}
	}

	public void ResetButton () {
		ColorBlock cb = Default.colors;
		Lowest.colors = cb;
		Low.colors = cb;
		Medium.colors = cb;
		High.colors = cb;
		Ultra.colors = cb;
	}

	public void SetQualityLevelColor (int qualityLevel) {
		int level = qualityLevel;

		switch (level) {
		case 1:
			ColorBlock cb = Lowest.colors;
			cb.normalColor = new Color (0.26f, 0.44f, 1f, 1f);
			Lowest.colors = cb;
			break;
		case 2:
			ColorBlock cc = Low.colors;
			cc.normalColor = new Color (0.26f, 0.44f, 1f, 1f);
			Low.colors = cc;
			break;
		case 3:
			ColorBlock cd = Medium.colors;
			cd.normalColor = new Color (0.26f, 0.44f, 1f, 1f);
			Medium.colors = cd;
			break;
		case 4:
			ColorBlock ce = High.colors;
			ce.normalColor = new Color (0.26f, 0.44f, 1f, 1f);
			High.colors = ce;
			break;
		case 5:
			ColorBlock cg = Ultra.colors;
			cg.normalColor = new Color (0.26f, 0.44f, 1f, 1f);
			Ultra.colors = cg;
			break;
		}
	}
}