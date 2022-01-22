//Fountain of Memories

VAR hasPieces = false

//EXTERNAL FUNCTIONS
EXTERNAL startMinigame(minigame)
EXTERNAL giveItem(item)

EXTERNAL startCinematic()
EXTERNAL startEnding()

//ITEMS
VAR drivingWheel = "Wheel"
VAR chessPiece = "Chess Pieces"
VAR carTire = "Tire"
VAR tractorTire = "Tractor Tire"
VAR bikeTire = "Bike Tire"

//NAMES
VAR nudo = "Nudo"
VAR tora = "Tora"
VAR anton = "Anton"
VAR barman = "Helen"
VAR fionna = "Fionna"

//OBJECTIVES
VAR find = "find"
VAR place = "place"
VAR drag = "drag"
VAR play = "play"
VAR bring = "bring"
VAR pick = "pick"
VAR car = "car"

//CHARACTER LOCATIONS
VAR toraLoc = "_4_Plaza"
VAR barmanLoc = "_8_Bar"
VAR nudoLoc = "_999_Limbo"
VAR antonLoc = "_8_Bar"
VAR fionnaLoc = "_9_FionnaHouse"

VAR colorsAreInit = false

//ITEM LOCS
VAR chessPiecesLoc = "_999_Limbo"

=== function item(name) ===
~return "<color=orange>" + name + "</color>"

=== function character(name) ===
~return "<color=orange>" + name + "</color>"

=== function action(name) ===
~return "<color=purple>" + name + "</color>"

=== function InitializeColorVariables ===
{
    - not colorsAreInit:
            ~colorsAreInit = true
            ~drivingWheel = item(drivingWheel)
            ~chessPiece = item(chessPiece)
            ~carTire = item(carTire)
            ~tractorTire = item(tractorTire)
            ~bikeTire = item(bikeTire)

            ~nudo = character(nudo)
            ~tora = character(tora)
            ~anton = character(anton)
            ~barman = character(barman)
            ~fionna = character(fionna)

            ~find = action(find)
            ~place = action(place)
            ~drag = action(drag)
            ~play = action(play)
            ~bring = action(bring)
            ~pick = action(pick)
            ~car = action(car)

}

//Fer Edits
=== InitialCrash ===
{not InitialCrashDefault : -> InitialCrashDefault }
->DONE

=== InitialCrashDefault
Patrick<"OH MY GOD."
~InitializeColorVariables()
Patrick<"Ok, that was- terrifying."
Patrick<"I'm still in one piece though, which is a start."
Patrick<"And despite the horrendous state of the car I think I might be able to fix it... At least just enough to carry me back home."
Patrick<"Wait, where am I?"
Patrick<"Anyway, I better {find} and {place} the missing {drivingWheel} and fix the flat {carTire}."
->DONE

=== CarWheel ===
Patrick<"Ok. Wheel's ready."
Patrick<"I hope it stays in place."
Patrick<"I'll check around if I can {find} a way to fix the flat {carTire}."
->DONE

=== CarDefault ===
Patrick<"Ugh! Look at its awful state."
Patrick<"What a disaster..."
->DONE

=== PickedUpCarWheel ===
Patrick<"Let's {place} it back in the car."
Patrick<"I'll {drag} it onto the car"
->DONE

=== CarTractorTire
//TODO add contemt
Patrick<"I don't think my car can handle such a wheel."
->DONE

=== CarBikeTire
Patrick<"I don't think such a wheel can support my car."
->DONE

=== CarFull ===
Patrick<"Ah... Finally."
Patrick<"Just a few adjustments and I'll be able to get this thing back home."
Patrick<"Although I'm gonna need a miracle..."
Patrick<"I'm glad I could help those memories get their fountain back."
Patrick<"I hope they can find what they are looking for."
~ startEnding()
->DONE

=== SignDefault ===
Patrick<"KPNTVLEY..."
Patrick<"It's barely readable."
Patrick<"But it's a sign, so it must indicate some place."
Patrick<"I'll have a look."
->DONE

=== ItemToSign
Patrick<"Why in the world would I throw an object at a sign?"
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
~InitializeColorVariables()
//Story logic Tora
{
- not ToraChat1:
        ~InitializeColorVariables()
        -> ToraChat1
- not NudoFarmChat1:
        ->ToraDefault1

- FionnaChat1:
        ->ToraChat2Car
- else:
        Tora<"I see you're making new friends and helping out the townspeople."
        Tora<"Thanks..."
        ->DONE
 }

