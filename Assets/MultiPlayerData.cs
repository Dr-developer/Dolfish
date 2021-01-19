using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class MultiPlayerData
{
  [JsonProperty("0")] public int whoTurn;
  [JsonProperty("1")] public string category;
  [JsonProperty("2")] public string sendCateGory;
  [JsonProperty("3")] public string cardName;
  [JsonProperty("4")] public Text[] changesTexts;
  [JsonProperty("5")] public string changedCateGory;
  

}
