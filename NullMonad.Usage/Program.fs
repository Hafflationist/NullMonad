module NullMonad.Usage.Program

open NullMonad


let normalCalculation x = x * x

let shelledCalculation = normalCalculation >> Shell.lift

let otherCalc x = x / 2 + 44


[<EntryPoint>]
let main _ =
    
    let result1 = shelledCalculation 42
                  |> Shell.map otherCalc
                  |> Shell.bind shelledCalculation
                  |> Shell.unlift
    
    printfn "%i" result1
    
    shelled {
        let! inter = shelledCalculation 42
        let! result2 =  inter |> otherCalc |> shelledCalculation
        printfn "%i" result2
    } |> ignore
    
    0
