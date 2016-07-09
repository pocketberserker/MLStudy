module MLStudy.Program

open MLStudy.TypeProviders

let inline dump (value: ^n) = (^n: (member FizzBuzz: string) value) |> printfn "%s"

type FizzBuzz = FizzBuzzProvider<1, 100>

[<EntryPoint>]
let main _ = 
  dump FizzBuzz.``1``
  dump FizzBuzz.``2``
  dump FizzBuzz.``3``
  dump FizzBuzz.``4``
  dump FizzBuzz.``5``
  0
