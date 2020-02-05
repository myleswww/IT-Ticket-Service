using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits1 : MonoBehaviour
{
    public TextAsset JsonFile;
    public TextAsset TalkedJsonFile;
    public Texture2D mad;
    public Texture2D happy;
    public Texture2D sad;
    public Texture2D common;
    public AudioClip[] sounds;
    private PlayerController bounds;
    private float maxX = 5f;
    private float minX = -5f;
    private float maxY = 5f;
    private float minY = -5f;



    private void Start()
    {
        bounds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        maxX = bounds.maxX;
        maxY = bounds.maxY;
        minX = bounds.minX;
        minY = bounds.minY;
    }

    private void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
    }

    public TextAsset GetStory()
    {
        return JsonFile;
    }

    public TextAsset GetTalkedStory()
    {
        return TalkedJsonFile;
    }

    public Texture2D GetEmote(string mood)
    {
        if (mood == "mad")
        {
            return mad;
        }
        else if (mood == "happy")
        {
            return happy;
        }
        else if (mood == "sad")
        {
            return sad;
        }
        else
        {
            return common;
        }
    }
}
