# GDIM33 Vertical Slice
## Milestone 1 Devlog

1. One important Visual Scripting Graph is my riddleUIGraph attached to RiddleManager, which triggers the monster ambush mechanic when the player chooses to go on the wrong path. It is an important mechanic which punish the player when they pick the wrong path and encourages more careful observation of the monster. riddleUIGraph works closely with the wrongAnswerTrigger graph, which handles the wrong answer trigger. When the player picks the wrong path and collides with the trigger, a Custom Event named WrongAnswer will be triggered. It will first read the Boolean ambushTriggered, and use the branch If to check whether the event has already been triggered. If the value is false, it means that it is the first time the wrong path collider is triggered. Then, the graph will use Set Variable to set the Boolean to True. This prevents the repetitive spawning of MonsterAmbush when the player gets in and out of the trigger zone. Then, the graph uses Set Active(false) to deactivate the Monster, which is patrolling. After that, a series of node including Get Transform, Get Position, and Set Position, will move the hidden MonsterAmbush to the position of the monsterAmbushPoint1. Lastly, Set Active(true) is used to activate MonsterAmbush and NavMeshAgent AI allows it to chase after the player. 

2. My breakdown is updated with the state machine systems controlling the monster’s AI. In the current milestone 1 build, I implemented three distinct states: Patrol, Chase, and Attack. With the use of a state machine, each state of the monster carries out a different function and has unique behavior. The Patrol state is used to control monster movement between two patrol points at a low speed of 3 units. It uses the On Enter State Event to set the speed of the NavMeshAgent and move the agent between patrol points using Set Destination. At the same time, the transition from Patrol to Chase checks if the player has enter monster’s detection range. If the player enters the detection range of 20 units, the monster will switch from Patrol to Chase state.

In Chase, the monster will chase after the player using chaseSpeed of 5.5 units. The Chase graph uses NavMeshAgent Set Speed to change the monster’s speed from patrolSpeed to chaseSpeed. In On Update, the monster constantly track player’s location using the player’s Get Transform/Get Position. The monster chases the player by receiving the player's position and setting the NavMeshAgent’s destination. 

When the player enters the attack range of the monster, the state machine sets the game over panel and sets the cursor visible so that the player can use the cursor to hit restart button to retry. At the same time, the attack state will also set the time scale to 0 to stop the game. 

The monster’s state machine is closely related to the UI system, such as GameOverPanel. The attack state will directly activate the game-over screen and allow the player to interact with UI buttons using the cursor. The monster’s state machine also connects to the hiding mechanic. This mechanic allows the player to hide from the monster by entering a hiding spot. As a non-core mechanic feature, it is still under development. However, once complete, the player will set a boolean variable called IsHiding to true by a C# script and reset the monster’s NavMeshAgent path and trigger a transition from chase to patrol.


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