=== ToraTractorTire
Tora<"A {tractorTire}, will that help you fix your car?"
->DONE

=== ToraBikeTire
Tora<"Ohh, a {bikeTire} , I used to have an amazing bike, but I lost it somewhere..."
->DONE

=== ToraChessPieces ===
Tora<"Me, playing chess? I don't even know the names of the pieces."
->DONE

=== ToraChat1
Tora<“Welcome to Kiponut Valley, traveler."
Patrick<“A... ARE YOU...? WHAT ARE YOU?!"
Tora<“Worry not, traveler. We are harmless."

Patrick<“W... WE?!"
Tora<“Ah, yes. Excuse me."
//~startMinigame(21)
// Tora<“As I was saying. Welcome to Kiponut Valley."
// Patrick<“Okay..."
// Tora<“This town was abandoned long ago, when the fountain broke."
// Tora<“What remains here are just ghosts of our past selves."
// Tora<“Forgotten souls that wander around waiting for the day..."
Tora<“For the day the town comes back to live."

    + “What do you mean?"

    Tora<“In its time, this town was a lively place. Full of joyful people which fed the town's natural atractive."
//     Tora<“The main structure of the village was the fountain I'm next to right now."
//     Tora<“It was usual to see people gaze at its beautiful shape at any time of the day."
//     Tora<“But people started leaving town and the fountain suddenly wasn't spilling water like before."
//     Tora<“And people kept leaving town till no more water came out of its beautiful spouts."
//     Tora<“It's like the fountain needed the people in town to keep going."

    + “Do you know where I can find a car tire?"

    Tora<“I'm afraid I don't have what you are looking for, traveler. But I know someone who might be able to help you."

    + “Why did the fountain break?"

    Tora<“First, it was Isaiah: the town's baker."
    Tora<“He had to move to the city because he wanted to expand his business."
    Tora<“The day after his departure, the fountain did not spill water like before."
    Tora<“Then it was Friederick: the mailman"
    Tora<“He was transfered to the city because the company was short on staff."
    Tora<“The day after, even less water was spilled from the fountain."
    Tora<“And people kept leaving town till no more water came out of its beautiful spouts."
    Tora<“It's like the fountain needed the people in town to keep going."

-
Tora<“I wish I could offer you a warm welcome, but there’s little a memory can do."

Tora<“May I ask you for a favour while you stay in town?"

Patrick<“I… Yes what is it?"

Tora<“The people left in town haven't seen a new face in some time." 

Tora<“If you talk to any of them, listen to their stories. Help them remember their good old times in town, retrieve their memories."

Patrick<“I guess I can do that."

Tora<“Thank you, Patrick."

Patrick<“Wait, how do you...?"

Tora<“You should find {nudo} in the bar, the building to the right of the fountain. He might help you with what you are looking for."
->DONE

=== ToraDefault1
Tora<“You should find {nudo} in the bar, the building over there."
->DONE


=== Barman ===
//Story logic for Helen
{
- not ToraChat1:
		->BarmanDefault1

- not BarChat1:
        ->BarChat1

- BarChat1 and not NudoFarmChat1:
        ->BarmanDefault2

- NudoFarmChat1 and not AntonHouseChat1 and not BarChat2:
        ->BarChat2

- AntonHouseChat1 and not BarChat3:
        ->BarChat3

- BarChat2:
        ->BarDefaultAfterNudoBeforeAnton

- AntonHouseChat2:
        Helen<"{tora} was looking for you. I think she went to {fionna}'s home."
        ->DONE

- BarChat3:
        ->BarmanDefault3
 }


=== BarChat1
Helen<“A real human."
Helen<"I see you’re not from here, welcome to Sander’s. I would serve you something, but- I can’t really, can I?"
Helen<"Would you give me a hand please?"

~startMinigame(21)

Helen<"Thanks for the help, it's been a long time since those bottles were filled."

	+ “It’s fine."

	+ “I would really do with a warm cup of milk though."

-

Helen<“So, what brings you to my humble little bar?"

~nudoLoc = "_12_Farm"

~antonLoc = "_17_AntonHouse"

	+ “I’m looking for the farmer, Nudo."
	Helen<“{nudo}? He was here just a few minutes ago, complaining about how much his back hurt, but he left again to finish some work at the farm."

	+ “Do you know where I can get a tire?"
	Helen<“{nudo} may help you with that. He was here just a few minutes ago, complaining about how much his back was hurting him, but he left again to finish some work at the farm." 
