using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ink.Runtime;
using System.Collections.Generic;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class BasicInkExample : MonoBehaviour {

    private GameObject GNPC;
    private GameObject player;
    private AudioClip soundClip;
    private Texture2D face;
    private List<string> portrait = new List<string>{ "common" };
    public Image shade;


    void Awake () {
		// Remove the default message
		RemoveChildren();
	}

	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory (GameObject NPC) {
        Cursor.visible = true;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<Animator>().SetBool("Paused", true);
        GNPC = NPC;
        TextAsset textAsset;
        if (NPC.GetComponent<talk>().hasTalked)
        {
            textAsset = NPC.GetComponent<Traits1>().GetTalkedStory();
        }
        else
        {
            textAsset = NPC.GetComponent<Traits1>().GetStory();
        }
		story = new Story (textAsset.text);

        RefreshView();
	}
	
	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView () {
		// Remove all the UI on screen
		RemoveChildren ();
        Image blinder = Instantiate(shade) as Image;
        blinder.transform.SetParent(canvas.transform, false);

        int index = Random.Range(0, GNPC.GetComponent<Traits1>().sounds.Length);
        soundClip = GNPC.GetComponent<Traits1>().sounds[index];
        GNPC.GetComponent<AudioSource>().clip = soundClip;
        GNPC.GetComponent<AudioSource>().Play();

        // Read all the content until we can't continue any more
        while (story.canContinue) {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen
            CreateContentView(text);
		}

        portrait = story.currentTags;
        if(portrait.Count == 0)
        {
            portrait = new List<string> { "common" };
        }
        if (portrait[0] == "common")
        {
            face = GNPC.GetComponent<Traits1>().GetEmote("common");
        }
        else
        {
            face = GNPC.GetComponent<Traits1>().GetEmote(portrait[0]);
        }
        CreateContentImage(face);

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0) {
			for (int i = 0; i < story.currentChoices.Count; i++) {
				Choice choice = story.currentChoices [i];
				Button button = CreateChoiceView (choice.text.Trim ());
				// Tell the button what to do when we press it
				button.onClick.AddListener (delegate {
					OnClickChoiceButton (choice);
				});
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else {
			Button choice = CreateChoiceView("Goodbye!");
			choice.onClick.AddListener(delegate{
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                player.GetComponent<Animator>().SetBool("Paused", false);
                GNPC.GetComponent<talk>().hasTalked = true;
                GNPC.GetComponent<Animator>().SetBool("Animate", true);
                Cursor.visible = false;
                RemoveChildren();
			});
		}
	}

	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton (Choice choice) {
		story.ChooseChoiceIndex (choice.index);
        RefreshView();
	}

    //create image
    void CreateContentImage(Texture2D face)
    {
        RawImage emotion = Instantiate(imagePrefab) as RawImage;
        emotion.texture = face;
        emotion.transform.SetParent(canvas.transform, false);
    }

    // Creates a button showing the choice text
    void CreateContentView (string text) {
		Text storyText = Instantiate (textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent (canvas.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView (string text) {
		// Creates the button from a prefab
		Button choice = Instantiate (buttonPrefab) as Button;
		choice.transform.SetParent (canvas.transform, false);
		
		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text> ();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent <HorizontalLayoutGroup> ();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren () {
		int childCount = canvas.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i) {
			GameObject.Destroy (canvas.transform.GetChild (i).gameObject);
		}
	}

   
	[SerializeField]
	public Story story;

	[SerializeField]
	private Canvas canvas;

    // UI Prefabs
    [SerializeField]
    private RawImage imagePrefab;
    [SerializeField]
	private Text textPrefab;
	[SerializeField]
	private Button buttonPrefab;
}