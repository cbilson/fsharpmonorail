#light
namespace FSharpMonoRail.Model

type Rate() =
    let mutable _value = 0M

    abstract Value : decimal with get, set
    
    default x.Value
        with get() = _value
         and set(v:decimal) = _value <- v

