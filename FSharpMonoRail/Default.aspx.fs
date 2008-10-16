#light

namespace FSharpMonoRail

open System
open System.Web
open System.Web.UI

type _Default() =
    inherit Page()
    
    member x.Page_Load(sender:obj, e:EventArgs) =
        x.Response.Redirect("~/Hello/Index.mr")    
        