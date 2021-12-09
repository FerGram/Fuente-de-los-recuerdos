//-> ToraChat1
VAR hasPieces = false

EXTERNAL startMinigame(minigame)
EXTERNAL startCinematic()

=== InitialCrash ===
{not InitialCrashDefault : -> InitialCrashDefault }

=== InitialCrashDefault
Patrick."Damn I crashed my car"

Patrick."I better FIND AND PLACE THE MISSING WHEEL and fix the broken tire"
->DONE

=== CarWheel ===
Patrick."Ok. Wheel's in place"

Patrick."I'll check around if I can FIND A WAY TO FIX THE TIRE"
->DONE

=== CarDefault ===
Patrick."Ugh! Look at its awful state"

Patrick."What a disaster"
->DONE

=== Tora ===
TODO Save system
//Story logic Tora
{not ToraChat1:
        -> ToraChat1
- else:
        ->ToraDefault1
 }

=== ToraChat1
Tora.“Welcome to the Fountain of Memories, traveler.”

Tora.“This town has been abandoned since long ago, when the fountain broke.”
//~ startMinigame(12)
Tora.“Only our memories are left.”
Tora.“Only our memories are left.”
    + “What do you mean?”

    Tora.“No one in this town is a real person, we’re just ghosts of our past selves, longing to revive the town.”

    + “Do you know where I can find a tire?”

    Tora.“No matter what you seek, traveler, if you don’t want to become a memory like us, you’ll have to fix the fountain. But if a tire is what you’re looking for, Nudo, the farmer, may help you.”

	+ “Why did the fountain break?”

    Tora.“It’s- no one really knows… It’s been so long ago…”

-
Tora.“I wish I could offer you a warm welcome, but there’s little a memory can do.”

Tora.“Would you be so kind as to… fix the fountain? Or you’d rather just- abandon us, too?”

Patrick.“I…”

Patrick.“I don’t know…”

~ startCinematic()
->DONE

=== ToraDefault1
Tora.“You should find Nudo in the bar, the building over there.”
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
Barman.“A real human,” he says. “You’re not from here haha, welcome to Sander’s. I would serve you something, but- I can’t really, can I?”

	+ “It’s fine.”

	+ “I would really do with a warm cup of milk.”

-
Barman.“So, what brings you to my humble little bar?”

	+ “I’m looking for the farmer, Nudo.”
	Barman.“Nudo? He was here just a few minutes ago along with Anton, complaining about how much his back was hurting him, but he left again to finish some work at the farm. The farm is south-east of here.”
	    ->DONE
	+ “Do you know where I can get a tire?”
	Barman.“Nudo may help you with that. He was here just a few minutes ago along with Anton, complaining about how much his back was hurting him, but he left again to finish some work at the farm. The farm is south-east of here.”
        ->DONE

=== BarmanDefault1
Barman."I miss the old days..."
->DONE

=== BarmanDefault2
Barman."You'll find Nudo in the farm."

Barman."The farm is south-east of here."
->DONE

=== Nudo ===
//Story logic for Nudo
{
- not ToraChat1:
		Nudo."What was I going to do?"
		//TODO can insert dialogue in the bar between the three men.

- not BarChat1:
        TODO //nudo shouldn't be visible in the farm unless the player talks with Barman.

- BarChat1 and not NudoFarmChat1:
        ->NudoFarmChat1

- NudoFarmChat1:
        ->NudoFarmDefault1
 }
 
 === NudoFarmChat1
Nudo.“Hey there, fella, you’re not from here, are ya?”

Patrick.“That’s… what everyone keeps saying.”

Nudo.“Don’t sweat it, we didn’t use to care much about outsiders, thousands of people came every year to see and taste the fountain… But now, it’s all dust and weed, and forgotten memories…”

Nudo.“You see that field over there? I used to work it all by myself. But nowadays, I can’t even keep the undergrowth away.”

Nudo.“Would you be so kind as to help me with it, please? You, young people, are always so full of energy, it shouldn’t take you too long.” 

	+ “I just came to ask you for a tire.”

	+ “Sure thing, but do you have an extra tire to lend me?”

