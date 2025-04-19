open System
open System.Globalization
open System.Text.RegularExpressions

//take user input
printfn "Please enter some text: "

//convert user input to a string called "string"
let string = System.Console.ReadLine()

//split string into words and sentences using Split

//words array
let words = string.Split " "

//sentences array
let sentences = string.Split([|'.'; '?'; '!'|])

//count number of words - store length of "words" array into "numWords" variable
let numWords = words.Length

//count number of sentences - store length of "sentences" array into "numSentences" variable
let numSentences = (sentences |> Array.filter(fun x ->
                                                        match x with
                                                        |"" -> false
                                                        | _->  true)).Length //does not count blank character at end

//make all words in "words" lowercase - store new words in "stringsToLower" list
let stringsToLower words =  List.map(fun (word:string) -> word.ToLower(new CultureInfo("en-US", false))) words

//convert "words" array to a list called "wordList"
//necessary for being processed by functions that take lists
let wordList = words |> Array.toList
let LowerWords:string list = stringsToLower wordList

//find and display most frequently occuring words

//make the array of lowercase words a single string
let concatLowerWords = String.concat " " LowerWords

//get rid of punctuation

//Adapted from https://stackoverflow.com/questions/20308875/remove-characters-from-string-in-f
let strip chars = String.collect (fun c -> if Seq.exists((=)c) chars then "" else c.ToString())

let noPunctuation = strip ".?!,-:" concatLowerWords

//find word occurances
//Adapted from https://fsharpforfunandprofit.com/posts/monoids-part2/
let wordOccurances (s) =
        Regex.Matches(s,@"\S+")
        |> Seq.cast<Match> //turns MatchCollection into a Match seq
        |> Seq.map (fun m -> m.ToString()) //turns each Match into a string
        |> Seq.groupBy id
        |> Seq.map (fun (k,v) -> k,Seq.length v)
        |> Seq.sortBy (fun (_,v) -> -v)
        |> Seq.toList

let uniqueWordCount = wordOccurances noPunctuation

//find proper nouns
//Regex adapted from: https://stackoverflow.com/questions/19691391/regex-find-proper-nouns-or-phrases-that-are-not-first-word-in-a-sentence
let properNounFinder (s) =
    Regex.Matches(s,@"(?<!^|\. |\? |\! |  )[A-Z][a-z]+")
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.ToString())
    |> Seq.distinct
    |> Seq.toList

let concatRegularWords = String.concat " " sentences

let properNouns = properNounFinder concatRegularWords

//displaying number of words
printfn "Number of words: \n %A \n" numWords

//displaying number of sentences
printfn "Number of sentences: \n %A \n" numSentences

//displaying unique word count
printfn "Unique word count: \n %A \n" uniqueWordCount

//displaying proper nouns
printfn "Proper nouns: \n %A" properNouns

(*
let replace chars = String.map (fun c -> if Seq.exists((=)c) chars then '\n' else c)

let uniqueWordCountString = uniqueWordCount.ToString()

let neatUniqueWordCountString = strip "[]()"uniqueWordCountString
replace ";" neatUniqueWordCountString


printfn "%s" neatUniqueWordCountString
*)

