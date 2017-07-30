using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    private List<Ranking> rankings;
    private GridLayoutGroup scores;

    void Awake()
    {
        Load();
        scores = GetComponentInChildren<GridLayoutGroup>();
        LoadRankingUI();
    }

    public void LoadRankingUI()
    {
        foreach (Transform child in scores.transform)
        {
            Destroy(child.gameObject);
        }
        if (rankings != null && rankings.Count > 5)
        {
            rankings = rankings.OrderByDescending(o => o.Timer).Take(5).ToList();
        }
        foreach (var ranking in rankings)
        {
            createRankingRow(ranking.Name);
            createRankingRow(ranking.Time);
        }
    }

    private void createRankingRow(string text)
    {
        GameObject obj = new GameObject();
        obj.transform.SetParent(scores.transform, false);
        obj.layer = LayerMask.NameToLayer("UI");
        Text txt = obj.AddComponent<Text>();
        Font customFont = (Font)Resources.Load("Font/showcardGothic");
        txt.font = customFont;
        txt.material = customFont.material;
        txt.resizeTextForBestFit = true;
        txt.fontStyle = FontStyle.Normal;
        txt.text = text;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/data.dat");
        bf.Serialize(file, rankings);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/data.dat", FileMode.Open);
            rankings = (List<Ranking>)bf.Deserialize(file);
            file.Close();
            rankings = rankings.OrderByDescending(o => o.Timer).Take(5).ToList();
        }
        else
        {
            rankings = new List<Ranking>();
        }
    }

    public void InsertNewScore(string name, string time, float timer)
    {
        Ranking newRank = new Ranking();
        newRank.Name = !string.IsNullOrEmpty(name) ? name : string.Empty;
        newRank.Time = !string.IsNullOrEmpty(time) ? time : string.Empty;
        newRank.Timer = timer;
        if (rankings == null)
        {
            Load();
            if (rankings == null)
            {
                rankings = new List<Ranking>();
            }
        }
        rankings.Add(newRank);
        if (rankings.Count > 5)
        {
            rankings = rankings.OrderByDescending(o => o.Timer).Take(5).ToList();
        }
        Save();
    }
}
