#light

namespace FSharpMonoRail.Controllers

open Castle.MonoRail.Framework

type HelloController() =
    inherit SmartDispatcherController()
    
    member x.Index() =
        printfn "Hello"
        