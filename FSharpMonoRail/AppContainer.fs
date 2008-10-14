#light

namespace FSharpMonoRail

open System.Reflection
open Castle.Core
open Castle.MicroKernel.Registration
open Castle.MonoRail.Framework
open Castle.Windsor
open Castle.Windsor.Configuration.Interpreters

type AppContainer() as x = 
    inherit WindsorContainer(new XmlInterpreter())
    
    do 
        x.RegisterAll (AllTypes.Of<Controller>())
        
    member x.RegisterAll (types:AllTypesOf) =
        let basedOn = types.FromAssembly(x.GetType().Assembly)
        let registrations = basedOn :> IRegistration
        registrations.Register(x.Kernel)
        
    