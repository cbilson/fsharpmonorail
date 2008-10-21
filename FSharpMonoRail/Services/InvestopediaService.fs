#light

namespace FSharpMonoRail.Services

open System
open System.IO
open SyStem.Net
open FSharpMonoRail.Model

type IInvestopediaService =
    abstract LoginToInvestopedia: userName:string -> password:string -> unit
    
type InvestopediaService =

    interface IInvestopediaSerivce with
        member LoginToInvestopedia userName password =
            let url = "http://simulator.investopedia.com/home.aspx"
            let request = WebRequest.Create(url)
            request.
    
    
    
    