-
Helen<“The farm is east of here."
->DONE

=== BarmanDefault1
Helen<"I'm tired of cleaning all this dust. I should hire someone to do it."
//~startMinigame(21)
->DONE

=== BarmanDefault2
Helen<"You'll find {nudo} in the farm."
->DONE

Helen<"The farm is south-east of here."
->DONE

=== Nudo ===
//Story logic for Nudo
{
- not ToraChat1:
		Nudo<"My back hurts. pour me another one, {barman}."
		//TODO can insert dialogue in the bar between the three men.

- not BarChat1:
        //nudo shouldn't be visible in the farm unless the player talks with Helen.

- BarChat1 and not NudoFarmChat1:
        ->NudoFarmChat1

- NudoFarmChat1:
        ->NudoFarmDefault1
- else:
    Nudo<“My back still hurts a little."
    ->DONE
 }

===NudoTractorTire
{
    -not NudoTractorTire:
        Nudo<"You can keep that, you earned it."
    -else:
        Nudo<"What is it that you don't understand, it's yours now."
}

->DONE

=== NudoFarmChat1
Nudo<“Hey there, fella, you’re not from here, are ya?"

Patrick<“That’s… what the barlady said."

Nudo<“Don’t sweat it, we didn’t use to care much about outsiders, thousands of people came every year to see and taste the fountain… But now, it’s all dust and weed, and forgotten memories…"

Nudo<“You see that field over there? I used to work it all by myself. But nowadays, I can’t even keep the undergrowth away."

Nudo<“Would you be so kind as to help me with it, please? You, young people, are always so full of energy, it shouldn’t take you too long." 

	+ “I just came to ask you for a tire."

	+ "Of course, but do you have an extra tire to lend me?"

-
Nudo<“A {carTire}? Yea, I do have a few spare ones. Finish the field and I’ll give you one."
Nudo<"You just have to put the good vegetables on the left basket, and the weed on the right basket."
~startMinigame(13)
Nudo<“Well well, you did a great job. You remind me of my son, he was just as skinny as you, and he still could work from dawn ‘til dusk."

Nudo<“What nice memories…"

	+ “Could you now give me the tire?"
	Nudo<“What’s the hurry, fella."

	+ “I’m glad I could be of help."
	Nudo<“You reminded me of many things I thought I had lost, thank you."

-
Nudo<“Here is your {carTire}."

~carTire = item("Car Tire")

//Reward the player with a tractor tire and change the scene to the “lively" version.
Patrick<“But this is… a tractor tire."
~giveItem(0)
Nudo<“Huh? Oh, you need a different tire? a {bikeTire}?"

    + “N-no, for a car, a car tire."
    Nudo<“For that, you’ll have to visit {anton}, the chess player. He lives just to the left of the fountain, I think he had a car."

	+ “Yes, a bike tire."
	~giveItem(1)
    Nudo<“There you go, a {bikeTire}."

    //Reward the player with a bike tire.
	Patrick<“Actually… I need a car tire."
    Nudo<“For that, you’ll have to visit {anton}, the chess player. He lives just to the left of the fountain, I think he had a car."

-
Nudo<“..."
Nudo<“Thanks again for this, traveler."
Nudo<“<size=25><i> Oh, only if you knew how much I miss you, son... </i></size>"
~startCinematic()
->DONE

===NudoFarmDefault1
Nudo<"{anton} lives just to the left of the fountain."
->DONE

=== Anton ===
{
- not BarChat1:
		Anton<"Serve me the usual, {barman}."
		->DONE

- BarChat1 and not NudoFarmChat1:
        ->AntonDefault1

- NudoFarmChat1 and not AntonHouseChat1:
        ->AntonHouseChat1

- AntonHouseChat1 and not hasPieces:
        ->AntonDefault2

- hasPieces and not AntonHouseChat2:
        ->AntonHouseChat2
- else:
    Anton<“Those trophies mean the world to me."
    ->DONE
 }

===AntonDefaultItem
Anton<"I doubt we can play chess with that."
TODO try the minigame with it.
->DONE

===AntonHouseChat1
Anton<“Strange."

Anton<“I spent all my life fighting for these trophies and teaching other people how to play."

Anton<“But once the time to leave came, I couldn’t bring myself to take them, and at the same time, I couldn’t let myself to leave them."

