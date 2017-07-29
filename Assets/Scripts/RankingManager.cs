using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    private List<Ranking> rankings;
    private GridLayoutGroup scores;

    void Awake()
    {
        //rankings = new List<Ranking>();
        //for (int i = 0; i < 10; i++)
        //{
        //    Ranking ranking = new Ranking();
        //    ranking.Time = i.ToString();
        //    ranking.Name = i.ToString();
        //    rankings.Add(ranking);
        //}
        //Save();
        Load();
        scores = GetComponentInChildren<GridLayoutGroup>();
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
}
