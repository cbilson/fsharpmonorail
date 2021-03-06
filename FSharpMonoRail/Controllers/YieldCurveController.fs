﻿#light

namespace FSharpMonoRail.Controllers

open System.Diagnostics
open Castle.MonoRail.Framework
open FSharpMonoRail.Services

[<Layout("Default")>]
type YieldCurveController(rateRepository:IRateRepository) =
    inherit SmartDispatcherController()

    member x.Index() = 
        let symbols = None
        x.PropertyBag.Add("Symbols", symbols)
        
            
    