Anton<“It was as if they belonged here, in this small town, and nowhere else. They stayed in my place."

//He turns around to face the player.
Anton<“Would you {play} one last game with me?"

    + “Sure thing."
    Anton<“Thanks."

	+ “I- don’t know how to play."
    Anton<“Don’t worry, the rules are simple."

-
Anton<“All you have to do is capture my King with your Knight. My Rooks and Bishops will move in the same pattern every time you move your Knight."

Anton<“The townspeople are sick of playing against me, hehe, so I’m glad you’re up to the challenge."

~chessPiecesLoc = "_8_Bar"

Anton<“Oh- I think I forgot my {chessPiece} at the bar… Would you be so kind as to {bring} them back, please?"
Anton<“With this age, my knees hurt for any small movement, and walking all the way there again…"
->DONE

===AntonDefault1
Anton<"Looking for Nudo huh?"
Anton<"He should be in the farm, all the way to the right of here."
->DONE

===AntonDefault2
Anton<"The pieces should be somewhere in the bar."
->DONE

===BarChat2
Helen<"Hey, welcome again."

Helen<“Did you find {nudo}?"

Patrick<“Yes, he asked me to help him with the farm."

Helen<“Haha, that’s {nudo} for you, always making other people help out."

Patrick<“But he wasn’t able to help me out."

Helen<“So, he doesn’t have a {carTire}, then? How strange…"

Patrick<“No, he did give me one, but it was a {tractorTire}."

Helen<“Hahaha, a {tractorTire}."

Patrick<“Do you know anyone else who may have an extra {carTire}?"

Helen<“Not really. {tora} is the one who knows everything around here."

Patrick<“She told me that {nudo} may have one, but I guess she was wrong."

Helen<“…"

Helen<“If {tora} doesn’t know anyone-"

Helen<“Never mind…"

Helen<“Have you tried asking {anton}?"

Helen<"He should be in his home. It's in the oposite side of the plaza, west of here."
->DONE

===BarDefaultAfterNudoBeforeAnton
Helen<"{anton} should be in his home. It's in the oposite side of the plaza, west of here."
->DONE

===BarChat3
Helen<"So, what did {anton} say? Does he have what you're looking for?"

Patrick<“The chess player? I haven't asked him yet. Actually, I was looking for his chess set, he seemed very eager to play a game with me."

Helen<“Yeah, poor {anton} is like that, always looking for a new partner to play with. He hasn’t been able to play new people since the chess club closed."

//Can send the player to pick up the pieces at Rosanna’s house.
Helen<“His pieces should be on that table over there, just {pick} them up."
->DONE

===BarmanDefault3
Helen<“The pieces should be on that stool over there, just {pick} them up."
->DONE

===AntonHouseChat2
Anton<“You found them, thank Goodness. Let’s play, then."
Anton<“Let me tell you the rules again."
Anton<“All you have to do is capture my King with your Knight. My Rooks and Bishops will move in the same pattern every time you move your Knight."
~toraLoc = "_999_Limbo"

//Start the minigame.
~ startMinigame(12)

//After winning the minigame, the room transforms, lightening up, getting tidy, and with a few children running about.
Anton<“Ah, the memories… How I missed this, playing against a new friend, thank you."

Patrick<“I’m glad I could help."

Patrick<“Em… Anton, would you, by any chance, have an extra {carTire} I can borrow?"

Anton<“A {carTire}… No, I don’t think I have one. You see, I left town in my car, like everyone else. I’m not sure if any car was left behind. 
Anton<“Tora was the last one to leave, she should know if any car was left behind."

Anton<“The poor thing blames herself for the death of the town."


	+“Why is that?"

	Anton<“Her father was the only doctor in town, and when he died… the person responsible for the maintenance of the fountain and a few others left town."
	Anton<“Tora found herself alone, accompanied only by the fountain. She rejected everyone’s help."

	+“Maybe it’s really her fault."
	“I doubt that. She’s a good kid, it was an accident. Things break, people die, and memories get forgotten, that’s the way of life."

    Patrick<“What do you mean?"

    Anton<“Her father was the only doctor in town, and when he died… the person responsible for the maintenance of the fountain and a few others left town."
    Anton<“Tora found herself alone, accompanied only by the fountain. She rejected everyone’s help."

-
Patrick<“How sad… it must have been tough for her."

Anton<“Yes, it was."

Anton<“Maybe you should try asking {fionna} about an extra tire, her partner’s car could have been left behind."

