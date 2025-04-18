open System
open System.Globalization
open System.Text.RegularExpressions

//take user input
printfn "Please enter some text: "
let string = System.Console.ReadLine()

//split string into words using Trim
let words = string.Split " "
let sentences = string.Split([|'.'; '?'; '!'|])

//count number of words and sentences
let numWords = words.Length
let numSentences = (sentences |> Array.filter(fun x ->
                                                        match x with
                                                        |"" -> false
                                                        | _->  true)).Length //does not count blank character at end

//make all words in "words" lowercase
let stringsToLower words =  List.map(fun (word:string) -> word.ToLower(new CultureInfo("en-US", false))) words
let wordList = words |> Array.toList
let LowerWords:string list = stringsToLower wordList

//find and display most frequently occuring words

//make the array of lowercase words a single string
let concatLowerWords = String.concat " " LowerWords

//get rid of punctuation
let strip chars = String.collect (fun c -> if Seq.exists((=)c) chars then "" else c.ToString())

let noPunctuation = strip ".?!" concatLowerWords

//find word occurances
//Adapted from https://fsharpforfunandprofit.com/posts/monoids-part2/
let wordOccurances (s) =
        Regex.Matches(s,@"\S+")
        |> Seq.cast<Match>
        |> Seq.map (fun m -> m.ToString())
        |> Seq.groupBy id
        |> Seq.map (fun (k,v) -> k,Seq.length v)
        |> Seq.sortBy (fun (_,v) -> -v)
        |> Seq.toList

let concatRegularWords = String.concat " " sentences

//find proper nouns
//Regex adapted from: https://stackoverflow.com/questions/19691391/regex-find-proper-nouns-or-phrases-that-are-not-first-word-in-a-sentence
let properNounFinder (s) =
    Regex.Matches(s,@"(?<!^|\. |\? |\! |  )[A-Z][a-z]+")
    |> Seq.cast<Match>
    |> Seq.toList

let uniqueWordCount = wordOccurances noPunctuation

let properNouns = properNounFinder concatRegularWords

//displaying number of words
printfn "Number of words: \n %A \n" numWords

//displaying number of sentences
printfn "Number of sentences: \n %A \n" numSentences

//displaying unique word count
printfn "Unique word count: \n %A \n" uniqueWordCount

//displaying proper nouns
printfn "Proper nounns: \n %A" properNouns

(*
let replace chars = String.map (fun c -> if Seq.exists((=)c) chars then '\n' else c)

let uniqueWordCountString = uniqueWordCount.ToString()

let neatUniqueWordCountString = strip "[]()"uniqueWordCountString
replace ";" neatUniqueWordCountString


printfn "%s" neatUniqueWordCountString
*)

