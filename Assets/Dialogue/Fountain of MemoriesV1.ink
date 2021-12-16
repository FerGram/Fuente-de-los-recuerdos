//-> ToraChat1
VAR hasPieces = false

EXTERNAL startMinigame(minigame)
EXTERNAL startCinematic()
EXTERNAL startEnding()

//ITEMS
VAR wheel = "Fernadito"
VAR chessPiece = "Chess Piece"

//Names
VAR nudo = "Nudo"
VAR tora = "Tora"
VAR anton = "Anton"
VAR barman = "Barman"
VAR fionna = "Fionna"

//OBJECTIVES
VAR find = "find"

//CHARACTER LOCATIONS
VAR toraLoc = "_4_Plaza"
VAR barmanLoc = "_8_Bar"
VAR nudoLoc = "_999_Limbo"
VAR antonLoc = "_8_Bar"
VAR fionnaLoc = "_9_FionnaHouse"

//ITEM LOCS
VAR chessPiecesLoc = "_999_Limbo"

=== function item(name) ===
~return "<color=red>" + name + "</color>"

=== function character(name) ===
~return "<color=black>" + name + "</color>"

=== function InitializeItems ===
{
    - true:
            ~wheel = item(wheel)
            ~chessPiece = item(chessPiece)
            ~nudo = character(nudo)
            ~tora = character(tora)
            ~anton = character(anton)
            ~barman = character(barman)
            ~fionna = character(fionna)
}

//Fer Edits
=== InitialCrash ===
{not InitialCrashDefault : -> InitialCrashDefault }

=== InitialCrashDefault
Patrick<"OH MY GOD."
Patrick<"Ok, that was terrifying."
Patrick<"I'm still in one piece though which is a start."
Patrick<"And although the horrendous actual state of the car I think I might be able to fix it just enough to take me back home."
Patrick<"Wait, where am I?"
Patrick<"Anyway, I better <color=yellow>find </color>and <color=yellow>place <color=red>the missing wheel </color></color>and fix the flat tire."
->DONE

=== CarWheel ===
Patrick<"Ok. Wheel's ready."
Patrick<"I hope it stays in place."
Patrick<"I'll check around if I can <color=red>find a way to fix the flat tire </color> and get back home."
->DONE

=== CarDefault ===
Patrick<"Ugh! Look at its awful state."
Patrick<"What a disaster."
->DONE

=== CarFull ===
Patrick<"Ah... Finally."
Patrick<"Just a few adjustments and I'll be able to get this thing back home."
Patrick<"Although I'm gonna need a miracle..."
Patrick<"I'm glad I could help those memories get their fountain back."
Patrick<"I hope they find what they are looking for."
~ startEnding()
->DONE

=== SignDefault ===
Patrick<"KPNTVLEY..."
Patrick<"It's barely readable."
Patrick<"But it's a sign so it must indicate some place."
Patrick<"I'll have a look."
->DONE

=== SignHikers ===
Martha<"KPNTVLEY..."
Martha<"It's barely readable."
Martha<"But it's a sign so it must indicate some place."
Martha<"I'll have a look."
->DONE

=== HikersInitial ===
Lewis<"Martha... I think we might have missed the right hiking track."
Martha<"Ahh, don't you worry about it. We are explorers! aren't we?"
Lewis<"Ermm..."
Martha<"Come on, let's go into the wilds for a bit."
Martha<"..."
Martha<"Hmm. I can see some footsteps on the floor. Someone must have been here not so long ago."
Martha<"Footsteps go in both forwards and backwards so he or she was able to make it back."
Lewis<"I guess you're right."
->DONE

=== HikersFarVillage ===
Martha<"Lewis! Look! Look!"
Martha<"See that village in the distance?"
Lewis<"I think I do, Martha."
Lewis<"It's quite impossible not to see it."
Martha<"Hurry! Let's take a closer look."

->DONE

=== HikersInFountain ===
Martha<"WOOOOW look at this place."
Lewis<"It seems abandoned."
Martha<"Right, but the fountain is still working."
Lewis<"And it's still in pretty good shape to be honest."
Lewis<"I wonder why everybody left..."
Lewis<"It's a pretty decent village to live in."
Martha<"Imagine we could restore the place and bring the village back to life, Lewis."
Lewis<"Ermm..."
Martha<"I could talk to a few people and make them join the project!."
Lewis<"Wait, Martha... The project?"
Martha<"AND WE CAN RUN THE LOCAL BAKERY!."
Lewis<"Martha, slow down."
Martha<"AND I'LL BE THE MAYORESS!!."
Lewis<"Hey how come you'll be the mayoress and not me?"
Martha<"Because I said it first."
Lewis<"That's not how it works!"
Martha<"I guess I can share a little bit of my power with you..."
Martha<"WE'LL BOTH BE MAYORS!!"
Martha<"Let's get back to the car. I have to make tons of calls!"
Lewis<"Let's talk about it first!"
->DONE

