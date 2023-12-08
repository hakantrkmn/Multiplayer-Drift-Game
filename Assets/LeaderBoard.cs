using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class LeaderBoard : MonoBehaviour
{

    public ScorePanel scorePrefab;

public Transform content;

    public List<ScorePanel> scorePanels;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Refresh),1f,1f);
    }

   public void Refresh()
   {

         var sortedList = (from player in PhotonNetwork.PlayerList orderby player.GetScore() descending select player).ToList();

         for (int i = scorePanels.Count; i < sortedList.Count; i++)
         {
            var panel = Instantiate(scorePrefab,content);
            scorePanels.Add(panel.GetComponent<ScorePanel>());
         }


         for (int i = 0; i < sortedList.Count; i++)
         {
            scorePanels[i].SetPanel(sortedList[i].NickName , sortedList[i].GetScore());
         }
   }

   /// <summary>
   /// Update is called every frame, if the MonoBehaviour is enabled.
   /// </summary>
   void Update()
   {
       content.gameObject.SetActive(Input.GetKey(KeyCode.Tab));
   }
}
