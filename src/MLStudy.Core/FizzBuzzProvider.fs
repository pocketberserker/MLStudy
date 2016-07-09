namespace MLStudy.TypeProviders

open System.IO
open System.Reflection
open Microsoft.FSharp.Core.CompilerServices
open ProviderImplementation.ProvidedTypes
open FSharp.Quotations

[<TypeProvider>]
type FizzBuzzProvider(cfg: TypeProviderConfig) as this =
  inherit TypeProviderForNamespaces()

  let asm = Assembly.GetExecutingAssembly()
  let ns = "MLStudy.TypeProviders"

  let typ = ProvidedTypeDefinition(asm, ns, "FizzBuzzProvider", Some typeof<obj>)
  do
    let parameters = [
      ProvidedStaticParameter("first", typeof<int>)
      ProvidedStaticParameter("last", typeof<int>)
    ]
    typ.DefineStaticParameters(parameters, fun typeName (args: obj[]) ->
      let first = unbox<int> args.[0]
      let last = unbox<int> args.[1]
      let typ = ProvidedTypeDefinition(asm, ns, typeName, Some typeof<obj>, HideObjectMethods = true)
      for n in [first .. last] do
        let fizzbuzz =
          match (n % 3, n % 5) with
          | (0, 0) -> "FizzBuzz"
          | (0, _) -> "Fizz"
          | (_, 0) -> "Buzz"
          | _ -> string n
        typ.AddMembersDelayed(fun () ->
          let numType = ProvidedTypeDefinition(sprintf "_%d" n, Some typeof<obj>)
          let ctor = ProvidedConstructor(parameters = [], InvokeCode = fun _ -> <@@ () @@>)
          numType.AddMember(ctor)
          numType.AddMemberDelayed(fun () -> ProvidedProperty("FizzBuzz", typeof<string>, GetterCode = fun _ -> <@@ fizzbuzz @@>))
          let prop =
            ProvidedProperty(
              propertyName = string n,
              IsStatic = true,
              propertyType = numType,
              GetterCode = (fun _ -> Expr.NewObject(ctor, []))
            )
          [
            numType :> MemberInfo
            prop :> MemberInfo
          ]
        )
      typ
    )
    this.AddNamespace(ns, [typ])

[<assembly:TypeProviderAssembly>]
do ()
