﻿(* 
Copyright (c) 2008, Raymond W. Vernagus (R.Vernagus@gmail.com)
All rights reserved.

Redistribution and use in source and binary forms, with or without modification,
are permitted provided that the following conditions are met:

  * Redistributions of source code must retain the above copyright notice,
    this list of conditions and the following disclaimer.
  * Redistributions in binary form must reproduce the above copyright notice,
    this list of conditions and the following disclaimer in the documentation
    and/or other materials provided with the distribution.
  * Neither the name of Raymond W. Vernagus nor the names of FsUnit's
    contributors may be used to endorse or promote products derived from this
    software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*)
#light

namespace FsUnit

type Result =
    | Pass
    | Fail of string
    | Error of string

type Expectation =
    | True
    | False
    | Empty
    | NullOrEmpty
    | Null
    | SameAs of obj

type Spec =
    abstract Check : unit -> Result
    abstract NegatedMessage : string

[<AutoOpen>]
module SpecOps =
    let internal safeCheck f =
        try
            f()
        with ex -> Error (ex.ToString())
    
    let make f nmsg =
        { new Spec with
            member this.Check() = safeCheck f
            member this.NegatedMessage = nmsg }
    
    let check (s: Spec) = s.Check()
    
    let not' f x =
        let s = f x
        match check s with
        | Pass      -> make (fun () -> Fail s.NegatedMessage) ""
        | Fail msg  -> make (fun () -> Pass) msg
        | Error msg -> make (fun () -> Error msg) msg
    
    let equal expected actual =
        make (fun () ->
            if actual = expected 
                then Pass
                else Fail (sprintf "Expected: %A\nActual: %A" expected actual))
            (sprintf "NOT Expected: %A\nActual: %A" expected actual)

    let have n lbl s =
        make (fun() ->
            let len = Seq.length s
            if len = n
                then Pass
                else Fail (sprintf "Expected: %d %s\nActual: %d %s" n lbl len lbl))
            (sprintf "NOT Expected: %d %s\nActual: %d %s" n lbl n lbl)
    
    let contain x s =
        make (fun () ->
            let exists = s |> Seq.exists (fun x' -> x' = x)
            if exists
                then Pass
                else Fail (sprintf "Expected %A to contain %A" s x))
            (sprintf "Did NOT expect %A to contain %A" s x)

    let raise'<'a when 'a :> System.Exception> f =
        let expType = typeof<'a>
        make (fun () -> 
            try
                f()
                Fail (sprintf "Expected %s but no exception was raised" expType.FullName)
            with ex ->
                let actualExType = ex.GetType()
                if expType = actualExType
                    then Pass
                    else Fail (sprintf "Expected: %s\nActual: %s" expType.FullName actualExType.FullName))
            (sprintf "Did NOT expect %s to be raised" expType.FullName)

    let be expectation x =
        let x = box x
        let msg = sprintf "Expected: %A\nActual: %A" expectation x
        let negmsg = sprintf "NOT Expected: %A\nActual: %A" expectation x
        match expectation with
        | True ->
            make (fun () ->
                if x = box true
                    then Pass
                    else Fail msg)
                negmsg
        | False ->
            make (fun () ->
                if x = box false
                    then Pass
                    else Fail msg)
                negmsg
        | Empty ->
            make (fun () ->
                if x = box System.String.Empty
                    then Pass
                    else Fail msg)
                negmsg
        | NullOrEmpty ->
            make (fun () ->
                if System.String.IsNullOrEmpty(x :?> string)
                    then Pass
                    else Fail msg)
                negmsg
        | Null ->
            make (fun () ->
                if x = null
                    then Pass
                    else Fail msg)
                negmsg
        | SameAs other ->
            make (fun () ->
                if System.Object.ReferenceEquals(x, other)
                    then Pass
                    else Fail (sprintf "Expected actual to be same reference as expected %A" other))
                (sprintf "Expected %A to have different reference than %A" x other)

module Results =
    open Microsoft.FSharp.Text.Printf
    
    let internal currentResults = new ResizeArray<string * Result>()
    
    let add = currentResults.Add
    
    let passedCount () =
        currentResults
        |> Seq.filter (function _,Pass -> true | _ -> false)
        |> Seq.length
    
    let failed () =
        currentResults
        |> Seq.filter (function _,Fail _ -> true | _ -> false)
    
    let failedCount () =
        failed()
        |> Seq.length
    
    let erred () =
        currentResults
        |> Seq.filter (function _,Error _ -> true | _ -> false)
    
    let erredCount () =
        erred()
        |> Seq.length
        
    let summary () =
        let buff = new System.Text.StringBuilder()
        bprintf buff "%d passed.\n%d failed.\n%d erred." (passedCount()) (failedCount()) (erredCount())
        failed()
        |> Seq.iter (function (lbl,Fail msg) -> bprintf buff "\n----\nFailed: %s\n%s" lbl msg | _ -> ())
        erred()
        |> Seq.iter (function (lbl,Error msg) -> bprintf buff "\n----\nErred: %s\n%s" lbl msg | _ -> ())
        buff.ToString()
    

[<AutoOpen>]
module SpecHelpers =
    let spec lbl s = (lbl, check s)
    
    let should f x = f x
    
    let specs lbl (results: seq<string * Result>) =
        results |> Seq.iter (fun x -> Results.add x)
