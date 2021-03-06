#light

namespace FSharpMonoRail.Services
open System
open Castle.Facilities.NHibernateIntegration
open Castle.Facilities.Logging
open FSharpMonoRail.Model

type IRateRepository =
    abstract GetRatesForSymbolOnDate : s:Symbol * d:DateTime -> Rate list

type RateRepository() =
    member x.Foo(symbol:Symbol, date:DateTime) : Rate list =
            let rate1 = new Rate()
            let rate2 = new Rate()
            [rate1; rate2]
            
    interface IRateRepository with
        member x.GetRatesForSymbolOnDate(symbol:Symbol, date:DateTime) : Rate list =
            let rate1 = new Rate()
            let rate2 = new Rate()
            [rate1; rate2]
    
    
    