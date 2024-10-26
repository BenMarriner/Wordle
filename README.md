# Wordle
 A CLI program that plays Wordle. Requires .NET 8.0
 
Below, I have listed some assumptions that were made based on the requirements of the task:
1. The original unfiltered dictionary file was obtained from http://gwicks.net/dictionaries.htm as per the task's requirements
2. The dictionary file gets copied from the Resources folder to the target folder as part of a post build event. Please ensure the Resources folder exists along with the dictionary file as the game will terminate if it cannot be found
3. The dictionary file was filtered using the filter_dictionary python script in the Dictionary Filtering Script folder
4. The dictionary was filtered to only contain words that meet the following criteria as per the task's requirements:
- The word has five letters
- The word does not contain any non-letter characters (i.e.: grammatical symbols)
5. The player gets six guesses
6. The game terminates early if the correct word is guessed
7. The game terminates after six incorrect guesses and displays the target word
8. The game will only accept words from the player that are found in the dictionary

## Update
The program has been updated to allow words with multiple occurrences of the same letter and the algorithm that evaluates the colours of each letter has been rewritten to account for this.
All words with multiple occurrences of the same letter have also been re-added to the dictionary.
Various other improvements and code refactorings have also been made.