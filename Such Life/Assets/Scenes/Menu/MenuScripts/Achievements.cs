using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Achievements : MonoBehaviour
{

    public class Achievement
    {
        public int category;
        public string name;
        public string description;
        public bool isObtained;
    }

    public Dictionary<string, Achievement> achievementTracker = new Dictionary<string, Achievement> 
    {
        // insert achievements here
        {"Play the game!", new Achievement {category=1, name="Play the game!", description="TBD", isObtained=true }  },
        {"Vanquish a Slimmer!", new Achievement {category=2, name="Vaniquish a Slimmer!", description="TBD", isObtained=false }  },
        {"Test Achievement1", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=true } },
        {"Test Achievement2", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement3", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement4", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement5", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement6", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=true } },
        {"Test Achievement7", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=true } },
        {"Test Achievement8", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement9", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=true } },
        {"Test Achievement10", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement11", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement12", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } },
        {"Test Achievement13", new Achievement{category=0, name="Test Achievement", description="TBD", isObtained=false } }
    };

    public GameObject achievementsPrefab;
    public GameObject[] achievementPages;
    public GameObject[] achievementContents;
    public Button[] tabButtons;

    private int currentPage = 0;

    void Start()
    {
        // Instantiate achievement prefabs for each achievement on each page
        float achievementsPerPage = achievementsPrefab.GetComponent<RectTransform>().rect.height;
        for (int i = 0; i < achievementContents.Length; i++)
        {
            int y = 0;
            foreach (KeyValuePair<string, Achievement> kvp in achievementTracker)
            {
                if (kvp.Value.category == i)
                {
                    GameObject achievement = Instantiate(achievementsPrefab, achievementContents[i].transform);
                    Text achievementText = achievement.GetComponentInChildren<Text>();
                    if (achievementText != null)
                    {
                        achievementText.text = kvp.Key;
                        if (kvp.Value.isObtained)
                        {
                            achievementText.color = new Color(231/255.0f, 175/255.0f, 27/255.0f);
                        }
                        else
                        {
                            achievementText.color = new Color(140/255.0f, 106/255.0f, 18/255.0f);
                        }
                        // Position the achievement below the previous one
                        achievement.transform.position -= new Vector3(0, y, 0);
                    }
                    y += 10;
                }
            }
        }
    }

    public void SetCurrentPage(int pageIndex)
    {
        currentPage = pageIndex;
        for (int i = 0; i < achievementPages.Length; i++)
        {
            achievementPages[i].SetActive(i == currentPage);
        }
    }

    public void OnTabButtonClick(int pageIndex)
    {
        SetCurrentPage(pageIndex);
    }

    public void setAchievement(string request)
    {
        if (achievementTracker.ContainsKey(request))
        {
            achievementTracker[request].isObtained = true;
        }
    }

}