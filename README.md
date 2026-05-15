# GDIM33 Vertical Slice
## Milestone 1 Devlog

### Question 1

One important Visual Scripting Graph is my riddleUIGraph attached to RiddleManager, which triggers the monster ambush mechanic when the player chooses to go on the wrong path. It is an important mechanic which punish the player when they pick the wrong path and encourages more careful observation of the monster. riddleUIGraph works closely with the wrongAnswerTrigger graph, which handles the wrong answer trigger. When the player picks the wrong path and collides with the trigger, a Custom Event named WrongAnswer will be triggered. It will first read the Boolean ambushTriggered, and use the branch If to check whether the event has already been triggered. If the value is false, it means that it is the first time the wrong path collider is triggered. Then, the graph will use Set Variable to set the Boolean to True. This prevents the repetitive spawning of MonsterAmbush when the player gets in and out of the trigger zone. Then, the graph uses Set Active(false) to deactivate the Monster, which is patrolling. After that, a series of node including Get Transform, Get Position, and Set Position, will move the hidden MonsterAmbush to the position of the monsterAmbushPoint1. Lastly, Set Active(true) is used to activate MonsterAmbush and NavMeshAgent AI allows it to chase after the player. 

### Question 2

[Updated Breakdown with State Machine](https://www.figma.com/design/npm787IlXqCvnhdhI53a0R/Xichan-Updated-Breakdown?node-id=0-1&p=f&t=pxJ83yr4TG1FQ7Bh-0)

My breakdown is updated with the state machine systems controlling the monster’s AI, more accurate description of patrol state(from wander to random location to move between set patrol points), specific variables like loseRange/detectionRange/attackRange, and state machine's connection to the execution animation, game over screen. In the current milestone 1 build, I implemented three distinct states: Patrol, Chase, and Attack. With the use of a state machine, each state of the monster carries out a different function and has unique behavior. The Patrol state is used to control monster movement between two patrol points at a low speed of 3 units. It uses the On Enter State Event to set the speed of the NavMeshAgent and move the agent between patrol points using Set Destination. At the same time, the transition from Patrol to Chase checks if the player has enter monster’s detection range. If the player enters the detection range of 20 units, the monster will switch from Patrol to Chase state.

In Chase, the monster will chase after the player using chaseSpeed of 5.5 units. The Chase graph uses NavMeshAgent Set Speed to change the monster’s speed from patrolSpeed to chaseSpeed. In On Update, the monster constantly track player’s location using the player’s Get Transform/Get Position. The monster chases the player by receiving the player's position and setting the NavMeshAgent’s destination. 

When the player enters the attack range of the monster, the state machine sets the game over panel and sets the cursor visible so that the player can use the cursor to hit restart button to retry. At the same time, the attack state will also set the time scale to 0 to stop the game. 

The monster’s state machine is closely related to the UI system, such as GameOverPanel. The attack state will directly activate the game-over screen and allow the player to interact with UI buttons using the cursor. The monster’s state machine also connects to the hiding mechanic. This mechanic allows the player to hide from the monster by entering a hiding spot. As a non-core mechanic feature, it is still under development. However, once complete, the player will set a boolean variable called IsHiding to true by a C# script and reset the monster’s NavMeshAgent path and trigger a transition from chase to patrol.

## Milestone 2 Devlog

### Question 1

This system will build on the existing Wrong-path-trigger-ambush-monster mechanic. Once the player chooses the wrong path and enters the wrongPathTrigger collider, this system will initiate: 1. monster ambush, 2. flickering light for 2 times, 3. change of vignette color from black to red, 4. play a jumpscare sound effect.

Basic steps:

#### Step 1
Preparation: Create lights, audio, and a post-processing vignette effect
1. Create and identify point light components used for each ceiling light that I want to flicker during the ambush
2. Add an audio source component in WrongPathTrigger
3. Import and drag the jump scare audio clip into the audio source component
4. Turn off the Loop and Play On Awake, set the Spatial Blend to 1,
5. Set up the vignette override in the global post-processing volume.

#### Step 2
Ensure that the visual scripting graph riddleUIGraph and wrongAnswerTrigger are still working
1. Use the existing WrongAnswerTrigger graph to detect when the player chooses the wrong path and collides with the wrong path trigger
2. Send the WrongAnswer custom event to the riddleUIGraph
3. Set ambushTriggered to true so that the same ambush does not repeat when the player collides with the wrong path trigger again

#### Step 3
Create a C# script that 1) controls light state change, 2) play audio clip, and 3) changes vignette color and connect it to a visual scripting graph
1. Create a new C# script and name it AmbushVisualEffects and create public variables for the lights, audio source, and global volume
2. Assign each variable in the inspector. Get vignette effect from volume
3. Create a public method PlayAmbushEffect(), which runs EffectRoutine and can be called from the visual scripting graph
4. Create an ambush sequence EffectRoutine() which plays the jumpscare audio, flickers the lights twice, changes the vignette color to red, and returns the vignette to black after a few seconds
5. Create a GameObject and name it AmbushVisualEffectManager
6. Regenerate nodes by clicking Edit, Project Settings, Visual Scripting
7. In riddleUIGraph, get AmbushVisualEffect by Get Component, then call PlayAmbushEffect(). Then, set ambushTriggered to True, set patrol monster inactive, activate ambush monster


### Question 2

The task breakdown effectively helped me to build an animation system and the wrong-path trigger mechanics by offering clear and detailed steps towards implementing that feature. More importantly, by breaking down the feature into 2-3 bigger steps, I get less overwhelmed by all the things I need to do. The wrong path ambush involves multiple components, ranging from lighting, audio, post-processing, to visual scripting and C# script communication. By sequencing the steps, I also made fewer mistakes because I’m less likely to skip steps and jump to the next step when I’m still working on the previous function. For instance, I remembered to regenerate nodes before bridging the visual scripting graph with a script. If I were to do the breakdown again, I might specify the node more clearly so that I don’t accidentally add the wrong node. For example, I will write the exact name for each node, like Trigger Custom Event, Get Object Variables.

### Question 3

<img width="1363" height="377" alt="RiddleManager_ riddleUIGraph" src="https://github.com/user-attachments/assets/dab11a02-3a04-4512-9b97-87a0ca3ad412" />

In milestone 2, I bridged the visual scripting graph and code in the wrong-path-triggered-ambush system. The existing riddleUIGraph is currently responsible for triggering the ambush monster when the player selects the wrong path at the crossroad. After the player collides with the WrongAnswerTrigger in the wrong path, the graph sets ambushTriggered to true, disables the patrol monster, moves the ambush monster to the ambushPoint, and finally activates it. After this visual scripting sequence, the riddleUIGraph calls the C# script AmbushVisualEffect.cs. The graph uses Get Component to access AmbushVisualEffect component on the AmbushEffectManager. Then, the graph calls the public method PlayAmbushEffect() in the C# script. This method then initiates the ambush sequence, including the flickering of the lights, playing of the jumpscare audio, and the setting of vignette color to red for a few seconds. This bridging of the visual scripting graph allows the graph to control the main gameplay mechanics of ambush monster spawning, while C# handles the visual and audio feedback for selecting the wrong path.

### Question 4

I hope the grader can grade the monster’s NavMesh navigation for my Unity System choice. The monster uses a NavMesh agent to patrol through the maze and chase the player when the player is detected. This could be visually seen in the game and by checking the monster’s inspector and its NavMesh Agent, as well as its state machine. 

## Milestone 3 Devlog
Milestone 3 Devlog goes here.
## Milestone 4 Devlog
Milestone 4 Devlog goes here.
## Final Devlog
Final Devlog goes here.
## Open-source assets
- Cite any external assets used here!
