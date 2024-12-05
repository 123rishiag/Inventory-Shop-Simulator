# Inventory-Shop-Simulator

This project is a shop and inventory management system with interactive gameplay mechanics. Players can trade items, gather resources, and manage inventory constraints like weight and rarity. The system is designed using **Service Locator**, **Dependency Injection**, **Model-View-Controller (MVC)**, and the **Observer Pattern**, ensuring modularity and scalability.

---

## Features

- **Shop System**:
  - Tabs for Materials, Weapons, Consumables, and Treasure.
  - Dynamic display of item buying/selling prices.
  - Confirmation popups to prevent accidental purchases.

- **Inventory Management**:
  - Weight limit dynamically updates as items are added/removed.
  - Tracks attributes like rarity, value, and type for all items.

- **Dynamic Resource Gathering**:
  - Items are randomly generated based on inventory value and rarity.
  - Resource gathering is disabled when the inventory is full.

- **Gameplay Enhancements**:
  - Immersive sound effects for actions like buying and selling.
  - Smooth transitions between UI panels and updates.

---

## Architectural Overview

The system uses **Service Locator**, **Dependency Injection**,  **MVC**, and the **Observer Pattern**. Below is the block diagram illustrating the architecture:

![Architectural Overview](Documents\block_diagram.png)

---

### Services and Components

1. **Game Service**: The central service responsible for initializing all other services using a private Service Locator.
   
2. **Event Service**: Implements the Observer Pattern for event-driven communication. It manages all events acting as mediators between services.
   - **Controller**: Inherits from EventController to manage event registrations and notifications.

3. **Sound Service**: Handles immersive sound effects for actions like buying/selling and resource gathering.
   - **Scriptable Objects**: 
      - `SoundScriptableObject` centralizes sound configurations.

4. **UI Service**: Handles user interactions and dynamically updates UI elements like inventory and shop panels.
   - **Controller**: Manages user interactions and dynamic UI updates.
   - **Model**: Placeholder for future enhancements.
   - **View**: Handles rendering of UI panels dynamically.

5. **Item Service**: Manages item-related data and logic, including creation, deletion, and UI updates.
   - **Controller**: Handles item-related actions (e.g., create, delete).
   - **Model**: Stores runtime item data (e.g., weight, rarity).
   - **View**: Manages item visual representation in the UI.
   - **Scriptable Objects**: 
     - `ItemScriptableObject`: Centralized data for item properties.
     - `ItemDatabaseScriptableObject`: Manages a collection of items for use in the game.

6. **Inventory Service**: Controls inventory constraints like weight and item slots, while providing real-time updates.
   - **Controller**: Handles inventory constraints like weight and slots.
   - **Model**: Tracks inventory contents and runtime updates.
   - **View**: Displays inventory items dynamically.
   - **Scriptable Object**: `InventoryScriptableObject` for inventory-specific configurations.

7. **Shop Service**: Manages shop functionalities like categorized navigation and buying/selling items.
   - **Controller**: Manages buying/selling logic and navigation.
   - **Model**: Tracks shop inventory and item availability.
   - **View**: Dynamically renders shop items.
   - **Scriptable Object**: `ShopScriptableObject` for shop data.

8. **Utilities**:
   - `ReadOnlyDrawer`: Custom inspector drawer for readonly properties in the Unity Editor.

---

## Events

Here are the key events used in the system and their descriptions:

1. **Item Service**:
   - `OnCreateItemEvent`: Creates and returns an item controller for the specified section.
   - `OnShowItemEvent`: Displays items of a specific type in a section (Shop/Inventory).
   - `OnDestroyItemEvent`: Removes an item from a section (Shop/Inventory).

2. **Shop Controller**:
   - `OnShopAddItemEvent`: Adds an item to the shop when selling from inventory.
   - `OnBuyItemEvent`: Triggers the item-buying logic.

3. **Inventory Controller**:
   - `OnInventoryAddItemEvent`: Adds an item to inventory when buying from the shop.
   - `OnSellItemEvent`: Triggers the item-selling logic.

4. **UI Controller**:
   - `OnBuySellButtonClickEvent`: Handles buy/sell button clicks and their respective actions.
   - `OnShopUpdatedEvent`: Updates the Shop UI dynamically.
   - `OnInventoryUpdatedEvent`: Updates the Inventory UI dynamically with weight and slots.
   - `OnItemClickEvent`: Displays an item’s UI details when clicked.
   - `OnPopupNotificationEvent`: Triggers popups for notifications.
   - `OnSetButtonInteractionEvent`: Enables/disables buttons dynamically.
   - `OnCreateItemButtonViewEvent`: Instantiates and returns an item button prefab.
   - `OnCreateMenuButtonViewEvent`: Instantiates and returns a menu button prefab.

5. **Sound Service**:
   - `OnPlaySoundEffectEvent`: Plays a specific sound effect (e.g., transaction, error).

---

## Script and Asset Hierarchy

1. **Scripts**:
   - Organized by feature: `Editor`, `Event`, `Main`, `UI`, `Inventory`, `Shop`, `Items`, `Sound`, etc.

2. **Assets**:
   - **Sounds**: From Outscal.
   - **Prefabs**: Self-created for UI components.
   - **Art**:
     - `Art/UI Icons/Item`: Organized into folders for each item type (e.g., Materials, Weapons, Consumables, Treasure).
     - Icons were generated via ChatGPT for consistency.

---

## Development Workflow

### Branches
1. **feature/game-screen-ui**: Initial UI framework for Inventory and Shop panels.
2. **feature/item-properties**: Created Scriptable Objects for item configurations.
3. **feature/item-mvc**: Modularized item logic into MVC.
4. **feature/inventory-mvc**: Implemented MVC for inventory management.
5. **feature/shop-mvc**: Developed shop system with categorized navigation.
6. **feature/service-locator-di**: Centralized services with Dependency Injection.
7. **feature/buying-selling-items**: Integrated buying/selling mechanics.
8. **feature/gather-resources**: Added dynamic resource gathering mechanics.
9. **feature/item-ui-enhancements**: Improved UI clarity with detailed feedback.
10. **feature/event-service**: Implemented the Observer Pattern for communication.
11. **feature/sound-service**: Added sound effects for gameplay immersion.
12. **feature/ui-mvc**: Enhanced UI structure using MVC.
13. **feature/gameplay-enhancements**: Polished gameplay with additional features.
14. **feature/documentation**: Prepared the README and added Block diagrams.

---

## Video Demo

[Watch the Gameplay Demo](https://www.loom.com/share/demo-link)

---

## Play Link

[Play the Game](https://outscal.com/narishabhgarg/game/play-shop-inventory)

---