-
Nudo.“A tire? Yea, I do have a few spare ones. Finish the field and I’ll give you one.”
//The player then proceeds to play the farm minigame.
Nudo.“Well well, you did a great job. You remind me of my son, he was just as skinny as you, and he still could work from dawn ‘til dusk.”

Nudo.“What nice memories…”

	+ “Could you now give me the tire?”
	Nudo.“What’s the hurry, fella.”

	+ “I’m glad I could be of help.”
	Nudo.“You reminded me of many things I thought I had lost, thank you.”

-
Nudo.“Here is your tire.”
//Reward the player with a tractor tire and change the scene to the “lively” version.
Patrick.“But this is… a tractor tire.”
Nudo.“Huh? Oh, you need a different tire? a bike tire?”

    + “N-no, for a car, a car tire.”
    Nudo.“For that, you’ll have to visit Anton, the chess player. He lives just to the left of the fountain, I think he had a car.”
    ->DONE

	+ “Yes, a bike tire.”
    Nudo.“There you go, a bike tire.”
    //Reward the player with a bike tire.
	Patrick.“Actually… I need a car tire.”
    Nudo.“For that, you’ll have to visit Anton, the chess player. He lives just to the left of the fountain, I think he had a car.”
    ->DONE

===NudoFarmDefault1
Nudo."Anton lives just to the left of the fountain."
->DONE

