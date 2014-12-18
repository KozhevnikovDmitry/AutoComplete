## AutoComplete base ##

**AutoComplete** is an implementation of base test job for SKB Contur.


### Overview ###

Console application must read vocabulary and prefixes from standard input and print autocomplete sets for every prefixes by vocabulary to standard output. If application receive single parameter(path to file), it will try to read vocabulary and prefixes from file. 


### Perfomance ###

Application must work 10 seconds or less on the vocabulary of 100k words, and 15k prefixes, which are provided by test.in file. 

### Hack ###

Sorce code contains commented hack (SheakyHack class), that improves perfomance extreamly, by hiding console window while output is performing. The slowest operation is <code>Console.WriteLine()</code>, that's why window hiding increases perfomance.


