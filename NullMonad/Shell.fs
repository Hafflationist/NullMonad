namespace NullMonad

type 'a shell = | Inner of 'a  

module Shell =
    
    let bind func = function
        | Inner content -> func content
        
    let map func = function
        | Inner content -> content |> func |> Inner
        
    let lift value = Inner value
    
    let unlift = function
        | Inner content -> content



[<AutoOpen>]
module ShellComputation =
    
    [<Struct>]
    type ShellBuilder =
        
        member __.Zero() = Inner ()
        
        member __.Bind(shell, binder) = Shell.bind binder shell
        
        member __.Return(value) = Shell.lift value
        
        member __.ReturnFrom(valueShelled) = valueShelled 
        
    let shelled = ShellBuilder()