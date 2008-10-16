#light

namespace FSharpMonoRail

open System
open System.Web
open Castle.Windsor

type Application() as x =
    inherit System.Web.HttpApplication()
    
    [<DefaultValue(false)>]
    static val mutable container : AppContainer option
    
    member x.Application_Start () =
        if Application.container = None then Application.container <- Some(new AppContainer())
    
    interface IContainerAccessor with
        member x.Container 
            with get() = Option.get Application.container :> IWindsorContainer
    
        