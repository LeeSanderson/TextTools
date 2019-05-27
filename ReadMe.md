# TextTools #

TextTools is a library of utilities for analyzing text.

Features:

- Uses enumerations and buffers to efficiently process text loaded from streams.
- Interface abstractions allow drop in replacement of tokenizer and word filters.
- Composition of filters allows multiple filter classes to be chained together to create a pipeline (e.g. lowercase and remove stop words).  
- Console application can be easily extended using the command pattern (currently only a word count command is supported - this counts the number of unique words in a text file and outputs the top 10 results ordered by their count).
- 100% code coverage on main TextAnalysis assembly.


Future enhancements:

- Use C# 8 [nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/nullable-reference-types) feature instead of JetBrains [NotNull] code annotations (once LTS version of .netcore 3 is released).
- Implement async version of tokenizer and filter to improve performance when reading from async streams.
- Create more advanced tokenizer that handles contractions and possessive apostrophes.


## Benchmarks ##

The following results of the word counting algorithm were generated using the text from Lord Of the Rings: Fellowship of the Ring. In generating the results we make the following assumptions:

- Words boundaries are defined by any white space or punctuation character.
- Words should be compared case-insensitively i.e. the words "the" and "The" should be considered the same word for the purposes of counting. 

### Initial results ###
Running the text of Fellowship of the Ring through the count command with the following command generates these results:

> dotnet TextTool.Console.dll count -i LOTR1.txt

1. the - 11795
2. and - 7566
3. of - 5111
4. to - 3951
5. a - 3730
6. he - 3027
7. in - 2962
8. i - 2820
9. it - 2784
10. that - 2541

> Command completed in 172 ms.

Unsurprisingly the results contain the most common words used in English. 


### Filtering stop words ###
More interesting results can be generated by removing the common English words by providing a stop word list. We can do this with the following command:

> dotnet TextTool.Console.dll count -i LOTR1.txt -s StopWordsEn.txt

1. said - 1481
2. frodo - 1101
3. s - 698
4. now - 679
5. will - 563
6. t - 476
7. gandalf - 467
8. long - 456
9. came - 436
10. one - 430

> Command completed in 203 ms.

These results are more interesting - containing two of the names of the main characters (**Frodo** and **Gandalf**) as well an **one** (as in "One Ring to rule them all, One Ring to find them, One Ring to bring them all and in the darkness bind them").

We also have the *words* **s** and **t**. These are to be expected due to the simple way in which we handle word boundaries - words like *Helm's* and *Shelob's* generate **s** tokens and words like *isn't* and *shouldn't* generate **t** tokens.


### Ignoring short words ###
One simple way to remove the **s** and **t** *words* from the results is to use apply the minimum length filter so that short words are removed. We can removed words less that 2 characters long with the following command:
 
> dotnet TextTool.Console.dll count -i LOTR1.txt -s StopWordsEn.txt -m 2

1. said - 1481
2. frodo - 1101
3. now - 679
4. will - 563
5. gandalf - 467
6. long - 456
7. came - 436
8. one - 430
9. like - 411
10. sam - 409

> Command completed in 203 ms.

Now as well as **Frodo** and **Gandalf** appearing in the results we also have Frodo's best friend **Sam**.

### Counting n-grams ###

Counting n-grams, sequences of words, can give us more insight into the structure of the text.

We can generate and count bi-grams (sequences of 2 word) with the following command:

> dotnet TextTool.Console.dll count -i LOTR1.txt -s StopWordsEn.txt -m 2 -n 2


1. said frodo - 220
2. said gandalf - 135
3. said aragorn - 65
4. said sam - 62
5. said merry - 59
6. bag end - 56
7. long ago - 55
8. said pippin - 55
9. chapter chapter - 53
10. said strider - 51

> Command completed in 655 ms.

The bi-grams seem to show the main characters and how frequently they talk. We also see **chapter chapter** - a noise bi-gram that is generated because of the way the table of contents of the book is structured. 

We can also generate and count tri-grams (sequences of 3 word) with the following command:

> dotnet TextTool.Console.dll count -i LOTR1.txt -s StopWordsEn.txt -m 2 -n 3


1. chapter chapter chapter - 47
2. let us go - 10
3. don know said - 8
4. sam said frodo - 8
5. said mr butterbur - 8
6. aragorn son arathorn - 8
7. frodo said gandalf - 6
8. sir said sam - 6
9. said frodo looking - 6
10. mr frodo said - 6

> Command completed in 749 ms.

The tri-grams show conversations between the main characters - Gandalf talking to Frodo, Frodo talking to Sam and Sam talking to Frodo (as sir). We also see the expected **chapter chapter chapter** noise tri-gram and Aragorn being mentioned as **Aragorn son [of] Arathorn** - when he introduces himself or is welcomed by other characters throughout the book. 


## Further Analysis ##

Future version of the library could be enhanced to include additional ways to analyze the text. This could include:

- Identification of named characters and places by filtering the words to only include tokens that are capitalized (ignoring words that are at the start of sentences).
- Using n-gram analysis to detect plagiarism by comparing multiple documents. 