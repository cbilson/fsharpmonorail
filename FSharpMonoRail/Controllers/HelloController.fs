#light

namespace FSharpMonoRail.Controllers

open System.Diagnostics
open Castle.MonoRail.Framework

type HelloController() =
    inherit SmartDispatcherController()
    
    member x.Index() =
        printfn "Hello"
        
