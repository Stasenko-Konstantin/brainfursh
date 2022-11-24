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

let rec scan (input: string) =
    let input = (input.ToCharArray ()) |> Array.toList
    match input with
    | head :: tail ->
        eval head
        List.fold
            (fun x e -> x + e.ToString ())
            "" tail |> scan
    | [] -> ()

let rec repl () =
    printf "    "
    System.Console.ReadLine () |> scan
    repl ()
    
let scanFile file =
    (System.IO.File.ReadAllText file) |> scan

[<EntryPoint>]
let main args =
    if args.Length = 1
    then args[0] |> scanFile
    else repl ()
    0
