# Project notes

This project is what I call "Cannonade". As in "You don't need a cannon to kill a mosquito". Meaning - whenever posible (and at least minimally feasible) I try to avoid implementing clean efficient ready-made code in favour of writing my own solution, even if it is sometimes an overkill.
The purpose of this is twofold. Firstly, I want to do something myself to see if I can do it and to find out how it is done (to quote prof. Philip Moriarty: "If I can't code it, I don't understand it"). Secondly, the more things I try for the first time, the... the more things I will not try for the first time next time. And thirdly, I need this project to focus on problem solving, not established dev pattern application. In short, I am doing it for myself, not for "the sweet Steam money". A cool end product would be welcomed, and certainly "completion" is in the check list, but the development is the ultimate goal, not the result of that development.

So be ready for clumsy overdone solutions. Those are direly needed.


# Main mechanics and general gameplay type

* top-down dungeon crawler

* dungen crawl / exploration / base builder

* PCG  with controllable persistence



# Gameplay notes bullet points

* The hero is trapped inside of a magical device, that creates something of a dungeon by pulling pieces of reality from different dimensions and piecing them together.

* The goal of the hero is to get to a certain persistent dungeon piece, by exploring new pieces in between, in order to activate a magical device and exit the dungeon.

* The inner space is composed of chunks with portals between them (dungeon rooms).

* Each chunk is proc-generated and it's theme is based on pseudo-randomly selected theme from a set of available ones (different dimensions).

* Each chunk has a center, that is used by the magical devide to incorporate it into the inner space.

* Chunks generally disappear after a certain time, which clocks down while the player is not in them. The player has limited (be resources) ability to keep a small number of chosen chunks from disappearing.

* Activities in the dungeon become more and more hard and resource expensive, the further away is the chunk where the activity is performed from the Origin chunk.

* Throughout the course of the game, at the cost of significant amount of resources, the player can change the Origin chunk, so that his base of operations slowly moves through the dungeon.

* At some point, after a certain amount of Origin chunk changes, the End chunk will be in reach and the player will be able to activate it and end the game.