//END Fer Edits

=== Tora ===
TODO Save system
//Story logic Tora
{not ToraChat1:
        ~InitializeItems() //Initialize all items, should move to the very first scene.
        -> ToraChat1
- else:
        ->ToraDefault1
 }

=== ToraChat1
Tora<“Welcome to Kiponut Valley, traveler.”
Patrick<“A... ARE YOU...? WHAT ARE YOU?!”
Tora<“Worry not, traveler. We are harmless.”
Patrick<“W... WE?!”
Tora<“Ah, yes. Excuse me.”
//~startMinigame(14)
Tora<“As I was saying. Welcome to Kiponut Valley.”
Patrick<“Okay...”
Tora<“This town was abandoned long ago, when the fountain broke.”
Tora<“What remains here are just ghosts of our past selves.”
Tora<“Forgotten souls that wander around waiting for the day...”
Tora<“For the day the town comes back to live.”

    + “What do you mean?”

    Tora<“In its time, this town was a lively place. Full of joyful people which fed the town's natural atractive.”
    Tora<“The main structure of the village was the fountain I'm next to right now.”
    Tora<“It was usual to see people gaze at its beautiful shape at any time of the day.”
    Tora<“But people started leaving town and the fountain suddenly wasn't spilling water like before.”
    Tora<“And people kept leaving town till no more water came out of its beautiful spouts.”
    Tora<“It's like the fountain needed the people in town to keep going.”

    + “Do you know where I can find a car tire?”

    Tora<“I'm afraid I don't have what you are looking for, traveler. But I know someone who might be able to help you.”

	+ “Why did the fountain break?”

    Tora<“First, it was Isaiah: the town's baker.”
    Tora<“He had to move to the city because he wanted to expand his business.”
    Tora<“The day after his departure, the fountain did not spill water like before.”
    Tora<“Then it was Friederick: the mailman”
    Tora<“He was transfered to the city because the company was short on staff.”
    Tora<“The day after, even less water was spilled from the fountain.”
    Tora<“And people kept leaving town till no more water came out of its beautiful spouts.”
    Tora<“It's like the fountain needed the people in town to keep going.”

-
Tora<“I wish I could offer you a warm welcome, but there’s little a memory can do.”

Tora<“May I ask you for a favour while you stay in town?”

Patrick<“I… Yes what is it?”

Tora<“People left in town haven't seen a new face in some time.” 

Tora<“If you talk to any of them, listen to their stories. Help them remember their good old times in town, retrieve their memories.”

Patrick<“I guess I can do that”

Tora<“Thank you, Patrick.” 

Patrick<“Wait, how do you...?”

Tora<“You should find {nudo} in the bar, the building to the right of the fountain. He might help you with what you are looking for.”
->DONE

=== ToraDefault1
Tora<“You should find {nudo} in the bar, the building over there.”
->DONE


=== Barman ===
//Story logic for Barman
{
- not ToraChat1:
		->BarmanDefault1

- not BarChat1:
        ->BarChat1

- BarChat1 and not AntonHouseChat1:
        ->BarmanDefault2

- AntonHouseChat1 and not BarChat2:
        ->BarChat2
        
- BarChat2:
        ->BarmanDefault3
 }


=== BarChat1
Barman<“A real human."
Barman<"You’re not from here haha, welcome to Sander’s. I would serve you something, but- I can’t really, can I?”

	+ “It’s fine.”

	+ “I would really do with a warm cup of milk.”

-
Barman<“So, what brings you to my humble little bar?”
~nudoLoc = "_12_Farm"
~antonLoc = "_17_AntonHouse"

	+ “I’m looking for the farmer, Nudo.”
	Barman<“Nudo? He was here just a few minutes ago along with Anton, complaining about how much his back was hurting him, but he left again to finish some work at the farm. The farm is south-east of here.”
	Barman<“The farm is south-east of here.”
	    
	+ “Do you know where I can get a tire?”
	Barman<“Nudo may help you with that. He was here just a few minutes ago along with Anton, complaining about how much his back was hurting him, but he left again to finish some work at the farm.”
	Barman<“The farm is south-east of here.”
        
-

->DONE

=== BarmanDefault1
Barman<"I miss the old days..."
->DONE

=== BarmanDefault2
Barman<"You'll find Nudo in the farm."

Barman<"The farm is south-east of here."
->DONE

=== Nudo ===
//Story logic for Nudo
{
- not ToraChat1:
		Nudo<"What was I going to do?"
		//TODO can insert dialogue in the bar between the three men.

- not BarChat1:
        //nudo shouldn't be visible in the farm unless the player talks with Barman.

- BarChat1 and not NudoFarmChat1:
        ->NudoFarmChat1

- NudoFarmChat1:
        ->NudoFarmDefault1
 }
 