Anton<“You can find her in the norteast of town, in the chicken coop."

Patrick<“Okay, thanks."

Anton<“No. Thanks to you, son."

Anton<“Thanks for playing one last game of chess with me."

Anton<“I almost forgot how good it feels..."

~startCinematic()
->DONE

=== Fionna ===
{
- not AntonHouseChat2:
    ->FionnaDefault1

- AntonHouseChat2 and not FionnaChat1:
    ->FionnaChat1
- else:
    Fionna<“Oh, my beautiful chicken."
    ->DONE
}
->DONE

===FionnaChat1
Fionna<“Hey, welcome to our little town."
//Maybe the player can see the chicken from the beginning, throughout the whole map?
Patrick<“Hi, I was looking for {fionna}, would you know where I can find her?"

Fionna<“Yes, it’s me. You're the new guy in town everyone is talking about. {tora} was looking for you."

Patrick<“For me?"

Fionna<“Yes, you. I can tell you where to find her, but first, can you help me out with the chicken? I have been all day trying to catch them, but they’re as playful as their owner."

    +“I guess I don’t have another option, do I?"
    Fionna<“Nop 😊."
    Patrick<“You said they are as playful as their owner, who’s their owner?"

	+“Who’s their owner?"

-
Fionna<“It was… Claire, my late wife. She died of illness when I was on a business trip. If I only stayed with her, I could have taken her to the doctor."

Fionna<“…"

Patrick<“Was it before Tora’s father… died?"

Fionna<“You know about Tora’s father?"

Fionna<“It was after, that’s why Tora- well, never mind."

	+ “What happened to Tora’s father?"
	Fionna<“I- I rather not say. You should ask Tora herself."

	+ “Where is Tora?"
	Fionna<“If I told you now, you’d run away from me, wouldn’t you?"

-
Fionna<“Help me out with the chicken and we’ll talk about Tora’s whereabouts so you can ask her."

Patrick<“Yea, but I came here to ask you a different questi-"

Fionna<“No buts, time to catch some chicken."

~startMinigame(14)
//After finishing the minigame and seeing the scene get alive…
Fionna<“Claire would thank you with an apple pie. I’m not that good with cooking, so I’ll answer your questions as promised."
~toraLoc = "_3_FarVillage"

	+ “Where can I find Tora?"
	Fionna<“She’s went to check on your car, in the forest."

	//Fionna<“She’s in the viaduct, east of town. She often goes there to visit her father’s tomb."
	+ “Do you have an extra car tire?"
	Fionna<“A {carTire}? No, I don’t have one, but Tora should know where you can find one. Didn’t she tell you yet? You can {find} her where you broke your car, in the forest."

-
~startCinematic()
->DONE

===FionnaDefault1
Fionna<"Oh, these damn chicken won't stay still."
->DONE

===FionnaTractorTire
Fionna<"Where did you get that from?"
->DONE

===FionnaBikeTire
Fionna<"A {bikeTire}, it can come in handy if you have a bike."
->DONE

===ToraChat2Car
Tora<"Hey..."
Patrick<"{fionna} said you were looking for me."
Tora<"I- I have something to tell you."
Patrick<"O...kay, sure."
Tora<"I'm- I have the thing you're looking for. A- {carTire}."
Patrick<"That's great."
Tora<"You're not... mad at me, for not telling you sooner?"
    + "No, of coruse not. Why would I be mad at you?"
    Patrick<"I got to know very interesting people, and I even got the chance to help them."
    Tora<"Really? Thank you, thank you very much for understanding."
    Tora<"It- it was my fault that the fountain broke... And I have been waiting for so long for someone to fix it..."

    + "Well, I'm a little annoyed, to be honest. I only thing I want right now is to go home and sleep."
    Tora<"I'm very sorry, I truly am."
    Tora<"It's just- I wanted the town to go back to life so badly, and there's no one else to help-"
-
~giveItem(2)
Tora<"Once more, I'm sorry for trying to deceive you. Here's the {carTire}. I hope you have a safe return to your home. If you ever feel like visiting us, you're always welcome."

->DONE

=== PickUpPieces ===
~ hasPieces = true
->Anton
->DONE

=== BarmanTractorTire ===
Helen<"Get that thing away from my counter, you'll scratch it."
->DONE

=== BarmanChessPieces ===
Helen<"You found them, {anton}'s {chessPiece}."
Helen<"He loves this set. He's always bragging about winning against some World Champion with them."
->DONE



->END
