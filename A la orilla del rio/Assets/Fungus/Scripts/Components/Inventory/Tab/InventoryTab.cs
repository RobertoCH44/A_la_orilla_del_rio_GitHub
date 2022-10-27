// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Fungus;
// using System.Linq;


// public class Inventory : MonoBehaviour
// {
//     //
//     private Inventory_UI_Control inventoryUI;
//     //
//     private MenuDialog[] menuDialogs;
//     private SayDialog[] sayDialogs;
//     private Target target;

//     //get button OpenInventory
//     public bool buttonPressed = false;

//     //Creo esto para poder agregar mas paginas de ser necesario
//     public enum InventoryPages { NORMAL_ITEMS, KEY_ITEMS };
//     public InventoryPages currentPage;

//     public InventoryItems[] inventoryItems;
//     public InventoryItems[] keyInventoryItems;

//     public ItemSlot[] itemSlots;

//     private Flowchart[] flowcharts;

//     private void Start()
//     {
//         menuDialogs = FindObjectsOfType<MenuDialog>();
//         sayDialogs = FindObjectsOfType<SayDialog>();

//         target = GameObject.FindGameObjectWithTag("Player").GetComponent<Target>();
//         flowcharts = FindObjectsOfType<Flowchart>();

//         inventoryUI = FindObjectOfType<Inventory_UI_Control>();

//         currentPage = InventoryPages.NORMAL_ITEMS;
//     }



//     // Update is called once per frame
//     //     void Update()
//     //     {

//     //         if (Input.GetButtonDown("Inventario"))
//     //         {
//     //             ToggleInventory(!canvasGroup.interactable);
//     //         }
//     // `       
//     //     }

//     /*
//    public void CanvasGroupPressed()
//     {
//         buttonPressed = true;
//         ToggleInventory(!canvasGroup.interactable);

//     }
//     */

//     public void ToggleInventory(bool setting)
//     {
//         //ToggleCanvasGroup(canvasGroup, setting);
//         InitializeItemSlots();

//         /*
//         if (!target.cutsceneInProgress)
//         {
//             target.inDialogue = setting;
//         }
//         */


//         foreach (MenuDialog menuDialog in menuDialogs)
//         {
//             inventoryUI.ToggleCanvasGroup(menuDialog.GetComponent<CanvasGroup>(), !setting);
//         }
//         foreach (SayDialog sayDialog in sayDialogs)
//         {
//             sayDialog.dialogEnabled = !setting;
//             if (setting)
//             {
//                 Time.timeScale = 0;
//             }
//             else
//             {
//                 Time.timeScale = 1;
//             }
//             inventoryUI.ToggleCanvasGroup(sayDialog.GetComponent<CanvasGroup>(), !setting);
//         }
//     }

//     public void InitializeItemSlots()
//     {
//         List<InventoryItems> ownedItems;

//         switch (currentPage)
//         {
//             default:
//                 ownedItems = GetOwnedItems(inventoryItems.ToList());
//                 break;
//             case InventoryPages.KEY_ITEMS:
//                 ownedItems = GetOwnedItems(keyInventoryItems.ToList());
//                 break;
//         }

//         for (int i = 0; i < itemSlots.Length; i++)
//         {
//             if (i < ownedItems.Count)
//             {
//                 itemSlots[i].DisplayItem(ownedItems[i]);
//             }
//             else
//             {
//                 itemSlots[i].ClearItem();
//             }
//         }
//     }

//     public List<InventoryItems> GetOwnedItems(List<InventoryItems> inventoryItems)
//     {
//         List<InventoryItems> ownedItems = new List<InventoryItems>();
//         foreach (InventoryItems item in inventoryItems)
//         {
//             if (item.itemOwned)
//             {
//                 ownedItems.Add(item);
//             }
//         }
//         return ownedItems;
//     }


//     public void CombineItems(InventoryItems item1, InventoryItems item2)
//     {
//         if (item1.combinable == true && item2.combinable == true)
//         {
//             for (int i = 0; i < item1.combinableItems.Length; i++)
//             {
//                 if (item1.combinableItems[i] == item2)
//                 {
//                     foreach (Flowchart flowchart in flowcharts)
//                     {
//                         if (flowchart.HasBlock(item1.succesBlockNames[i]))
//                         {

//                             //ToggleInventory(false);
//                             target.enterDialogue();
//                             flowchart.ExecuteBlock(item1.succesBlockNames[i]);
//                             UpdateInventory();
//                             return;
//                         }
//                     }
//                 }
//             }
//         }
//         foreach (Flowchart flowchart in flowcharts)
//         {
//             if (flowchart.HasBlock(item1.failBlockNames))
//             {
//                 ToggleInventory(false);
//                 target.enterDialogue();
//                 flowchart.ExecuteBlock(item1.failBlockNames);
//             }
//         }
//     }





//     public void SetSelectedInventory(int i)
//     {
//         switch (i)
//         {
//             default:
//                 currentPage = InventoryPages.NORMAL_ITEMS;
//                 break;
//             case 1:
//                 currentPage = InventoryPages.KEY_ITEMS;
//                 break;
//         }
//     }

//     public void ClearInventoryUI()
//     {
//         for (int i = 0; i < itemSlots.Length; i++)
//         {
//             itemSlots[i].ClearItem();
//         }
//     }

//     public void FillItemSlots()
//     {
//         List<InventoryItems> ownedItems;

//         switch (currentPage)
//         {
//             default:
//                 ownedItems = GetOwnedItems(inventoryItems.ToList());
//                 break;
//             case InventoryPages.KEY_ITEMS:
//                 ownedItems = GetOwnedItems(keyInventoryItems.ToList());
//                 break;
//         }

//         for (int i = 0; i < itemSlots.Length; i++)
//         {
//             if (i < ownedItems.Count)
//             {
//                 itemSlots[i].DisplayItem(ownedItems[i]);
//             }
//             else
//             {
//                 itemSlots[i].ClearItem();
//             }
//         }
//     }

//     public void UpdateInventory()
//     {
//         ClearInventoryUI();
//         FillItemSlots();
//     }

//     public void CloseInventory()
//     {
//         ClearInventoryUI();
//     }

// }