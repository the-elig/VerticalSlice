# GDIM33 Vertical Slice
## Milestone 1 Devlog
To be clear before I begin: my state machine IS my visual scripting graph (titled `UIController`), which is being used to toggle between the journal and the play screen.

In my [Project Breakdown](https://github.com/user-attachments/files/27191920/GDIM.33.Breakdown.-.Milestone.1.pdf), I explain how when the journal is inactive, the player should be able to click anywhere on the screen except for the "Toggle Journal" button in order to advance the dialogue line, and when the journal is active, the player should be able to use the "Back" and "Next" buttons to see all pages of the journal. In practice, this means that when the state is set to "Journal On", the journal UI is set to active, and the `Dialogue Box` is set to inactive. This transition/state change is triggered by the "Toggle Journal" button using the Unity Event system. Additionally, with the "Back" and "Next" buttons enabled, their `OnClick()` is defined using custom Unity events in the VS graphs that check for the index of the current page, disable that UI, update the current page, then set that page to active. 

Furthermore, in order to display the correct text in the journal, the `Player` class holds references to the UI that the state machine enables/disables, and updates the text in scripts based on options that the players select in the dialogue system.

The dialogue system, which is utilized when the journal is _inactive_ (again, called through the "Toggle Journal" button), is handled almost entirely by the `DialogueController` script. By checking the mouse position, it ensures the mouse isn't over the "Toggle Journal" button, while also checking the state to ensure the journal is inactive. Then, when a branching dialogue is selected, it checks if it holds a reference to a Recollection node, which would then contain potential data to be stored in the Journal's UI when it is next toggled using the state machine. 



## Milestone 2 Devlog
Milestone 2 Devlog goes here.
## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Cite any external assets used here!
