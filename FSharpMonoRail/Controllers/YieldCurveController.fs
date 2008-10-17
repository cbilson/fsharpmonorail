#light

namespace FSharpMonoRail.Controllers

open System.Diagnostics
open Castle.MonoRail.Framework
open FSharpMonoRail.Services

type YieldCurveController(rateRepository:IRateRepository) =
    inherit SmartDispatcherController()

    member x.Index() = ignore
            
    