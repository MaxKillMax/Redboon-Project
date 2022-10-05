Store - the first part of the task.
____
### Hand
- Hand is responsible for what is in the player's hand (what item)
### Item
- Item stores data about its value and image
- ItemPanel displays this and handles clicks on the item
- ItemFactory creates items
- ItemData is used by game designers to customize items
### Player
- Inventory keeps a list of items from the player. It does not add or remove anything from itself yet :) Also, Inventory is used to fill the inventory of the store
### Store
- Store is the controller of the entire store. Confirms and verifies purchases and sales, initializes items. It's pretty dirty unfortunately.
### Slot
- Slot is the item's storage. Everything.
### UI
- TradeWindow is just an empty window that allows you to go back (well, as if all this happens inside this window)
### Wallet
- Wallet is a store of money. They can be created many, combined. In theory, it is necessary to make a global single wallet. But really it all depends on the game.
### Test
- Test is a layer that needs to be removed if this game is to be developed. This script opens the shop :)