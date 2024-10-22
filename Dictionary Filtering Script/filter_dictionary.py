import os
import string

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


def filter_words(input_file, output_file):
    """Filter words from the input file and write to the output file."""
    with open(input_file, 'r') as infile:
        words = infile.read().splitlines()

    filtered_words = [word for word in words if not has_duplicate_letters(word) and has_five_letters(word) and not contains_non_letter_chars(word)]

    with open(output_file, 'w') as outfile:
        for word in filtered_words:
            outfile.write(word + '\n')

    print(f"Filtered words written to {output_file}")

if __name__ == "__main__":
    # Change these file names as needed
    input_file = 'english3.txt'
    output_file = 'dictionary.txt'
    
    script_path = os.path.dirname(os.path.abspath(__file__))
    input_path = os.path.join(script_path, input_file)
    output_path = os.path.join(script_path, output_file)
    
    filter_words(input_path, output_path)