=== NudoFarmChat1
Nudo<“Hey there, fella, you’re not from here, are ya?”

Patrick<“That’s… what everyone keeps saying.”

Nudo<“Don’t sweat it, we didn’t use to care much about outsiders, thousands of people came every year to see and taste the fountain… But now, it’s all dust and weed, and forgotten memories…”

Nudo<“You see that field over there? I used to work it all by myself. But nowadays, I can’t even keep the undergrowth away.”

Nudo<“Would you be so kind as to help me with it, please? You, young people, are always so full of energy, it shouldn’t take you too long.” 

	+ “I just came to ask you for a tire.”

	+ “Sure thing, but do you have an extra tire to lend me?”

-
Nudo<“A tire? Yea, I do have a few spare ones. Finish the field and I’ll give you one.”
~startMinigame(13)
Nudo<“Well well, you did a great job. You remind me of my son, he was just as skinny as you, and he still could work from dawn ‘til dusk.”

Nudo<“What nice memories…”

	+ “Could you now give me the tire?”
	Nudo<“What’s the hurry, fella.”

	+ “I’m glad I could be of help.”
	Nudo<“You reminded me of many things I thought I had lost, thank you.”

-
Nudo<“Here is your tire.”
//Reward the player with a tractor tire and change the scene to the “lively” version.
Patrick<“But this is… a tractor tire.”
Nudo<“Huh? Oh, you need a different tire? a bike tire?”

    + “N-no, for a car, a car tire.”
    Nudo<“For that, you’ll have to visit Anton, the chess player. He lives just to the left of the fountain, I think he had a car.”

	+ “Yes, a bike tire.”
    Nudo<“There you go, a bike tire.”
    //Reward the player with a bike tire.
	Patrick<“Actually… I need a car tire.”
    Nudo<“For that, you’ll have to visit Anton, the chess player. He lives just to the left of the fountain, I think he had a car.”
    
-
Nudo<“...”
Nudo<“Thanks again for this traveler.”
Nudo<“<size=25><i> Oh, only if you knew how much I miss you, son... </i></size>”
~startCinematic()
->DONE

===NudoFarmDefault1
Nudo<"Anton lives just to the left of the fountain."
->DONE

=== Anton ===
{
- not BarChat1:
		Anton<"Serve me the usual, bald man."
		->DONE
		TODO can insert dialogue in the bar between the three men.

- BarChat1 and not NudoFarmChat1:
        ->AntonDefault1

- NudoFarmChat1 and not AntonHouseChat1:
        ->AntonHouseChat1

- AntonHouseChat1 and not hasPieces:
        ->AntonDefault2

- hasPieces and not AntonHouseChat2:
        ->AntonHouseChat2
 }


===AntonHouseChat1
Anton<“Strange.”

Anton<“I spent all my life fighting for these trophies and teaching other people how to play.”

Anton<“But once the time to leave came, I couldn’t bring myself to take them, and at the same time, I couldn’t let myself to leave them.”

Anton<“It was as if they belonged here, in this small town, and nowhere else. They stayed in my place.”

//He turns around to face the player.
Anton<“Would you play one last game with me?”

    + “Sure thing.”
    Anton<“Thanks.”

	+ “I- don’t know how to play.”
    Anton<“Don’t worry, the rules are simple.”

-
Anton<“All you have to do is capture my King with your Knight. My Rooks and Bishops will move in the same pattern every time you move your Knight.”

Anton<“The townspeople are sick of playing against me, hehe, so I’m glad you’re up to the challenge.”

~chessPiecesLoc = "_8_Bar"

Anton<“Oh- I think I forgot my pieces at the bar… Would you be so kind as to bring them back, please? With this age, my knees hurt for any small movement, and walking all the way there again…”
->DONE

===AntonDefault1
Anton<"Looking for Nudo huh?"
Anton<"He should be in the farm, all the way to the right of here."
->DONE

===AntonDefault2
Anton<"The pieces should be somewhere in the bar."
->DONE

===BarChat2
Barman<"Hey, welcome again.”

Barman<“Did you find Nudo?”

Patrick<“Yes, he asked me to help him with the farm.”

Barman<“Haha, that’s Nudo for you, always making other people help out.”

Patrick<“But he wasn’t able to help me out.”

Barman<“So, he doesn’t have a tire, then? How strange…”

Patrick<“No, he did give me one, but it was a tractor tire.”

Barman<“Hahaha, a tractor tire.”

Patrick<“Do you know anyone else who may have an extra car tire?”

Barman<“Not really. Tora is the one who knows everything around here.”

Patrick<“She told me that Nudo may have one, but I guess she was wrong.”

Barman<“…”

Barman<“If Tora doesn’t know anyone-”

Barman<“Never mind…”

