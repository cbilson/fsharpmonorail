#light

namespace FSharpMonoRail

open System.Reflection
open Castle.Core
open Castle.MicroKernel.Registration
open Castle.MonoRail.Framework
open Castle.MonoRail.WindsorExtension
open Castle.Windsor
open Castle.Windsor.Configuration.Interpreters

type AppContainer() as x = 
    inherit WindsorContainer(new XmlInterpreter())
    
    do 
        x.RegisterFacilities()
        x.RegisterControllers()
        
    member x.RegisterFacilities() =
        x.AddFacility<MonoRailFacility>() |> ignore
        
    member x.RegisterControllers() = x.RegisterAll(AllTypes.Of<Controller>())
        
    member x.RegisterAll (types:AllTypesOf) =
        let basedOn = types.FromAssembly(x.GetType().Assembly)
        let registrations = basedOn :> IRegistration
        registrations.Register(x.Kernel)
        
    