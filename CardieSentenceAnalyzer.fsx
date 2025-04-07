open System

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

//find and display most frequently occurring words

//can't figure out how to make all my words in "words" list be lowercase letters
let rec stringsToLower = function
    | (x:string)::xs -> x.ToLower