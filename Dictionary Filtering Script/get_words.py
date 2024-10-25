# Used for querying the dictionary based on custom set of criteria. Used for brainstorming various combinations of words for testing the program with and does not output any text files unlike filter_dictionary.py.

import os
import string
import array

def has_duplicate_letters(word):
    """Check if the word contains duplicate letters."""
    return len(word) != len(set(word))

def has_five_letters(word):
    """Check if the word has 5 letters"""
    return len(word) == 5

def contains_non_letter_chars(word):
    """Checks the word for punctuation symbols"""
    for char in word:
        if char not in string.ascii_letters:
            return True
    return False


if __name__ == "__main__":
    # Change these file names as needed
    input_file = 'english3.txt'
    output_file = 'dictionary.txt'
    
    script_path = os.path.dirname(os.path.abspath(__file__))
    input_path = os.path.join(script_path, input_file)
    
    """Filter words from the input file and write to the output file."""
    with open(os.path.join(input_path), 'r') as infile:
        words = infile.read().splitlines()

    # filtered_words = []
    # for letter in string.ascii_lowercase:
    #     for word in words:
    #         if has_five_letters(word) and word.count(letter) == 3:
    #             filtered_words.append(word)

    # for word in filtered_words:
    #     print(word + '\n')

    filtered_words = [word for word in words if word.count('\'') == 1 and len(word.replace('\'', '')) == 5]
    print([word for word in filtered_words])
    print(len(filtered_words), "words found")
