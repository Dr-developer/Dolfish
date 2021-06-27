using System.Threading.Tasks;
using FiroozehGameService.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace Handlers
{
   public  static  class GsLiveHandler 
   {
      public static async Task TakeTurn(int whoIsTurn, string category, string name, Sprite cardImag,
         string sendCategory,string PlayeId)
      {
         var data = new MultiPlayerData
         {
            whoTurn = whoIsTurn,
            category = category,
            cardName = name,
            cardImage = cardImag,
            sendCateGory = sendCategory

         };
         var dataToSend = JsonConvert.SerializeObject(data);
         if (GameService.GSLive.IsTurnBasedAvailable())
         {
            await GameService.GSLive.TurnBased.TakeTurn(dataToSend, PlayeId);
         }
      }
   }

}