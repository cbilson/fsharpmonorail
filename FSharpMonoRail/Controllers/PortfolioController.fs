#light

namespace FSharpMonoRail.Controllers

open System.Diagnostics
open Castle.MonoRail.Framework
open FSharpMonoRail.Services

[<Layout("Default")>]
type PortfolioController() =
    inherit SmartDispatcherController()
    
    member x.Index() =
        ignore
            