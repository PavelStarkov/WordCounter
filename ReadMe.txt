WordCounter console app – getting distinct list of words and counts the number of times they have occurred.
List of options:
-i  [[path to file] [“actual text”]] – input e.g.: (wordcounter –i “test.txt”; wordcounter –i “sample text”). Value should be in quotes   

-ds [[ht] [sl]] – data structure. For now, user could choose from two implementations: ht – HashTable and sl – SortedList. Default is HashTable. E.g. (wordcounter –i “test.txt” –ds ht )

-sa [[qs] [ds] [hs]] – sorting algorithm. To show result in ordered way, based on word occurrence. For now, user could choose from: qs – QuickSort, ds – Distribution Sort, hs – HybridSort (based on fast input data check, one of two above will be chosen) E.g. (wordcounter –i test.txt –sa qs)

-w [number of threads] – app allows run counting process in multithreaded way. Worker count option allows user to set up amount of parallel workers. E.g. (wordcounter –i test.txt –w 4) By default application is running in one main thread.

-cs [string message chunk size number] - Chunk size  of string messages, that goes to worker’s queue  in multithreaded run. Default value = 50. E.g. (wordcounter –i text.txt –w4 –cs 1000)
