# GDIM33 Vertical Slice
## Milestone 1 Devlog
To be clear before I begin: my state machine IS my visual scripting graph (titled `UIController`), which is being used to toggle between the journal and the play screen.

In my [Project Breakdown](https://github.com/user-attachments/files/27191920/GDIM.33.Breakdown.-.Milestone.1.pdf), I explain how when the journal is inactive, the player should be able to click anywhere on the screen except for the "Toggle Journal" button in order to advance the dialogue line, and when the journal is active, the player should be able to use the "Back" and "Next" buttons to see all pages of the journal. In practice, this means that when the state is set to "Journal On", the journal UI is set to active, and the `Dialogue Box` is set to inactive. This transition/state change is triggered by the "Toggle Journal" button using the Unity Event system. Additionally, with the "Back" and "Next" buttons enabled, their `OnClick()` is defined using custom Unity events in the VS graphs that check for the index of the current page, disable that UI, update the current page, then set that page to active. 

Furthermore, in order to display the correct text in the journal, the `Player` class holds references to the UI that the state machine enables/disables, and updates the text in scripts based on options that the players select in the dialogue system.

The dialogue system, which is utilized when the journal is _inactive_ (again, called through the "Toggle Journal" button), is handled almost entirely by the `DialogueController` script. By checking the mouse position, it ensures the mouse isn't over the "Toggle Journal" button, while also checking the state to ensure the journal is inactive. Then, when a branching dialogue is selected, it checks if it holds a reference to a Recollection node, which would then contain potential data to be stored in the Journal's UI when it is next toggled using the state machine. 



## Milestone 2 Devlog
### General Notes
- My complicating feature is sanity, which while it is completely functional right now, there is not enough content to really showcase it. You'll notice that the first two "intake" questions raise the sanity by 5 (as a sort of "freebie"), while depending on your answer for "dream or "memory" for the last question, it will either add or subtract 5 points. You'll notice it can't go over 100. Unfortunately, because even if you get the sorting wrong, 5 + 5 - 5 is still 5, there is no way to drain the sanity down, which is what will trigger a lose condition once I have more content. However, the sanity is in a trackable state that can be monitered to trigger effects (like VFX, later on) and the lose-condition I mentioned once more content is available.
- While there are only three recollections in the build right now, you can repeat the game several times once all the dialogue has run through by just clicking again, and the journal will continue to fill up, showcasing the page function. Whatever page you were on when you close it is what page is open when you toggle it back on. Page numbers are a future endeavor. 

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
### General Notes
- As of right now, my shader is only present on the clock in the therapist office scene so it is shown off. THowever, that is not its intended use case. Once I have more environments built, I plan to use it as a stylistic representation of obscured memories/dreams, and it will be applied to objects in a scene to make it more disconcerting. I do not have enough environments built for that yet.
- Aside from the glitch shader, I also have a global volume and box volume in effect in order to add a vignette and a clinical coolness to the scene.

### Devlog Question Answers
<img width="1429" height="711" alt="milestone3-shader-graph" src="https://github.com/user-attachments/assets/ab5b838e-b1f2-4ca7-b80a-e525f69e2330" />

1. At a macro level, my shader graph takes random vertex positions from a mesh and displaces them by a value between -0.5 and 0.5 on the x-axis in spurts (as dictated by a Sine Time Node with some additional modifications to make it less formulaic). It also changes the base color to magenta. More specifically, it first grabs the current position data of the mesh and isolates the x-value so that it can be modified. The y-value and z-value are left unchanged. Before we can generate a random value, however, we must first make it so that it is referring to only one "slice" of the mesh, rather than the mesh as a whole (which would result in the mesh doing the equivalent of a transform to the left or right). To do this, I used a Simple Noise Texture and plugged the original x-value into the UV while forcing the y-value to remain 0 (which makes the simple noise texture look like horizontal stripes). Then, we add the -0.5 to 0.5 value generated from the Random Range Node to our specific x-value, which displaces only a slice. But this only results in one change. To make it repeat, the random range is being triggered by a Time Node, which is further modified by a Sine Time Node in order to have the glitch effect occur is bursts, rather than constantly.
   
2. To make my dialogue UI more legible, I made the background darker and text larger (as suggested). Additionally, I added a box denoting who's speaking. I've also added a ceiling to the therapist's office-- since it was mentioned in every playtest. The journal now has page numbers.

3. Since the last milestone, I've added an entirely new scene (dialogue wise) as well as two new environments. The new scene includes another recollection, which contributes to both the sanity feature and the journal. Additionally, I worked more on the aesthetics by using Volumes, both a global one that is used primarily for the therapist's office in order to make it seem more clinical, as well as a box volume that is used to differentiate the bedroom recollection. I've also added a skybox :D


## Final Submission Devlog
1. The gameplay loop of Head Count is largely this: the player is asked a question or presented with a situation. From there, they must determine the reality of the situation in order to avoid losing sanity by answering incorrectly. If too much sanity is lost, the player loses, and is only given however much of the story they had uncovered so far, however inaccurate it may be. The goal, then, is to make it as far as possible so you can uncover the full, accurate story that the character is telling. And somewhat suprisingly, this loop matches almost exactly what I pitched back at the beginning of the quarter: "a psychological horror narrative game centered around an inability to tell dreams from memory, and memory from dreams. The story takes place in a therapist’s office, where the player recounts their past and slowly determines what actually happened in their childhood." While not long enough to effectively communicate the whole story, what my vertical slice includes are shortened versions of the three main "sections" of the game: the intro, the main loop, and the breaking of the main loop. The intro functions largely as a tutorial for the basic mechanics, including the journal to keep track of previous choices and the sanity meter implemented via simple branching dialogue. From there, it moves fairly seamlessly into the main gameplay loop, where the player is physically transported into different scenes as they recollect and are prompted to investigate how real it is. Then, they return to the therapist office and make a decision about whether it is real or not. In the full game, there would be much more of this than two instances, and there would probably be more methods of investigation. However, the vertical slice communicates what it needs to, and moves into messing with player expectations by breaking the formula. Rather than being transported from a recollection back to the therapist office, they transport from a recollection into a different therapist office, which is actually another recolection. This works to demonstrate a method of unsteadying the player that would be implemented slowly and increasingly over time. 

2. While there are two rendering effects present in my project (the glitching objects and a sanity vignette), only the sanity vignette is triggered directly with C# logic. Specifically, in the `Player` class in the `Update()` function, it checks if the `sanityMeter` is below 68 (just an if-statement). If it *is* below 68, I call `SetFloat()` on the `Intensity` property in the `sanityEffect` shader graph to set the intensity of the effect to 1 using a reference to the URP pass material. If it's over 68, I do the same thing, but set the `Intensity` property to 0. 

3. **[(A)]** For me, when I approach a large project I first break it down into the large systems that I'll need to implement, while also taking note of how those systems might interact with each other (for example, one might need to be created first in order for the other to work). If I'm having trouble figuring out what the systems *are* in my project, then I'll go over my core game mechanics and core loop, which often write it out quite plainly. Then, in much the same way we did task break-downs in class, I'd go through each system and write down how to implement it in steps, noting down in which class/script some of the bigger things would be located to keep things organized. And, although, it is worth noting that I can do most of these things in my head. I do think these are important skills to learn so I can communicate ideas with teammates and/or keep track of where I am if I take a break from a project. **[(B)]** Beyond that, breaking down a large project into systems/steps definitely helps with scope. Sometimes, when you just say an idea outloud, you can't fully grasp what structures you're actually proposing, nor how difficult their implementation will be. Easier said than done, and all that. By breaking things down, you are forced to consider how long something will take, and whether or not a mechanic is even within your bredth of knowledge (if it isn't, do you have time to learn it?). More than that, it gives you actionable steps that you can put timelines on, and how well you stick to said timeline can definitely tell you a lot about how achiveable a project is. It's all an iterative process, and as you learn more about your own project, your might realize you have more/less time than you realized, and adjusting based on that is key. **[(C)]** For me, actually, I realized I could do more than I thought I would have time to do. Largely this was due to prioritizing well-functioning and expandable systems from the get-go, keeping in mind larger project goals with my implementation before putting too much effort into any of the more story-based stuff, which could always be written later. Because of that, I was able to sort-of implement the excluded feature I mentioned in my pitch, wherein the line between what's real and fake gets more and more blurred as the game goes on, really leaning into the phycological horror that inspired the project in the first place. Because I had such a clear idea of what my game mechanics were, and how I was going to go about making them, I was able to really build a strong foundation that I could mold to fit situations that hadn't explicitly been planned. Additionally, I made sure my code was really well documented so that I always knew where different things were implemented, even if I didn't know exactly how it worked off a glance so I knew where I could go to modify it if necessary (which was done a couple times). So I will definitely be using that strategy in the future.

## Open-source assets
### Models
- [Therapist Model and Animation](https://www.mixamo.com/#/)
- [Bedroom Assets](https://assetstore.unity.com/packages/3d/props/interior/low-poly-interior-pack-stylized-furniture-set-369340)
- [Office Assets](https://assetstore.unity.com/packages/3d/props/low-poly-office-set-1-140-models-vnb-327126)
- [Star Skybox](https://assetstore.unity.com/packages/3d/environments/sci-fi/real-stars-skybox-lite-116333)

### UI Elements
- [Fonts](https://www.dafont.com/)
- [Journal Assets](https://srtoasty.itch.io/ui-assets-pack-2)
- [Dialogue Borders](https://gx310.itch.io/pxiel-art-ui-borders)

### Audio
- [Page Turning Sound 1](https://www.freesound.org/people/partheeban/sounds/457767/)
- [Page Turning Sound 2](https://www.freesound.org/people/Koops/sounds/20263/)
- [Page Turning Sound 3](https://www.freesound.org/people/Flem0527/sounds/630019/)
- [Electric Buzzing Hum](https://www.freesound.org/people/soundofsong/sounds/641336/)
- [Rain and Distant Traffic](https://www.freesound.org/people/olsonbock623/sounds/442296/)
