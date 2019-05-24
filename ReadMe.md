# TextTools #

TextTools is a library of utilities for analyzing text.

Features:

- Uses enumerations and buffers to efficiently process text loaded from streams.
- Interface abstractions allow drop in replacement of tokenizer and word filters.
- Composition of filters allows multiple filter classes to be chained together to create a pipeline (e.g. lowercase and remove stop words).  
- Console application can be easily extended using the command pattern.
- 100% code coverage on main TextAnalysis assembly.


Future enhancements:

- Use C# 8 [nullable reference types](https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/nullable-reference-types) feature instead of JetBrains [NotNull] code annotations (once LTS version of .netcore 3 is released).
- Implement async version of tokenizer and filter to improve performance when reading from async streams.
- Create more advanced tokenizer that handles contractions and possessive apostrophes.


## Benchmarks ##

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

Help

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

Command completed in 203 ms.



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

Command completed in 203 ms.