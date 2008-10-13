#light

namespace FSharpMonoRail

open System.Web
open Castle.Windsor

type Application() =
    inherit System.Web.HttpApplication()
    static member container = new AppContainer()
    
    interface IContainerAccessor with
        member x.Container 
            with get() = container
    
        