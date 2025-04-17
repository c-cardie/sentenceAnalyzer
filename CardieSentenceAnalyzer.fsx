open System
open System.Globalization
open System.Text.RegularExpressions

//take user input
printfn "Please enter some text: "
let string = System.Console.ReadLine()
printfn "%s" string

//split string into words using Trim
let words = string.Split " "
let sentences = string.Split "."

//count number of words and sentences
let numWords = words.Length
let numSentences = (sentences |> Array.filter(fun x ->
                                                        match x with
                                                        |"" -> false
                                                        | _->  true)).Length //does not count blank character at end


(*
let rec stringsToLower  = function
    | (x:string)::xs -> Some x.ToLower
        stringsToLower xs
    | [] -> None
*)

//make all words in "words" lowercase
let stringsToLower words =  List.map(fun (word:string) -> word.ToLower(new CultureInfo("en-US", false))) words
let wordList = words |> Array.toList
let LowerWords:string list = stringsToLower wordList

//find and display most frequently occuring words

let concatLowerWords = String.concat " " LowerWords

let strip chars = String.collect (fun c -> if Seq.exists((=)c) chars then "" else c.ToString())

let noPeriods = strip "." concatLowerWords

let mostFrequentWord (s) =
        Regex.Matches(s,@"\S+")
        |> Seq.cast<Match>
        |> Seq.map (fun m -> m.ToString())
        |> Seq.groupBy id
        |> Seq.map (fun (k,v) -> k,Seq.length v)
        |> Seq.sortBy (fun (_,v) -> -v)
        |> Seq.head
        |> fst

mostFrequentWord noPeriods
