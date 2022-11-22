let mutable i = 0
let mutable arr: char[] = Array.create 30000 'a'

let charToInt c = int c - int '\u0000'

let inc () =
    let c = (charToInt arr[i]) + 1
    arr[i] <- c |> char
    
let dec () =
    let c = (charToInt arr[i]) - 1
    arr[i] <- c |> char

let eval char =
    match char with
    | '>' -> i <- i + 1
    | '<' -> i <- i - 1
    | '+' -> inc ()
    | '-' -> dec ()
    | '.' -> printf $"{arr[i]}"
    | ',' -> arr[i] <- (System.Console.ReadKey ()).KeyChar
    | '[' -> ()
    | ']' -> ()
    | _ -> ()

let rec scan input =
    match input with
    | head :: tail ->
        eval head
        scan tail
    | [] -> ()

let rec repl () =
    let input = System.Console.ReadLine ()
    (input.ToCharArray ()) |> Array.toList |> scan
    repl ()

repl ()