=== Anton ===
{
- not ToraChat1:
		Anton."Serve me the usual, baldman."
		TODO can insert dialogue in the bar between the three men.

- not BarChat1:
        TODO //nudo shouldn't be visible in his house unless the player talks with Barman.

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
Anton.“Strange.”

Anton.“I spent all my life fighting for these trophies and teaching other people how to play.”

Anton.“But once the time to leave came, I couldn’t bring myself to take them, and at the same time, I couldn’t let myself to leave them.”

Anton.“It was as if they belonged here, in this small town, and nowhere else. They stayed in my place.”

//He turns around to face the player.
Anton.“Would you play one last game with me?”

    + “Sure thing.”
    Anton.“Thanks.”

	+ “I- don’t know how to play.”
    Anton.“Don’t worry, the rules are simple.”

-
Anton.“All you have to do is capture my King with your Knight. My Rooks and Bishops will move in the same pattern every time you move your Knight.”

Anton.“The townspeople are sick of playing against me, hehe, so I’m glad you’re up to the challenge.”

Anton.“Oh- I think I forgot my pieces at the bar… Would you be so kind as to bring them back, please? With this age, my knees hurt for any small movement, and walking all the way there again…”
->DONE

===AntonDefault1
Anton."..."
Anton."The good old days..."
->DONE

===AntonDefault2
Anton."The pieces should be somewhere in the bar."
->DONE

===BarChat2
Barman."Hey, welcome again.”

Barman.“Did you find Nudo?”

Patrick.“Yes, he asked me to help him with the farm.”

Barman.“Haha, that’s Nudo for you, always making other people help out.”

Patrick.“But he wasn’t able to help me out.”

Barman.“So, he doesn’t have a tire, then? How strange…”

Patrick.“No, he did give me one, but it was a tractor tire.”

Barman.“Hahaha, a tractor tire.”

Patrick.“Do you know anyone else who may have an extra car tire?”

Barman.“Not really. Tora is the one who knows everything around here.”

Patrick.“She told me that Nudo may have one, but I guess she was wrong.”

Barman.“…”

Barman.“If Tora doesn’t know anyone-”

Barman.“Never mind…”

Barman.“Have you tried asking Anton?”

Patrick.“The chess player? Not yet. Actually, I was looking for his chess set, he seemed very eager to play a game with me.”

Barman.“Yeah, poor Anton is like that, always looking for a new partner to play with. He hasn’t been able to play new people since the chess club here in town closed.”

//Can send the player to pick up the pieces at Rosanna’s house.
Barman.“His pieces should be on that table over there, just pick them up.”
->DONE

===BarmanDefault3
Barman.“The pieces should be on that table over there, just pick them up.”
->DONE

===AntonHouseChat2
Anton.“You found them, thank Goodness. Let’s play, then.”

//Start the minigame.
~ startMinigame(12)
Anton."Let's play."
//After winning the minigame, the room transforms, lightening up, getting tidy, and with a few children running about.
Anton.“Ah, the memories… How I missed this, playing against a new friend, thank you.”

Patrick.“I’m glad I could help.”

Patrick.“Em… Anton, would you, by any chance, have an extra car tire I can borrow?”

Anton.“A car tire… No, I don’t think I have one. You see, I left town in my car, like everyone else. I’m not sure if any car was left behind. Tora was the last one to leave, she should know if any car was left behind.”

Anton.“The poor thing blames herself for the death of the town.”


	+“Why is that?”

	Anton.“Her father was the only doctor in town, and when he died… the person responsible for the maintenance of the fountain and a few others left town. Tora found herself alone, accompanied only by the fountain. She rejected everyone’s help.”

	+“Maybe it’s really her fault.”
	“I doubt that. She’s a good kid, it was an accident. Things break, people die, and memories get forgotten, that’s the way of life.”

    Patrick.“What do you mean?”

    Anton.“Her father was the only doctor in town, and when he died… the person responsible for the maintenance of the fountain and a few others left town. Tora found herself alone, accompanied only by the fountain. She rejected everyone’s help.”

-
Patrick.“How sad… it must have been tough for her.”

Anton.“Yes, it was.”

Anton.“Maybe you should try asking Fionna about an extra tire, her partner’s car could have been left behind.”

Anton.“You can find her in the northwest of town, in the chicken coop.”

Patrick.“Okay, thanks.”
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
Fionna.“Hey, welcome to our little town.”
//Maybe the player can see the chicken from the beginning, throughout the whole map?
Patrick.“Hi, I was looking for Fionna, would you know where I can find her?”

Fionna.She smiles. “Yes, it’s me, Tora was looking for you.”

Patrick.“For me?”

Fionna.“Yes, you. I can tell you where to find her, but first, can you help me out with the chicken? I have been all day trying to catch them, but they’re as playful as their owner.”

    +“I guess I don’t have another option, do I?”
    Fionna.“Nop 😊.”
    Patrick.“You said they are as playful as their owner, who’s their owner?”
    
	+“Who’s their owner?”

-
Fionna.“It was… Claire, my late wife. She died of sickness when I was on a business trip. If I only stayed with her, I could have taken her to the doctor.”

Fionna.“…”

Patrick.“Was it before Tora’s father… died?”

Fionna.“You know about Tora’s father?”

Fionna.“It was after, that’s why Tora- well, never mind.”

	+ “What happened to Tora’s father?”
	Fionna.“I- I rather not say. You should ask Tora herself.”

	+ “Where is Tora?”
	Fionna.“If I told you now, you’d run away from me, wouldn’t you?”

-
Fionna.“Help me out with the chicken and we’ll talk about Tora’s whereabouts so you can ask her.”

Patrick.“Yea, but I came here to ask you a different questi-”

Fionna.“No buts, time to catch some chicken.”

//Start the minigame.
//After finishing the minigame and seeing the scene get alive…
Fionna.“Claire would thank you with an apple pie. I’m not that good with cooking, so I’ll answer your questions as promised.”

	+ “Where can I find Tora?”
	Fionna.“She’s in the viaduct, east of town. She often goes there to visit her father’s tomb.”

	+ “Do you have an extra car tire?”
	Fionna.“A car tire? No, I don’t have one, but Tora should know where you can find one. Didn’t she tell you yet?”

- 
TO BE CONTINUED.
->DONE

===FionnaDefault1
Fionna."Oh, these damn chicken won't stay still."
->DONE

=== PickUpPieces ===
~ hasPieces = true
->Anton
->DONE

->END
