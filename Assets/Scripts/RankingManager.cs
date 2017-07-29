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

    private void LoadRankingUI()
    {
        foreach (Transform child in scores.transform)
        {
            Destroy(child.gameObject);
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
        Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        txt.font = ArialFont;
        txt.material = ArialFont.material;
        txt.resizeTextForBestFit = true;
        txt.fontStyle = FontStyle.Normal;
        txt.text = text;
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
        }
    }

    public void InsertNewScore(string name, string time, float timer)
    {
        Ranking newRank = new Ranking();
        newRank.Name = name;
        newRank.Time = time;
        newRank.Timer = timer;
        if (rankings.Count <= 10)
        {
            rankings.Add(newRank);
        }
        else
        {
            float minimun = rankings.Min(m => m.Timer);
            var minRank = rankings.Where(w => w.Timer == minimun && w.Timer < timer).First();
            rankings.Remove(minRank);
            rankings.Add(newRank);
        }
        Save();
    }
}
