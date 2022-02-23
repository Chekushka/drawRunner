using System.Collections;
using Character;
using UnityEngine;

public class DrawTrigger : MonoBehaviour
{
   [SerializeField] private Item itemNeededToUse;
   
   private void OnTriggerEnter(Collider other)
   {
      if(!other.gameObject.CompareTag("Character")) return;
      var characterMovement = other.GetComponent<CharacterMovement>();
      characterMovement.SetToIdle();
      if (characterMovement.isPlayerCharacter)
      {
         var itemDrawing = FindObjectOfType<ItemDrawing>();
         itemDrawing.gameObject.SetActive(true);
         itemDrawing.EnableDrawing(itemNeededToUse);
      }
      else
      {
         var enemy = other.GetComponent<EnemyDrawing>();
         StartCoroutine(WaitForDraw(characterMovement, enemy));
      }
   }
   
   private IEnumerator WaitForDraw(CharacterMovement characterMovement, EnemyDrawing enemy)
   {
      yield return new WaitForSeconds(Random.Range(enemy.GetMinDrawTime(), enemy.GetMaxDrawTime()));
      if(enemy.GetEnemyDrawResult())
         characterMovement.StartCorrectAction(itemNeededToUse);
      else
         characterMovement.StartFailAction(itemNeededToUse);
   }
}

public enum Item
{
   Wings,
   Helmet,
   JetPack
}