Barman<“Have you tried asking Anton?”

Patrick<“The chess player? Not yet. Actually, I was looking for his chess set, he seemed very eager to play a game with me.”

Barman<“Yeah, poor Anton is like that, always looking for a new partner to play with. He hasn’t been able to play new people since the chess club here in town closed.”

//Can send the player to pick up the pieces at Rosanna’s house.
Barman<“His pieces should be on that table over there, just pick them up.”
->DONE

===BarmanDefault3
Barman<“The pieces should be on that table over there, just pick them up.”
->DONE

===AntonHouseChat2
Anton<“You found them, thank Goodness. Let’s play, then.”

//Start the minigame.
~ startMinigame(12)
Anton<"Let's play."
//After winning the minigame, the room transforms, lightening up, getting tidy, and with a few children running about.
Anton<“Ah, the memories… How I missed this, playing against a new friend, thank you.”

Patrick<“I’m glad I could help.”

Patrick<“Em… Anton, would you, by any chance, have an extra car tire I can borrow?”

Anton<“A car tire… No, I don’t think I have one. You see, I left town in my car, like everyone else. I’m not sure if any car was left behind. 
Anton<“Tora was the last one to leave, she should know if any car was left behind.”

Anton<“The poor thing blames herself for the death of the town.”


	+“Why is that?”

	Anton<“Her father was the only doctor in town, and when he died… the person responsible for the maintenance of the fountain and a few others left town.”
	Anton<“Tora found herself alone, accompanied only by the fountain. She rejected everyone’s help.”

	+“Maybe it’s really her fault.”
	“I doubt that. She’s a good kid, it was an accident. Things break, people die, and memories get forgotten, that’s the way of life.”

    Patrick<“What do you mean?”

    Anton<“Her father was the only doctor in town, and when he died… the person responsible for the maintenance of the fountain and a few others left town.”
    Anton<“Tora found herself alone, accompanied only by the fountain. She rejected everyone’s help.”

-
Patrick<“How sad… it must have been tough for her.”

Anton<“Yes, it was.”

Anton<“Maybe you should try asking Fionna about an extra tire, her partner’s car could have been left behind.”

Anton<“You can find her in the northwest of town, in the chicken coop.”

Patrick<“Okay, thanks.”

Anton<“No. Thanks to you, son.”

Anton<“Thanks for playing one last game of chess with me.”

Anton<“I almost forgot how good it feels...”

~startCinematic()
->DONE

=== Fionna ===
{
- not AntonHouseChat2:
    ->FionnaDefault1

- AntonHouseChat2 and not FionnaChat1:
    ->FionnaChat1

}
->DONE

===FionnaChat1
Fionna<“Hey, welcome to our little town.”
//Maybe the player can see the chicken from the beginning, throughout the whole map?
Patrick<“Hi, I was looking for Fionna, would you know where I can find her?”

Fionna<She smiles. “Yes, it’s me, Tora was looking for you.”

Patrick<“For me?”

Fionna<“Yes, you. I can tell you where to find her, but first, can you help me out with the chicken? I have been all day trying to catch them, but they’re as playful as their owner.”

    +“I guess I don’t have another option, do I?”
    Fionna<“Nop 😊.”
    Patrick<“You said they are as playful as their owner, who’s their owner?”
    
	+“Who’s their owner?”

-
Fionna<“It was… Claire, my late wife. She died of sickness when I was on a business trip. If I only stayed with her, I could have taken her to the doctor.”

Fionna<“…”

Patrick<“Was it before Tora’s father… died?”

Fionna<“You know about Tora’s father?”

Fionna<“It was after, that’s why Tora- well, never mind.”

	+ “What happened to Tora’s father?”
	Fionna<“I- I rather not say. You should ask Tora herself.”

	+ “Where is Tora?”
	Fionna<“If I told you now, you’d run away from me, wouldn’t you?”

-
Fionna<“Help me out with the chicken and we’ll talk about Tora’s whereabouts so you can ask her.”

Patrick<“Yea, but I came here to ask you a different questi-”

Fionna<“No buts, time to catch some chicken.”

~ startMinigame(14)
//After finishing the minigame and seeing the scene get alive…
Fionna<“Claire would thank you with an apple pie. I’m not that good with cooking, so I’ll answer your questions as promised.”

	+ “Where can I find Tora?”
	Fionna<“She’s in the viaduct, east of town. She often goes there to visit her father’s tomb.”

	+ “Do you have an extra car tire?”
	Fionna<“A car tire? No, I don’t have one, but Tora should know where you can find one. Didn’t she tell you yet?”

- 
TO BE CONTINUED.
~startCinematic()
->DONE

===FionnaDefault1
Fionna<"Oh, these damn chicken won't stay still."
->DONE

=== PickUpPieces ===
~ hasPieces = true
->Anton
->DONE

->END
