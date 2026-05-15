# GDIM33 Vertical Slice
## Milestone 1 Devlog
To be clear before I begin: my state machine IS my visual scripting graph (titled `UIController`), which is being used to toggle between the journal and the play screen.

In my [Project Breakdown](https://github.com/user-attachments/files/27191920/GDIM.33.Breakdown.-.Milestone.1.pdf), I explain how when the journal is inactive, the player should be able to click anywhere on the screen except for the "Toggle Journal" button in order to advance the dialogue line, and when the journal is active, the player should be able to use the "Back" and "Next" buttons to see all pages of the journal. In practice, this means that when the state is set to "Journal On", the journal UI is set to active, and the `Dialogue Box` is set to inactive. This transition/state change is triggered by the "Toggle Journal" button using the Unity Event system. Additionally, with the "Back" and "Next" buttons enabled, their `OnClick()` is defined using custom Unity events in the VS graphs that check for the index of the current page, disable that UI, update the current page, then set that page to active. 

Furthermore, in order to display the correct text in the journal, the `Player` class holds references to the UI that the state machine enables/disables, and updates the text in scripts based on options that the players select in the dialogue system.

The dialogue system, which is utilized when the journal is _inactive_ (again, called through the "Toggle Journal" button), is handled almost entirely by the `DialogueController` script. By checking the mouse position, it ensures the mouse isn't over the "Toggle Journal" button, while also checking the state to ensure the journal is inactive. Then, when a branching dialogue is selected, it checks if it holds a reference to a Recollection node, which would then contain potential data to be stored in the Journal's UI when it is next toggled using the state machine. 



## Milestone 2 Devlog
### General Notes
My complicating feature is sanity, which while it is completely functional right now, there is not enough content to really showcase it. You'll notice that the first two "intake" questions raise the sanity by 5 (as a sort of "freebie"), while depending on your answer for "dream or "memory" for the last question, it will either add or subtract 5 points. You'll notice it can't go over 100. Unfortunately, because even if you get the sorting wrong, 5 + 5 - 5 is still 5, there is no way to drain the sanity down, which is what will trigger a lose condition once I have more content. However, the sanity is in a trackable state that can be monitered to trigger effects (like VFX, later on) and the lose-condition I mentioned once more content is available. 

### Before Coding
Since I already described my complicating factor in my W5 devlog, I will be describing the journal system I will be implementing. 
1. Create a dialogue system in which selecting the branching options result in different information saved in game logic
   - have a way to determine whether a branching dialogue node contains information that should be saved to the game logic (`if (recollection != null)`)
   - if it must be saved, be able to access relevant information (that changes depending on what branching option was chosen) and save it to game logic
     - save what option was chosen and have it reference a recollection version within the `RecollectionNode` that is saved to the `DialogueNode`
     - take the correct recollection version from within the correct `RecollectionNode` and save it to `_recollections` list in `Player`
2. Create a journal menu that can be accessed by the player, in which they can flip between the pages/screens
   - make a button to open the journal
     - make a button that triggers that toggled the a journal UI on and the not-journal UI off by setting a reference to the journal to active and everything else to inactive
   - create journal UI
     - create a panel that contains two text boxes to represent the left and rise sides of a page (also make this a prefab that can be instantiated from within the `Player` class if we need more pages)
     - add a close button that calls on the same toggle logic as the "open journal" button
     - add a next and back button that toggle between a list of "page" game objects, with the current index being set to active     
3. Store saved information in the journal, with two entries per leaf/side and four entries per page; once a page runs out of room, it should go to the next one
  - take each entry within `_recollections` and separate it into the heading and description + format them (bold the heading and put them on separate lines)
  - if numRecollections % 4 == 1 or 2, put it on the left page, otherwise, it should go on the right
  - if there are more than four entries in `_recollections` (two on each side of the page), instantiate a new page prefab and continue adding with the same logic as before

### After Coding
1. I found the task breakdown very helpful, especially for putting the saved information in `_recollections` into the journal format itself. Without it I probably would have found the filling of the journal pages to be quite difficult, as there are a lot of different values to juggle and keep track of in different if-statements. If I were to do this again, I would probably actually do it before coding instead of in the middle of it. My bad. But seriously, it was helpful to have next steps that I could refer back to, and if I were to make another in the future I would ensure that I wrote everything with notes about whether they have "prerequisite" systems that need to be built/implemented in order to work on them, so I know what to prioritize. 

2. In order for my VS graph (the `UIController`) to flip through the pages of the journal, it has to have access to new pages that are intantiated within the C# script `Player`. This requires accessing the `journalPages` variable from `UIController` to add the newly instantiated page to the AotList storing all of them. The `UIController` is then called using Unity Events on the back/next buttons. I realize this is a variable, rather than a method, but to use a C# method in a VS graph, reference an object within the graph with a public method. you create a public method within a script, and have a reference to an object with that script within the VS graph. Make sure to refresh the nodes in the project settings and now you can call the public method(s) from the game object with the script attached.
<img width="1058" height="728" alt="vs-graph-nextPage" src="https://github.com/user-attachments/assets/4011239f-6303-436c-a2cc-f36f96e3e65c" />

3. I used the ScriptableObject system for both my dialogue, as well as to store information about the recollections, with each recollection storing possible entries for the journal (which is displayed is determined by player input) as well as the sanity value associated with it (which raises or lowers the sanity depending on sorting). 



## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Cite any external assets used here!
