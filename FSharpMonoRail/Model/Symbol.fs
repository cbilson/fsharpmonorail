#light
namespace FSharpMonoRail.Model

type Symbol() =
    let mutable _name = ""

    abstract Name : string with get, set
    
    default x.Name
        with get() = _name
         and set(v:string) = _name <- v

