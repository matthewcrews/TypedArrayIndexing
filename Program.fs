open System
open FSharp
open System.Collections.Generic
open System.Runtime.CompilerServices
open FSharp.NativeInterop
open BenchmarkDotNet.Attributes
open BenchmarkDotNet.Running
open BenchmarkDotNet.Diagnosers


[<Measure>]
type Chicken

type Arr<'T, [<Measure>] 'Measure>(v: array<'T>) =

    member _.Item
        with get (i: int<'Measure>) =
            v[int i]

        and set (index: int<'Measure>) value =
            v[int index] <- value

    member _.Length = LanguagePrimitives.Int32WithMeasure<'Measure> v.Length


[<Struct>]
type StructArr<'T, [<Measure>] 'Measure>(v: array<'T>) =

    member _.Item
        with get (i: int<'Measure>) =
            v[int i]

        and set (index: int<'Measure>) value =
            v[int index] <- value

    member _.Length = LanguagePrimitives.Int32WithMeasure<'Measure> v.Length


[<HardwareCounters(
    HardwareCounter.BranchMispredictions,
    HardwareCounter.CacheMisses
)>]
type Benchmarks () =

    let rng = Random 123
    let loopCount = 10_000_000
    let arraySize = 16
    let randomLookups =
        [|
            for _ = 1 to loopCount * arraySize do
                rng.Next (arraySize)
        |]

    let typedRandomLookups =
        randomLookups
        |> Array.map (fun x -> x * 1<Chicken>)

    let lookupArray =
        [| for _ = 1 to arraySize do rng.Next (0, 100) |]

    let typedLookupArray =
        lookupArray
        |> Arr<_, Chicken>

    let structTypedLookupArray =
        lookupArray
        |> StructArr<_, Chicken>


    [<Benchmark>]
    member _.ArrayRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < randomLookups.Length - 1 do
            let nextLookup = randomLookups[i]
            result <- lookupArray[nextLookup]
            i <- i + 1

        result


    [<Benchmark>]
    member _.TypedArrayRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < typedRandomLookups.Length - 1 do
            let nextLookup = typedRandomLookups[i]
            result <- typedLookupArray[nextLookup]
            i <- i + 1

        result

    [<Benchmark>]
    member _.StructTypedArrayRandom () =
        let mutable result = 0
        let mutable i = 0

        while i < typedRandomLookups.Length - 1 do
            let nextLookup = typedRandomLookups[i]
            result <- structTypedLookupArray[nextLookup]
            i <- i + 1

        result

    [<Benchmark>]
    member _.ArrayOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0
            while i < lookupArray.Length - 1 do
                result <- lookupArray[i]
                i <- i + 1

        result


    [<Benchmark>]
    member _.TypedArrayOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0<Chicken>
            while i < typedLookupArray.Length - 1<Chicken> do
                result <- typedLookupArray[i]
                i <- i + 1<Chicken>

        result


    [<Benchmark>]
    member _.StructTypedArrayOrdered () =
        let mutable result = 0

        for _ = 1 to loopCount do
            let mutable i = 0<Chicken>
            while i < structTypedLookupArray.Length - 1<Chicken> do
                result <- structTypedLookupArray[i]
                i <- i + 1<Chicken>

        result


[<EntryPoint>]
let main argv =

    let summary = BenchmarkRunner.Run<Benchmarks>()

    // let x = Benchmarks ()
    // let result = x.TypedArray ()
    // printfn "%A" result
    1
