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
        x.RegisterControllers()
    
    member private x.RegisterControllers : unit = 
        let controllers = AllTypes.Of<Controller>()
        let basedOn = controllers.FromAssembly(x.GetType().Assembly)
        let registrations = basedOn :> IRegistration
        registrations.Register(x.Kernel)
        
    